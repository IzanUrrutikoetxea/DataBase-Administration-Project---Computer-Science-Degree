using DbManager;

namespace OurTests
{
  public class DeleteUserTests
  {
    #region Constructor Tests
    [Fact]
    public void DeleteUser_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var profileName = "pepito";

      //Act
      var deleteUser = new DeleteUser(profileName);

      //Assert
      Assert.Equal(profileName, deleteUser.Username);

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