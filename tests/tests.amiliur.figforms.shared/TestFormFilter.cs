using amiliur.shared.Json;
using amiliur.web.shared.Filtering;
using tests.amiliur.figforms.shared.Asserting;
using Xunit.Abstractions;

namespace tests.amiliur.figforms.shared;

public class TestFormFilter
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestFormFilter(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestFilter()
    {
        var filter = new BinaryExpr
        {
            FilterType = WhereFilterType.And,
            Left = new FieldValueExpr
            {
                FilterType = WhereFilterType.Equal,
                Field = "field_1",
                Value = "angel"
            },
            Right = new BinaryExpr
            {
                FilterType = WhereFilterType.Or,
                Left = new FieldValueExpr
                {
                    FilterType = WhereFilterType.NotEqual,
                    Field = "field_2",
                    Value = "north"
                },
                Right = new BinaryExpr
                {
                    FilterType = WhereFilterType.Or,
                    Left = new BinaryExpr
                    {
                        FilterType = WhereFilterType.And,
                        Left = new BinaryExpr
                        {
                            FilterType = WhereFilterType.And,
                            Left = new FieldValueExpr
                            {
                                FilterType = WhereFilterType.Equal,
                                Field = "field_1",
                                Value = "field_2"
                            },
                            Right = new FieldValueExpr
                            {
                                FilterType = WhereFilterType.Any,
                                Field = "field_3",
                                Value = new List<object> {1.25, 2.0}
                            }
                        },
                        Right = new BinaryExpr
                        {
                            FilterType = WhereFilterType.And,
                            Left = new FieldValueExpr
                            {
                                FilterType = WhereFilterType.GreaterThan,
                                Field = "field_4",
                                Value = 15
                            },
                            Right = new BinaryExpr
                            {
                                FilterType = WhereFilterType.And,
                                Left = new FieldValueExpr
                                {
                                    FilterType = WhereFilterType.LessThanOrEqual,
                                    Field = "field_4",
                                    Value = 20
                                },
                                Right = new BinaryExpr
                                {
                                    FilterType = WhereFilterType.And,
                                    Left = new FieldValueExpr
                                    {
                                        FilterType = WhereFilterType.Equal,
                                        Field = "field_5",
                                        Value = true
                                    },
                                    Right = new UnaryExpr
                                    {
                                        FilterType = WhereFilterType.IsNotNull,
                                        Operand = new FieldValueExpr {Field = "field_6"}
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };


        var expected = """
        {
          "__objectType__": "BinaryExpr, amiliur.figforms.shared",
          "Left": {
            "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
            "Field": "field_1",
            "Value": "angel",
            "FilterType": "Equal"
          },
          "Right": {
            "__objectType__": "BinaryExpr, amiliur.figforms.shared",
            "Left": {
              "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
              "Field": "field_2",
              "Value": "north",
              "FilterType": "NotEqual"
            },
            "Right": {
              "__objectType__": "BinaryExpr, amiliur.figforms.shared",
              "Left": {
                "__objectType__": "BinaryExpr, amiliur.figforms.shared",
                "Left": {
                  "__objectType__": "BinaryExpr, amiliur.figforms.shared",
                  "Left": {
                    "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                    "Field": "field_1",
                    "Value": "field_2",
                    "FilterType": "Equal"
                  },
                  "Right": {
                    "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                    "Field": "field_3",
                    "Value": [
                      1.25,
                      2
                    ],
                    "FilterType": "Any"
                  },
                  "FilterType": "And"
                },
                "Right": {
                  "__objectType__": "BinaryExpr, amiliur.figforms.shared",
                  "Left": {
                    "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                    "Field": "field_4",
                    "Value": 15,
                    "FilterType": "GreaterThan"
                  },
                  "Right": {
                    "__objectType__": "BinaryExpr, amiliur.figforms.shared",
                    "Left": {
                      "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                      "Field": "field_4",
                      "Value": 20,
                      "FilterType": "LessThanOrEqual"
                    },
                    "Right": {
                      "__objectType__": "BinaryExpr, amiliur.figforms.shared",
                      "Left": {
                        "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                        "Field": "field_5",
                        "Value": true,
                        "FilterType": "Equal"
                      },
                      "Right": {
                        "__objectType__": "UnaryExpr, amiliur.figforms.shared",
                        "Operand": {
                          "__objectType__": "FieldValueExpr, amiliur.figforms.shared",
                          "Field": "field_6",
                          "Value": null,
                          "FilterType": "None"
                        },
                        "FilterType": "IsNotNull"
                      },
                      "FilterType": "And"
                    },
                    "FilterType": "And"
                  },
                  "FilterType": "And"
                },
                "FilterType": "And"
              },
              "Right": null,
              "FilterType": "Or"
            },
            "FilterType": "Or"
          },
          "FilterType": "And"
        }
    """;
        _testOutputHelper.WriteLine(filter.ToJson(SerializableModel.SerializerOptions(true)));
        Assertions.EqualString(filter.ToJson(SerializableModel.SerializerOptions(true)), expected);
    }
}