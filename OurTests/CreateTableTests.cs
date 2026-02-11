using DbManager;

namespace OurTests
{
  public class CreateTableTests
  {
    #region Constructor Tests
    [Fact]
    public void CreateTable_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<ColumnDefinition>()
      {
        (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
        (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
      };

      //Act
      var createTable = new DbManager.CreateTable(table, columns);

      //Assert
      Assert.Equal(table, createTable.Table);
      Assert.Equal(columns.Count, createTable.ColumnsParameters.Count);
    }
    #endregion

    #region Execute tests
    [Fact]
    public void CreateTable_Execute_ShouldWorkCorrectly()
    {
      //Arrange
      var database = Database.CreateTestDatabase();
      var tableName = "TestingTable";
      var columns = new List<ColumnDefinition>()
      {
        (new ColumnDefinition(ColumnDefinition.DataType.String, "Name")),
        (new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"))
      };
      var table = new Table(tableName, columns);
      var createTable = new DbManager.CreateTable(tableName, columns);

      //Act
      var result = createTable.Execute(database);

      //Assert
      Assert.Equal(table.ToString(), database.TableByName(tableName).ToString());
      Assert.Equal(Constants.CreateTableSuccess, result);
    }
    #endregion
  }
}
