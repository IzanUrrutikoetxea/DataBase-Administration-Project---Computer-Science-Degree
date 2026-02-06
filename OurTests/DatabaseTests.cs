using System.Reflection.Metadata;

namespace OurTests
{
  public class UnitTest1
  {
    #region AddTable Tests
    [Fact]
    public void Database_AddTable_ShouldAddTableToDatabaseCorrectly()
    {
      //Assert
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      database.AddTable(table);

      //Assert
      Assert.Equal(table, database.TableByName(tableName));
    }
    #endregion

    #region TableByName Tests
    [Fact]
    public void Database_TableByName_ShouldReturnTheCorrectTable()
    {
      //Assert
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      var database = DbManager.Database.CreateTestDatabase();
      database.AddTable(table);

      //Act
      var table1 = database.TableByName(tableName);
      var table2 = database.TableByName("TestTable");

      //Assert
      Assert.Equal(DbManager.Table.CreateTestTable().ToString(), table2.ToString());
      Assert.Equal(table.ToString(), table1.ToString());
    }
    #endregion

    #region CreateTable Tests
    [Fact]
    public void Database_CreateTable_ShouldReturnFalse_WhenTableAlreadyExists()
    {
      //Assert
      string tableName = "TestTable";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_CreateTable_ShouldSetLastMessageApropietaly_WhenTableAlreadyExists()
    {
      //Assert
      string tableName = "TestTable";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.Equal(DbManager.Constants.TableAlreadyExistsError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_CreateTable_ShouldReturnFalse_WhenNoColumnIsProvided()
    {
      //Assert
      string tableName = "Pepe";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_CreateTable_ShouldSetLastMessageApropietaly_WhenNoColumnIsProvided()
    {
      //Assert
      string tableName = "Pepe";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.Equal(DbManager.Constants.DatabaseCreatedWithoutColumnsError, database.LastErrorMessage);
    }
    public void Database_CreateTable_ShouldReturnTrue_WhenAllGoesOk()
    {
      //Assert
      string tableName = "Pepe";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Database_CreateTable_ShouldSetLastMessageApropietaly_WhenAllGoesOk()
    {
      //Assert
      string tableName = "Pepe";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);

      //Assert
      Assert.Equal(DbManager.Constants.CreateTableSuccess, database.LastErrorMessage);
    }
    [Fact]
    public void Database_CreateTable_ShouldAddTheTable_WhenAllGoesOk()
    {
      //Assert
      string tableName = "Pepe";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Double, "Height"))
      };
      List<string> valuesRow1 = new List<string> { "30", "Paco", "183.22" };
      List<string> valuesRow2 = new List<string> { "22", "Miren", "165.08" };
      List<string> valuesRow3 = new List<string> { "56", "Pedro", "188.57" };
      List<string> valuesRow4 = new List<string> { "14", "Paco", "154.77" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, valuesRow1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, valuesRow2);
      DbManager.Row row3 = new DbManager.Row(columnDefinitions, valuesRow3);
      DbManager.Row row4 = new DbManager.Row(columnDefinitions, valuesRow4);

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      var database = DbManager.Database.CreateTestDatabase();

      //Act
      bool result = database.CreateTable(tableName, columnDefinitions);
      var tableResult = database.TableByName(tableName);

      //Assert
      Assert.Equal(table.ToString(), tableResult.ToString());
    }
    #endregion
  }
}