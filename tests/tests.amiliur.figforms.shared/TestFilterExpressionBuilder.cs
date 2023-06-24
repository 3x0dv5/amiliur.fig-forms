using amiliur.figforms.shared;
using amiliur.web.shared.Filtering;
using Xunit.Abstractions;
using System.Linq.Expressions;

namespace tests.amiliur.figforms.shared;

public class TestElement
{
    public string Id { get; set; }
}

public class TestFilterExpressionBuilder
{
    private readonly ITestOutputHelper _testOutputHelper;

    private List<TestElement> _elements = new()
    {
        new TestElement
        {
            Id = "123456"
        },
        new TestElement
        {
            Id = "999"
        },
        new TestElement
        {
            Id = "998"
        },
        new TestElement
        {
            Id = "1000"
        },
    };

    public TestFilterExpressionBuilder(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void EqualField()
    {
        var filter = new FieldValueExpr("Id", "123456", WhereFilterType.Equal);
        var filterExp = FilterExpressionBuilder.BuildPredicate<TestElement>(filter);
        Assert.Equal("123456", _elements.AsQueryable().Where(filterExp).Single().Id);
    }
    
}