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
  }
}
