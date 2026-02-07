namespace OurTests
{
  public class DropTableTests
  {
    #region Constructor Tests
    [Fact]
    public void DropTable_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<DbManager.ColumnDefinition>()
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      //Act
      var createTable = new DbManager.CreateTable(table, columns);

      //Assert
      Assert.Equal(table, createTable.Table);
      Assert.Equal(columns, createTable.ColumnsParameters);
    }
    #endregion
  }
}
