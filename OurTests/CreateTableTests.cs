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

      //Act
      var dropTable = new DbManager.DropTable(table);

      //Assert
      Assert.Equal(table, dropTable.Table);
    }
    #endregion
  }
}
