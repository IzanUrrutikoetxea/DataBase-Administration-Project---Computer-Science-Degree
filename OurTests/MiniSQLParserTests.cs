using DbManager;

namespace OurTests
{
  public class MiniSQLParserTests
  {
    #region Parse Select Tests
    [Fact]
    public void MiniSQLParser_Parse_ShouldParseSelectCorrectly()
    {
      //Arrange
      var tableName = "TestTable";
      var columns = new List<string>() { "Age", "Height", "Name" };
      var condition = new Condition("Age", "=", "30");
      var expectedReturn = new Select(tableName, columns, condition);

      //Act
      var result = MiniSQLParser.Parse("SELECT Age,Height,Name FROM TestTable WHERE Age=30");

      //Assert
      Assert.IsType<Select>(result);
      
      var select = (Select)result;

      Assert.Equal(expectedReturn.Table, select.Table);
      Assert.Equal(expectedReturn.Columns, select.Columns);
      Assert.Equal(expectedReturn.Where.ColumnName, select.Where.ColumnName);
      Assert.Equal(expectedReturn.Where.Operator, select.Where.Operator);
      Assert.Equal(expectedReturn.Where.LiteralValue, select.Where.LiteralValue);
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldParseSelectCorrectlyWithSpaces()
    {
      //Arrange
      var tableName = "TestTable";
      var columns = new List<string>() { "Age", "Height", "Name" };
      var condition = new Condition("Age", "=", "30");
      var expectedReturn = new Select(tableName, columns, condition);

      //Act
      var result = MiniSQLParser.Parse("SELECT         Age,Height,Name FROM         TestTable          WHERE     Age=30");

      //Assert
      Assert.IsType<Select>(result);
      
      var select = (Select)result;

      Assert.Equal(expectedReturn.Table, select.Table);
      Assert.Equal(expectedReturn.Columns, select.Columns);
      Assert.Equal(expectedReturn.Where.ColumnName, select.Where.ColumnName);
      Assert.Equal(expectedReturn.Where.Operator, select.Where.Operator);
      Assert.Equal(expectedReturn.Where.LiteralValue, select.Where.LiteralValue);
    }

    [Theory]
    [InlineData("Select Age,Height,Name FROM TestTable WHERE Age=30")]
    [InlineData("SELECT Age,Height,Name from TestTable WHERE Age=30")]
    [InlineData("SELECT Age,Height,Name FROM TestTable wHERE Age=30")]
    public void MiniSQLParser_Parse_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("SELECT Age,Height,Name FROM TestTable1 WHERE Age=30")]
    [InlineData("SELECT Age,Height,Name FROM TestTable_ WHERE Age=30")]
    public void MiniSQLParser_Parse_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT Age,Height,Name FROM  WHERE Age=30"));
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldReturnNull_ForMissingCondition()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT Age,Height,Name FROM TestTable WHERE "));
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldReturnNull_ForMissingColumns()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT  FROM TestTable WHERE Age=30"));
    }
    #endregion
  }
}
