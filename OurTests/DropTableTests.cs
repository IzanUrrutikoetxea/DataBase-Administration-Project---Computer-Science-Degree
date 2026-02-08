using DbManager;

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

      //Act
      var dropTable = new DbManager.DropTable(table);

      //Assert
      Assert.Equal(table, dropTable.Table);
    }
    #endregion

    #region Execute tests
    [Fact]
    public void DropTable_Execute_ShouldWorkCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var dropTable = new DbManager.DropTable(table);
      var database = Database.CreateTestDatabase();

      //Act
      var result = dropTable.Execute(database);

      //Assert
      Assert.Equal(Constants.DropTableSuccess, result);
      Assert.Null(database.TableByName(table));
      
    }
    #endregion
  }
}
