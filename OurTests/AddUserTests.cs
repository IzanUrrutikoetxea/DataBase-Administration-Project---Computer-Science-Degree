using DbManager;

namespace OurTests
{
  public class AddUSerTests
  {
    #region Constructor Tests
    [Fact]
    public void AddUser_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var username = "admin";
      var password = "admin";
      var profileName = "pepito";

      //Act
      var addUser = new AddUser(username, password, profileName);

      //Assert
      Assert.Equal(username, addUser.Username);
      Assert.Equal(username, addUser.Password);
      Assert.Equal(username, addUser.ProfileName);

    }
    #endregion

    #region Execute tests
    //[Fact]
    //public void Delete_Execute_ShouldWorkCorrectly()
    //{
    //  //Arrange
    //  var table = "TestTable";
    //  var condition = new DbManager.Condition("Name", "=", "Pepe");
    //  var delete = new DbManager.Parser.Delete(table, condition);
    //  var database = Database.CreateTestDatabase();

    //  //Act
    //  var result = delete.Execute(database);

    //  //Assert
    //  Assert.Equal(Constants.DeleteSuccess, result);
    //  Assert.Equal(2, database.TableByName("TestTable").NumRows());
    //}
    #endregion
  }
}