using System.ComponentModel.DataAnnotations;
using System.Globalization;
using amiliur.figforms.shared.Fields.Models;
using amiliur.shared.Json;

namespace amiliur.figforms.shared.Validation
{
    public abstract class FormFieldValidation: ISerializableModel
    {
        public string ErrorMessageString { get; set; }

        public FormFieldValidation()
        {
        }

        protected FormFieldValidation(string errorMessageString)
        {
            ErrorMessageString = errorMessageString;
        }

        public virtual string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);

        protected abstract bool IsValid(object value);

        public ValidationResult IsValid(object value, BaseFormFieldModel field)
        {
            var result = ValidationResult.Success;

            // call overridden method.
            if (!IsValid(value))
            {
                string[] memberNames = field.FieldName != null ? new string[] {field.FieldName} : null;
                result = new ValidationResult(FormatErrorMessage(field.DisplayName), memberNames);
            }

            return result;
        }
    }

    public class RequiredFormFieldValidation : FormFieldValidation
    {
        public bool AllowEmptyStrings { get; set; }

        public RequiredFormFieldValidation() : base("Atributo {0} é obrigatório")
        {
        }

        protected override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            // only check string length if empty strings are not allowed
            if (!AllowEmptyStrings)
            {
                if (value is string)
                {
                    if (string.IsNullOrEmpty(value.ToString()?.Trim()))
                        return false;
                }
            }

            return true;
        }
    }
}