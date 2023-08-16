using amiliur.shared.Json;
using amiliur.web.shared.Models.Generic;

namespace amiliur.figforms.shared;

public class FormDataSaveContainer: ISerializableModel
{
    public BaseEditModel Data { get; set; } = null!;
    public FormDefinition FormDefinition { get; set; } =null!;

    // ReSharper disable once EmptyConstructor
    public FormDataSaveContainer()
    {
        
    }
}