using DbManager;

namespace OurTests
{
  public class DeleteTests
  {
    #region Constructor Tests
    [Fact]
    public void Delete_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var condition = new DbManager.Condition("Name", "=", "Paco");

      //Act
      var delete = new DbManager.Parser.Delete(table, condition);

      //Assert
      Assert.Equal(table, delete.Table);
      Assert.Equal(condition, delete.Where);
    }
    #endregion

    #region Execute tests
    [Fact]
    public void Delete_Execute_ShouldWorkCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var condition = new DbManager.Condition("Name", "=", "Pepe");
      var delete = new DbManager.Parser.Delete(table, condition);
      var database = Database.CreateTestDatabase();

      //Act
      var result = delete.Execute(database);

      //Assert
      Assert.Equal(Constants.DeleteSuccess, result);
      Assert.Equal(2, database.TableByName("TestTable").NumRows());
    }
    #endregion
  }
}
