using DbManager;
using DbManager.Parser;

namespace OurTests
{
  public class UpdateTests
  {
    #region Constructor Tests
    [Fact]
    public void Update_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<DbManager.Parser.SetValue>() 
      {
        (new DbManager.Parser.SetValue("Name", "Miguel"))
      };
      var condition = new DbManager.Condition("Name", "=", "Paco");

      //Act
      var update = new DbManager.Update(table, columns, condition);

      //Assert
      Assert.Equal(table, update.Table);
      Assert.Equal(columns, update.Columns);
      Assert.Equal(condition, update.Where);
    }
    #endregion

    #region Execute tests
    [Fact]
    public void Update_Execute_ShouldWorkCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<DbManager.Parser.SetValue>()
      {
        (new DbManager.Parser.SetValue("Name", "Paco"))
      };
      var condition = new DbManager.Condition("Name", "=", "Rodolfo");
      var update = new DbManager.Update(table, columns, condition);
      var database = Database.CreateTestDatabase();

      //Act
      var result = update.Execute(database);

      //Assert
      Assert.Equal(Constants.UpdateSuccess, result);
      Assert.Equal("Paco", database.TableByName("TestTable").GetRow(0).Values[0]);
    }
    #endregion
  }
}
