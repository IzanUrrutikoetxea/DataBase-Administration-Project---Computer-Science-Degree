using System.Collections.Generic;
using System.Xml.Linq;
using DbManager;
using DbManager.Parser;

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
    public void MiniSQLParser_Parse_Select_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("SELECT Age,Height,Name FROM TestTable1 WHERE Age=30")]
    [InlineData("SELECT Age,Height,Name FROM TestTable_ WHERE Age=30")]
    public void MiniSQLParser_Parse_Select_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_Select_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT Age,Height,Name FROM  WHERE Age=30"));
    }

    [Fact]
    public void MiniSQLParser_Parse_Select_ShouldReturnNull_ForMissingCondition()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT Age,Height,Name FROM TestTable WHERE "));
    }

    [Fact]
    public void MiniSQLParser_Parse_Select_ShouldReturnNull_ForMissingColumns()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("SELECT  FROM TestTable WHERE Age=30"));
    }
    #endregion

    #region Parse Insert Tests
    [Fact]
    public void MiniSQLParser_Parse_ShouldParseInsertCorrectly()
    {
      //Arrange
      var tableName = "TestTable";
      var values = new List<string>() { "32", "137.24", "Paco" };
      var expectedReturn = new Insert(tableName, values);

      //Act
      var result = MiniSQLParser.Parse("INSERT INTO TestTable VALUES (32,137.24,Paco)");

      //Assert
      Assert.IsType<Insert>(result);

      var insert = (Insert)result;

      Assert.Equal(expectedReturn.Table, insert.Table);
      Assert.Equal(expectedReturn.Values, insert.Values);
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldParseInsertCorrectlyWithSpaces()
    {
      //Arrange
      var tableName = "TestTable";
      var values = new List<string>() { "32", "137.24", "Paco" };
      var expectedReturn = new Insert(tableName, values);

      //Act
      var result = MiniSQLParser.Parse("INSERT         INTO         TestTable            VALUES           (32,137.24,Paco)");

      //Assert
      Assert.IsType<Insert>(result);

      var insert = (Insert)result;

      Assert.Equal(expectedReturn.Table, insert.Table);
      Assert.Equal(expectedReturn.Values, insert.Values);
    }

    [Theory]
    [InlineData("insert into TestTable VALUES (32,137.24,Paco)")]
    [InlineData("INSERT INTO TestTable values (32,137.24,Paco)")]
    [InlineData("INSERT into TestTable VALUES (32,137.24,Paco)")]
    public void MiniSQLParser_Parse_Insert_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("INSERT INTO TestTable1 VALUES (32,137.24,Paco)")]
    [InlineData("INSERT INTO TestTable_1 VALUES (32,137.24,Paco)")]
    public void MiniSQLParser_Parse_Insert_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_Insert_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("INSERT INTO  VALUES (32,137.24,Paco)"));
    }

    [Fact]
    public void MiniSQLParser_Parse_Insert_ShouldReturnNull_ForMissingValues()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("INSERT INTO TestTable VALUES "));
    }
    #endregion

    #region Parse DropTable Tests
    [Fact]
    public void MiniSQLParser_Parse_ShouldParseDropTableCorrectly()
    {
      //Arrange
      var tableName = "TestTable";
      var expectedReturn = new DropTable(tableName);

      //Act
      var result = MiniSQLParser.Parse("DROP TABLE TestTable");

      //Assert
      Assert.IsType<DropTable>(result);

      var dropTable = (DropTable)result;

      Assert.Equal(expectedReturn.Table, dropTable.Table);
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldParseDropTableCorrectlyWithSpaces()
    {
      //Arrange
      var tableName = "TestTable";
      var expectedReturn = new DropTable(tableName);

      //Act
      var result = MiniSQLParser.Parse("DROP       TABLE            TestTable");

      //Assert
      Assert.IsType<DropTable>(result);

      var dropTable = (DropTable)result;

      Assert.Equal(expectedReturn.Table, dropTable.Table);
    }

    [Theory]
    [InlineData("drop TABLE TestTable")]
    [InlineData("DROP table TestTable")]
    [InlineData("DROp TABLE TestTable")]
    public void MiniSQLParser_Parse_DropTable_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("DROP TABLE TestTable_1")]
    [InlineData("DROP TABLE TestTable1")]
    public void MiniSQLParser_Parse_DropTable_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_DropTable_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("DROP TABLE "));
    }
    #endregion

    #region Parse Create Table Tests
    [Fact]
    public void MiniSQLParser_Parse_ShouldParseCreateTableCorrectly()
    {
      //Arrange
      var tableName = "TestTable";
      var columns = new List<ColumnDefinition>()
      {
        (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
        (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
      };
      var expectedReturn = new CreateTable(tableName, columns);

      //Act
      var result = MiniSQLParser.Parse("CREATE TABLE TestTable (Name String,Age Int)");

      //Assert
      Assert.IsType<CreateTable>(result);

      var createTable = (CreateTable)result;

      Assert.Equal(expectedReturn.Table, createTable.Table);
      Assert.Equal(expectedReturn.ColumnsParameters.Count, createTable.ColumnsParameters.Count);
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldParseCreateTableCorrectlyWithSpaces()
    {
      //Arrange
      var tableName = "TestTable";
      var columns = new List<ColumnDefinition>()
      {
        (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
        (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
      };
      var expectedReturn = new CreateTable(tableName, columns);

      //Act
      var result = MiniSQLParser.Parse("CREATE               TABLE    TestTable              (Name String,Age Int)");

      //Assert
      Assert.IsType<CreateTable>(result);

      var createTable = (CreateTable)result;

      Assert.Equal(expectedReturn.Table, createTable.Table);
      Assert.Equal(expectedReturn.ColumnsParameters.Count, createTable.ColumnsParameters.Count);
    }

    [Theory]
    [InlineData("create TABLE TestTable (Name String,Age Int)")]
    [InlineData("CREATE table TestTable (Name String,Age Int)")]
    [InlineData("CREATE tABLE TestTable (Name String,Age Int)")]
    public void MiniSQLParser_Parse_CreateTable_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("CREATE TABLE TestTable1 (Name String,Age Int)")]
    [InlineData("CREATE TABLE TestTable_1 (Name String,Age Int)")]
    public void MiniSQLParser_Parse_CreateTable_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_CreateTable_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("CREATE TABLE  (Name String,Age Int)"));
    }

    [Fact]
    public void MiniSQLParser_Parse_CreateTable_ShouldNotReturnNull_ForMissingValues()
    {
      //Arrange
      var tableName = "TestTable";
      var columns = new List<ColumnDefinition>()
      {
      };
      var expectedReturn = new CreateTable(tableName, columns);

      //Act
      var result = MiniSQLParser.Parse("CREATE TABLE TestTable");

      //Assert
      Assert.IsType<CreateTable>(result);

      var createTable = (CreateTable)result;

      Assert.Equal(expectedReturn.Table, createTable.Table);
      Assert.Equal(expectedReturn.ColumnsParameters, createTable.ColumnsParameters);
    }
    #endregion

    #region Parse Update Tests
    [Fact]
    public void MiniSQLParser_Parse_ShouldParseUpdateCorrectly()
    {
      //Arrange
      var tableName = "TestTable";
      var values = new List<SetValue>()
      {
        (new SetValue("Name", "Paco")),
        (new SetValue("Age", "22"))
      };
      var condition = new Condition("Age", "=", "31");

      var expectedReturn = new Update(tableName, values, condition);

      //Act
      var result = MiniSQLParser.Parse("UPDATE TestTable SET Name=Paco,Age=22 WHERE Age=31");

      //Assert
      Assert.IsType<Update>(result);

      var update = (Update)result;

      Assert.Equal(expectedReturn.Table, update.Table);
      Assert.Equal(expectedReturn.Columns.Count, update.Columns.Count);
      Assert.Equal(expectedReturn.Where.Operator, update.Where.Operator);
      Assert.Equal(expectedReturn.Where.ColumnName, update.Where.ColumnName);
      Assert.Equal(expectedReturn.Where.LiteralValue, update.Where.LiteralValue);
    }

    [Fact]
    public void MiniSQLParser_Parse_ShouldParseUpdateCorrectlyWithSpaces()
    {
      //Arrange
      var tableName = "TestTable";
      var values = new List<SetValue>()
      {
        (new SetValue("Name", "Paco")),
        (new SetValue("Age", "22"))
      };
      var condition = new Condition("Age", "=", "31");

      var expectedReturn = new Update(tableName, values, condition);

      //Act
      var result = MiniSQLParser.Parse("UPDATE      TestTable    SET         Name=Paco,Age=22      WHERE    Age=31");

      //Assert
      Assert.IsType<Update>(result);

      var update = (Update)result;

      Assert.Equal(expectedReturn.Table, update.Table);
      Assert.Equal(expectedReturn.Columns.Count, update.Columns.Count);
      Assert.Equal(expectedReturn.Where.Operator, update.Where.Operator);
      Assert.Equal(expectedReturn.Where.ColumnName, update.Where.ColumnName);
      Assert.Equal(expectedReturn.Where.LiteralValue, update.Where.LiteralValue);
    }

    [Theory]
    [InlineData("update TestTable SET Name = Paco, Age = 22 WHERE Age = 31")]
    [InlineData("UPDATE TestTable set Name=Paco,Age=22 WHERE Age=31")]
    [InlineData("UPDATE TestTable SET Name=Paco,Age=22 where Age=31")]
    public void MiniSQLParser_Parse_Update_ShouldReturnNull_ForIncorrectCapitalization(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Theory]
    [InlineData("UPDATE TestTabl1 SET Name=Paco,Age=22 WHERE Age=31")]
    [InlineData("UPDATE TestTable_1 SET Name=Paco,Age=22 WHERE Age=31")]
    public void MiniSQLParser_Parse_Update_ShouldReturnNull_ForIncorrectTableNameWithForbiddenChars(string query)
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse(query));
    }

    [Fact]
    public void MiniSQLParser_Parse_Update_ShouldReturnNull_ForMissingTableName()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("UPDATE  SET Name=Paco,Age=22 WHERE Age=31"));
    }

    [Fact]
    public void MiniSQLParser_Parse_Update_ShouldReturnNull_ForMissingSetValues()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("UPDATE TestTable SET  WHERE Age=31"));
    }

    [Fact]
    public void MiniSQLParser_Parse_Update_ShouldReturnNull_ForMissingCondition()
    {
      //Assert
      Assert.Null(MiniSQLParser.Parse("UPDATE TestTable SET Name=Paco,Age=22 WHERE "));
    }
    #endregion
  }
}
