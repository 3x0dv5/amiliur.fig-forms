using amiliur.figforms.shared.Fields.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace amiliur.figforms.web.blazor.Form.Components;

/// <summary>
/// Displays a list of validation messages for a specified field within a cascaded <see cref="EditContext"/>.
/// </summary>
public class FigValidationMessage : ComponentBase, IDisposable
{
    private EditContext _previousEditContext;
    private BaseFormFieldModel _previousField;
    private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
    private FieldIdentifier _fieldIdentifier;


    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created <c>div</c> element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

    [CascadingParameter] EditContext CurrentEditContext { get; set; } = default!;

    /// <summary>
    /// Specifies the field for which validation messages should be displayed.
    /// </summary>
    [Parameter] public BaseFormFieldModel? Field { get; set; }

    /// <summary>
    /// Instance of the object containing the data
    /// </summary>
    [Parameter] public object? Model { get; set; }

    public FigValidationMessage()
    {
        _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
                                                $"of type {nameof(EditContext)}. For example, you can use {GetType()} inside " +
                                                $"an {nameof(EditForm)}.");
        }

        if (Field == null || Model == null)
        {
            throw new InvalidOperationException($"{GetType()} requires a value for the {nameof(Field)} parameter.");
        }

        if (Field != _previousField)
        {
            _fieldIdentifier = new FieldIdentifier(Model, Field.FieldName);
            _previousField = Field;
        }

        if (CurrentEditContext != _previousEditContext)
        {
            DetachValidationStateChangedListener();
            CurrentEditContext.OnValidationStateChanged += _validationStateChangedHandler;
            _previousEditContext = CurrentEditContext;
        }
    }

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        foreach (var message in CurrentEditContext.GetValidationMessages(_fieldIdentifier))
        {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", "validation-message");
            builder.AddContent(3, message);
            builder.CloseElement();
        }
    }

    /// <summary>
    /// Called to dispose this instance.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> if called within <see cref="IDisposable.Dispose"/>.</param>
    protected virtual void Dispose(bool disposing)
    {
    }

    void IDisposable.Dispose()
    {
        DetachValidationStateChangedListener();
        Dispose(disposing: true);
    }

    private void DetachValidationStateChangedListener()
    {
        if (_previousEditContext != null)
        {
            _previousEditContext.OnValidationStateChanged -= _validationStateChangedHandler;
        }
    }
}