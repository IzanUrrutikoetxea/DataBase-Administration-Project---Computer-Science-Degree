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
    #region IsTrue Tests
    [Theory]
    [InlineData (true, "hello", "=", "hello", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (false, "hello", "=", "hi", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (true, "hello", "!=", "hi", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (false, "hello", "!=", "hello", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (false, "a", ">", "b", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (true, "b", ">", "a", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (true, "a", "<", "b", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (false, "b", "<", "a", DbManager.ColumnDefinition.DataType.String)]
    [InlineData (true, "2", "=", "2", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (false, "2", "=", "3", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (true, "2", "!=", "3", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (false, "2", "!=", "2", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (false, "2", ">", "3", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (true, "2", ">", "1", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (true, "2", "<", "3", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (false, "2", "<", "1", DbManager.ColumnDefinition.DataType.Int)]
    [InlineData (true, "2.3", "=", "2.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (false, "2.3", "=", "3.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (true, "2.3", "!=", "3.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (false, "2.3", "!=", "2.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (false, "2.3", ">", "3.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (true, "2.3", ">", "1.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (true, "2.3", "<", "3.3", DbManager.ColumnDefinition.DataType.Double)]
    [InlineData (false, "2.3", "<", "1.3", DbManager.ColumnDefinition.DataType.Double)]
    public void Condition_Constructor_ShouldReturnTheCorrectResultOfOperation(bool expectedResult, string value, string op, string valueCompar, DbManager.ColumnDefinition.DataType type)
    {
      //Arrange
      string columnName = "Name";
      var condition = new DbManager.Condition(columnName, op, valueCompar);

      //Act
      var result = condition.IsTrue(value, type);

      //Assert
      Assert.Equal(expectedResult, result);
    }
    #endregion

  }
}