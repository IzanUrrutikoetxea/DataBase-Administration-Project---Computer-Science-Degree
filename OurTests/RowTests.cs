using DbManager;

namespace OurTests
{
  public class RowTests
  {
    #region Row Constructor 
    [Fact]
    public void Row_Constructor_ShouldInitializeMemberVariables()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };

      //Act
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);

      //Assert
      Assert.Equal(values, row.Values);
    }
    [Fact]
    public void Row_Constructor_ShouldThrowException_WhenColumnDefinitionsAndValuesCountMismatch()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel"};

      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new Row(columnDefinitions, values)
      );
    }   
    [Fact]
    public void Row_Constructor_ShouldThrowException_WhenColumnDefinitionsIsEmpty()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition> {};
      List<string> values = new List<string> { "Mikel" , "30" };

      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new Row(columnDefinitions, values)
      );
    }
    [Fact]
    public void Row_Constructor_ShouldThrowException_WhenValuesIsEmpty()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> {};

      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new Row(columnDefinitions, values)
      );
    }
    #endregion
  }
}