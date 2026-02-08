using DbManager;

namespace OurTests
{
  public class InsertTests
  {
    #region Constructor Tests
    [Fact]
    public void Insert_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var values = new List<string>() { "Paco" };
      //Act
      var insert = new DbManager.Insert(table, values);

      //Assert
      Assert.Equal(table, insert.Table);
      Assert.Equal(values, insert.Values);
    }
    #endregion

    #region Execute tests
    [Fact]
    public void Insert_Execute_ShouldWorkCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var values = new List<string>() { "Paco", "157.45", "23" };
      var insert = new DbManager.Insert(table, values);
      var database = Database.CreateTestDatabase();

      //Act
      var result = insert.Execute(database);

      //Assert
      Assert.Equal(Constants.InsertSuccess, result);
      Assert.Equal("Paco", database.TableByName("TestTable").GetRow(3).Values[0]);
      Assert.Equal("157.45", database.TableByName("TestTable").GetRow(3).Values[1]);
      Assert.Equal("23", database.TableByName("TestTable").GetRow(3).Values[2]);
    }
    #endregion
  }
}
