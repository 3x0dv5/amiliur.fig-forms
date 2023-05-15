using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using amiliur.figforms.shared.Fields.Models;
using amiliur.figforms.shared.Validation;
using amiliur.shared.Reflection;
using Microsoft.AspNetCore.Components.Forms;

namespace amiliur.figforms.web.blazor.Form.Components;

/// <summary>
/// Extension methods to add DataAnnotations validation to an <see cref="EditContext"/>.
/// </summary>
public static class EditContextFigFieldExtensions
{
    private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache = new();

    /// <summary>
    /// Adds DataAnnotations validation support to the <see cref="EditContext"/>.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    public static EditContext AddFigFieldValidation(this EditContext editContext, List<BaseFormFieldModel> formFields)
    {
        if (editContext == null)
        {
            throw new ArgumentNullException(nameof(editContext));
        }

        var messages = new ValidationMessageStore(editContext);

        // Perform object-level validation on request
        editContext.OnValidationRequested += (sender, eventArgs) => ValidateModel((EditContext) sender!, messages, formFields);

        // Perform per-field validation on each field edit
        editContext.OnFieldChanged += (sender, eventArgs) =>
        {
            Console.WriteLine($"Received the event OnFieldChange for {eventArgs.FieldIdentifier.FieldName}");
            // validate only if the field is in the formfields
            if (formFields.Select(m => m.FieldName).Contains(eventArgs.FieldIdentifier.FieldName))
                ValidateField(editContext, messages, formFields, eventArgs.FieldIdentifier);
        };

        return editContext;
    }

    private static bool TryValidateObject(object instance, List<BaseFormFieldModel> formFields, List<ValidationResult> validationResults)
    {
        if (instance == null)
        {
            Console.WriteLine("Objecto model é null");
            return false;
        }

        var properties = instance
            .GetType()
            .GetProperties()
            .Where(p => formFields.Select(m => m.FieldName).ToList().Contains(p.Name))
            .ToList();

        foreach (var property in properties)
        {
            TryValidateProperty(property, instance.GetPropertyValue(property.Name), formFields, validationResults);
        }

        return true;
    }

    private static bool TryValidateProperty(PropertyInfo propertyInfo, object propertyValue, List<BaseFormFieldModel> formFields, List<ValidationResult> results)
    {
        var formField = formFields.Single(m => m.FieldName.Equals(propertyInfo.Name));
        var validationRules = formField.ValidationRules ?? new List<FormFieldValidation>();

        results.AddRange(
            validationRules
                .Select(rule => rule.IsValid(propertyValue, formField))
                .Where(r => r != null)
        );
        return true;
    }

    private static void ValidateModel(EditContext editContext, ValidationMessageStore messages, List<BaseFormFieldModel> formFields)
    {
        var validationResults = new List<ValidationResult>();
        TryValidateObject(editContext.Model, formFields, validationResults);

        // Transfer results to the ValidationMessageStore
        messages.Clear();
        foreach (var validationResult in validationResults)
        {
            if (validationResult == null)
            {
                continue;
            }

            if (!validationResult.MemberNames.Any())
            {
                messages.Add(new FieldIdentifier(editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage!);
                continue;
            }

            foreach (var memberName in validationResult.MemberNames)
            {
                messages.Add(editContext.Field(memberName), validationResult.ErrorMessage!);
            }
        }

        editContext.NotifyValidationStateChanged();
    }

    private static void ValidateField(EditContext editContext, ValidationMessageStore messages, List<BaseFormFieldModel> formFields, in FieldIdentifier fieldIdentifier)
    {
        if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
        {
            var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
            var results = new List<ValidationResult>();

            TryValidateProperty(propertyInfo, propertyValue, formFields, results);
            messages.Clear(fieldIdentifier);
            var __messages = results.Select(r => r.ErrorMessage);
            messages.Add(fieldIdentifier, __messages);

            // We have to notify even if there were no messages before and are still no messages now,
            // because the "state" that changed might be the completion of some async validation task
            editContext.NotifyValidationStateChanged();
        }
    }

    private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
    {
        var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
        if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
        {
            // DataAnnotations only validates public properties, so that's all we'll look for
            // If we can't find it, cache 'null' so we don't have to try again next time
            propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

            // No need to lock, because it doesn't matter if we write the same value twice
            _propertyInfoCache[cacheKey] = propertyInfo;
        }

        return propertyInfo != null;
    }
}