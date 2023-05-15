using amiliur.figforms.shared.Fields.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace amiliur.figforms.web.blazor.Form.Components;

/// <summary>
/// Adds Data Annotations validation support to an <see cref="EditContext"/>.
/// </summary>
public class FigFieldDataValidator : ComponentBase
{
    [CascadingParameter] EditContext CurrentEditContext { get; set; } = null!;
    [Parameter] public List<BaseFormFieldModel> FormFields { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(FigFieldDataValidator)} requires a cascading " +
                                                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FigFieldDataValidator)} " +
                                                $"inside an EditForm.");
        }

        CurrentEditContext.AddFigFieldValidation(FormFields);
    }
}