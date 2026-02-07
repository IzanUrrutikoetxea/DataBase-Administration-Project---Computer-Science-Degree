namespace OurTests
{
  public class SelectTests
  {
    #region Constructor Tests
    [Fact]
    public void Select_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var table = "TestTable";
      var columns = new List<string>() { "Age", "Name" };
      var condition = new DbManager.Condition("Name", "=", "Paco");

      //Act
      var select = new DbManager.Select(table, columns, condition);

      //Assert
      Assert.Equal(table, select.Table);
      Assert.Equal(columns, select.Columns);
      Assert.Equal(condition, select.Where);
    }
    #endregion
  }
}
