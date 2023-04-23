using amiliur.figforms.shared.Attributes.Datagrid.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace amiliur.figforms.shared.DependencyInjection;

public static class FigFormsServiceExtensions
{
    public static void AddFigForms(this IServiceCollection services)
    {
        services.AddSingleton<IGridSettingsReader, GridSettingsAttributesReaderObject>();
    }
}