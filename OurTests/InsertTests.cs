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
  }
}
