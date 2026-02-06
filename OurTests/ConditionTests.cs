namespace OurTests
{
  public class ConditionTests
  {
    #region Constructor Tests
    [Fact]
    public void Condition_Constructor_ShouldInitializeCorrectlyValues()
    {
      //Arrange
      var columnName = "name";
      var operatorString = "=";
      var literalValue = "30";

      //Act
      var condition = new DbManager.Condition(columnName, operatorString, literalValue);

      //Assert 
      Assert.Equal(columnName, condition.ColumnName);
      Assert.Equal(operatorString, condition.Operator);
      Assert.Equal(literalValue, condition.LiteralValue);
    }
    [Theory]
    [InlineData(null, "=", "30")]
    [InlineData("Name", null, "30")]
    [InlineData("Name", "=", null)]
    [InlineData("", "=", "30")]
    [InlineData("Name", "", "30")]
    [InlineData("Name", "=", "")]
    public void Condition_Constuctor_ShouldDoNothingIfAnyIsNullOrEmprty(string columnName, string operatorString, string value)
    {
      //Act
      var condition = new DbManager.Condition(columnName, operatorString, value);
      //Assert
      Assert.Null(condition.ColumnName);
      Assert.Null(condition.Operator);
      Assert.Null(condition.LiteralValue);
    }
    #endregion
  }
}