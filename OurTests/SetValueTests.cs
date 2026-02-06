using DbManager.Parser;

namespace OurTests
{
  public class SetValueTests
  {
    #region Constructor Tests
    [Fact]
    public void SetValue_Constructor_ShouldInitializeCorrectlyVariables()
    {
      //Arrange
      var columnName = "Age";
      var value = "30";

      //Act
      var setValue = new SetValue(columnName, value);

      //Assert
      Assert.Equal(columnName, setValue.ColumnName);
      Assert.Equal(value, setValue.Value);
    }
    [Theory]
    [InlineData("","30")]
    [InlineData(null,"30")]
    [InlineData("Age","")]
    [InlineData("Age",null)]
    public void SetValue_Constructor_ShouldDoNothing_WhenAnyOfValuesAreEmptyOrNull(string name, string value)
    {
      //Act
      var setValue = new SetValue(name, value);

      //Assert
      Assert.Null(setValue.ColumnName);
      Assert.Null(setValue.Value);
    }
    #endregion
  }
}

