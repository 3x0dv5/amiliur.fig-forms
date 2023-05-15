using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using amiliur.figforms.shared;
using amiliur.figforms.shared.Fields.Models;
using Microsoft.AspNetCore.Components;

namespace amiliur.figforms.web.blazor.Components.Standard;

public partial class FigLabel<T> : ComponentBase
{
    [Parameter] public Expression<Func<T>> For { get; set; }

    [Parameter] public bool? OverriddenRequired { get; set; } = null;
    [Parameter] public bool Visible { get; set; } = true;
    [Parameter] public BaseFormFieldModel ForField { get; set; }
    [Parameter] public FormDefinition ForForm { get; set; }

    private IDictionary<string, object> _additionalAttributes = new Dictionary<string, object>
    {
        {"class", "col-form-label"}
    };

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes
    {
        get => _additionalAttributes;
        set
        {
            if (value == null) return;

            foreach (var (attrName, attrValue) in value)
            {
                if (attrName == "class")
                {
                    _additionalAttributes[attrName] += $" {attrValue}";
                }
                else
                {
                    _additionalAttributes[attrName] = attrValue;
                }
            }
        }
    }

    private string _description = null;
    private string Description => _description ??= GetDescription();

    private MarkupString Label => new(GetDisplayName());

    private TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
    {
        var expression = For.Body;
        if (expression is MemberExpression memberExpression)
        {
            if (memberExpression.Member.GetCustomAttribute(typeof(TAttribute)) is TAttribute ta)
                return ta;
        }
        else if (expression is MethodCallExpression methodCallExpression)
        {
            var result = (PropertyInfo) Expression.Lambda(methodCallExpression).Compile().DynamicInvoke();
            if (result != null && result.GetCustomAttribute(typeof(TAttribute)) is TAttribute tattr)
                return tattr;
        }

        return null;
    }

    private string GetPropertyName()
    {
        var expression = For.Body;
        if (expression is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        if (expression is MethodCallExpression methodCallExpression)
        {
            var result = (PropertyInfo) Expression.Lambda(methodCallExpression).Compile().DynamicInvoke();
            if (result != null)
                return result.Name;
        }

        return "";
    }

    private string GetDescription()
    {
        if (ForField != null)
            return GetDescriptionForField();

        var att = GetAttribute<DescriptionAttribute>();
        return att?.Description ?? "";
    }

    private string GetDescriptionForField()
    {
        return !string.IsNullOrEmpty(ForField.Description) ? ForField.Description : string.Empty;
    }

    private string GetDisplayName()
    {
        if (ForField != null)
            return GetDisplayNameForField();

        var value = GetAttribute<DisplayAttribute>();
        return value?.Name ?? GetPropertyName() ?? "";
    }

    private string GetDisplayNameForField()
    {
        if (!string.IsNullOrEmpty(ForField.DisplayName))
            return ForField.DisplayName;
        return ForField.FieldName;
    }

    private bool IsRequired()
    {
        if (OverriddenRequired != null)
            return OverriddenRequired.Value;

        if (ForField != null)
            return IsRequiredForField();

        if (GetAttribute<RequiredAttribute>() != null)
            return true;
        return false;
    }

    private bool IsRequiredForField()
    {
        return ForField.IsRequired().GetValueOrDefault(false);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (IsRequired())
        {
            _additionalAttributes["class"] += " required";
        }

        ForAttribute();
    }

    private string Name()
    {
        return ForField != null ? ForField.FieldName : For.Name;
    }

    private void ForAttribute()
    {
        if (AdditionalAttributes.ContainsKey("for"))
            return;
        if (ForField != null && ForForm != null)
            AdditionalAttributes["for"] = $"{ForForm.Id}-{ForField.FieldName}";
    }
}