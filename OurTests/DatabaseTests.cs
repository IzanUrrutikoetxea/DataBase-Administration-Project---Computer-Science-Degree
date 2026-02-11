using System.Reflection.Metadata;
using DbManager;

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

    #region DropTable Tests
    [Fact]
    public void Database_DropTable_ShouldReturnFalse_WhenTableDoesNotExst()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      var result = database.DropTable("Pepe");

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_DropTable_ShouldSetCorrectLastMessage_WhenTableDoesNotExst()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      var result = database.DropTable("Pepe");

      //Assert
      Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_DropTable_ShouldReturnTrue_WhenTableDoesExst()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      var result = database.DropTable("TestTable");

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Database_DropTable_ShouldSetCorrectLastMessage_WhenTableDoesExst()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      var result = database.DropTable("TestTable");

      //Assert
      Assert.Equal(DbManager.Constants.DropTableSuccess, database.LastErrorMessage);
    }
    [Fact]
    public void Database_DropTable_ShouldCorrectlyDropTable_WhenTableDoesExst()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();

      //Act
      var result = database.DropTable("TestTable");

      //Assert
      Assert.Null(database.TableByName("TestTable"));
    }
    #endregion

    #region Insert Tests
    [Fact]
    public void Database_Insert_ShouldReturnFalse_WhenTheTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var values = new List<string>() { "Pepe", "137.21", "11" };

      //Act
      var result = database.Insert("Pepe", values);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_Insert_ShouldSetLastMessageCorrecty_WhenTheTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var values = new List<string>() { "Pepe", "137.21", "11" };

      //Act
      var result = database.Insert("Pepe", values);

      //Assert
      Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Insert_ShouldReturnTrue_WhenTheTableDoesExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var values = new List<string>() { "Pepe", "137.21", "11" };

      //Act
      var result = database.Insert("TestTable", values);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Database_Insert_ShouldSetLastMessageCorrecty_WhenTheTableDoesExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var values = new List<string>() { "Pepe", "137.21", "11" };

      //Act
      var result = database.Insert("TestTable", values);

      //Assert
      Assert.Equal(DbManager.Constants.InsertSuccess, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Insert_ShouldInsertRowCorrectly_WhenTheTableDoesExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var values = new List<string>() { "Pepe", "137.21", "11" };

      //Act
      var result = database.Insert("TestTable", values);

      //Assert
      Assert.Equal(4, database.TableByName("TestTable").NumRows());
    }
    #endregion

    #region Select Tests
    [Fact]
    public void Database_Select_ShouldReturnNull_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Age" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("Pepe", columns, condition);

      //Assert
      Assert.Null(table);
    }
    [Fact]
    public void Database_Select_ShouldSetAppropietalyLastMessage_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Age" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("Pepe", columns, condition);

      //Assert
      Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Select_ShouldReturnNull_WhenAColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Class" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("TestTable", columns, condition);

      //Assert
      Assert.Null(table);
    }
    [Fact]
    public void Database_Select_ShouldSetAppropietalyLastMessage_WhenAColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Class" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("TestTable", columns, condition);

      //Assert
      Assert.Equal(DbManager.Constants.ColumnDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Select_ShouldReturnATable_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Height" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("TestTable", columns, condition);

      //Assert
      Assert.NotNull(table);
    }
    [Fact]
    public void Database_Select_ShouldReturnTheCorrect_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var columns = new List<string>() { "Height" };
      var condition = new DbManager.Condition("Name", "=", "Maider");

      //Act
      var table = database.Select("TestTable", columns, condition);

      //Assert
      Assert.Equal(table.ToString(), DbManager.Table.CreateTestTable().Select(columns, condition).ToString());
    }
    #endregion

    #region DeleteWhere Tests
    [Fact]
    public void Database_DeleteWhere_ShouldReturnFalse_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.DeleteWhere("Pepe", condition);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_DeleteWhere_ShouldSetTheCorrectLastMessage_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.DeleteWhere("Pepe", condition);

      //Assert
      Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_DeleteWhere_ShouldReturnFalse_WhenColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Pepe", "=", "25");

      //Act
      var result = database.DeleteWhere("TestTable", condition);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_DeleteWhere_ShouldSetTheCorrectLastMessage_WhenColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Pepe", "=", "25");

      //Act
      var result = database.DeleteWhere("TestTable", condition);

      //Assert
      Assert.Equal(DbManager.Constants.ColumnDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_DeleteWhere_ShouldReturnTrue_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.DeleteWhere("TestTable", condition);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Database_DeleteWhere_ShouldDeleteRowsCorrectly_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var condition = new DbManager.Condition("Age", "=", "25");
      var expectedTable = DbManager.Table.CreateTestTable();
      expectedTable.DeleteWhere(condition);

      //Act
      var result = database.DeleteWhere("TestTable", condition);

      //Assert
      Assert.Equal(expectedTable.ToString(), database.TableByName("TestTable").ToString());
    }
    #endregion

    #region Update Tests
    [Fact]
    public void Database_Update_ShouldReturnFalse_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.Update("Pepe", setValues, condition);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_Update_ShouldReturnFalse_WhenColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Pepe", "=", "25");

      //Act
      var result = database.Update("TestTable", setValues, condition);

      //Assert
      Assert.False(result);
    }
    [Fact]
    public void Database_Update_ShouldSetLastMessageCorrectly_WhenTableDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.Update("Pepe", setValues, condition);

      //Assert
      Assert.Equal(DbManager.Constants.TableDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Update_ShouldSetLastMessageCorrectly_WhenColumnDoesNotExist()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Pepe", "=", "25");

      //Act
      var result = database.Update("TestTable", setValues, condition);

      //Assert
      Assert.Equal(DbManager.Constants.ColumnDoesNotExistError, database.LastErrorMessage);
    }
    [Fact]
    public void Database_Update_ShouldReturnTrue_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Age", "=", "25");

      //Act
      var result = database.Update("TestTable", setValues, condition);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Database_Update_ShouldUpdateTheDatabaseCorrectly_WhenAllGoesOk()
    {
      //Arrange
      var database = DbManager.Database.CreateTestDatabase();
      var setValues = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Age","32")),
        (new DbManager.Parser.SetValue("Height","123.22"))
      };
      var condition = new DbManager.Condition("Age", "=", "25");

      var table = DbManager.Table.CreateTestTable();
      var x = table.Update(setValues, condition);
      //Act
      var result = database.Update("TestTable", setValues, condition);

      //Assert
      Assert.Equal(database.TableByName("TestTable").ToString(), table.ToString());
    }
    #endregion

    #region Save Tests
    [Fact]
    public void Database_Save_ShouldCreateAFileWithDatabaseName()
    {
      //Arrange
      var database = Database.CreateTestDatabase();
      var fileName = "TestTable.txt";

      if (File.Exists(fileName)) File.Delete(fileName);

      //Act
      var result = database.Save(fileName);

      //Assert
      Assert.True(result);
      Assert.True(File.Exists(fileName));
    }
    [Fact]
    public void Database_Save_ShouldCorrectlySaveAllData()
    {
      //Arrange
      var database = Database.CreateTestDatabase();
      var fileName = "TestTable.txt";

      if (File.Exists(fileName)) File.Delete(fileName);

      //Act
      database.Save(fileName);
      var lines = File.ReadAllLines(fileName);

      //Assert
      Assert.NotEmpty(lines);
      Assert.Equal("TestTable", lines[0]);
      Assert.Equal("String,Double,Int", lines[1]);
      Assert.Contains("['Name','Height','Age']", lines[2]);
      Assert.Contains("Rodolfo", lines[2]);
      Assert.Contains("Maider", lines[2]);
      Assert.Contains("Pepe", lines[2]);
    }
    #endregion

    #region Load Tests
    [Fact]
    public void Database_Load_ShoulLoadCorrectlyAllData()
    {
      //Arrange
      var database = Database.CreateTestDatabase();
      var fileName = "test_db_load.txt";

      if (File.Exists(fileName)) File.Delete(fileName);

      database.Save(fileName);

      //Act
      var resultDatabase = Database.Load(fileName, Database.AdminUsername, Database.AdminPassword);

      //Assert
      Assert.NotNull(resultDatabase);

      var table = resultDatabase.TableByName(Table.TestTableName);
      Assert.NotNull(table);

      Assert.Equal(3, table.NumRows());
      Assert.Equal(3, table.NumColumns());

      Assert.Equal("Rodolfo", table.GetRow(0).Values[0]);
      Assert.Equal("1.62", table.GetRow(0).Values[1]);
      Assert.Equal("25", table.GetRow(0).Values[2]);

      Assert.Equal("Maider", table.GetRow(1).Values[0]);
      Assert.Equal("Pepe", table.GetRow(2).Values[0]);
    }
    #endregion
  }
}