using amiliur.shared.Extensions;
using Serilog;

namespace tests.amiliur.figforms.shared.Asserting;

public static class Assertions
{ 
    public static void EqualString(string actual, string expected)
    {
        try
        {
            Assert.Equal(expected.TrimLines().ReplaceLineEndings(), actual.TrimLines().ReplaceLineEndings());
        }
        catch (Exception e)
        {
            Log.Information("Actual: \n\n{actual}\n\n\n\n.", actual);
            Log.Information("\n\nExpected: \n\n{expected}", expected);
            throw;
        }
    }
}