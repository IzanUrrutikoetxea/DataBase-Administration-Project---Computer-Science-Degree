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

    #region Row SetValue Tests
    [Fact]
    public void Row_SetValue_ShouldUpdateValue_ForGivenColumnName()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      //Act
      row.SetValue("Age", "31");
      //Assert
      Assert.Equal("31", row.Values[1]);
    }
    [Fact]
    public void Row_SetValue_ShouldThrowException_WhenColumnNameDoesNotExist()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          row.SetValue("Height", "180")
      );
    }
    #endregion

    #region Row GetValue Tests
    [Fact]
    public void Row_GetValue_ShouldReturnValue_ForGivenColumnName()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      //Act
      string ageValue = row.GetValue("Age");
      //Assert
      Assert.Equal("30", ageValue);
    }
    [Fact]
    public void Row_GetValue_ShouldThrowException_WhenColumnNameDoesNotExist()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          row.GetValue("Height")
      );
    }
    #endregion

    #region Row GetColumnDefinition Tests
    [Fact]
    public void Row_GetColumnDefinition_ShouldReturnColumnDefinitions()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      List<string> values = new List<string> { "Mikel", "30" };

      DbManager.Row row = new DbManager.Row(columnDefinitions, values);

      //Act
      List<DbManager.ColumnDefinition> result = row.GetColumnDefinition();

      //Assert
      Assert.Equal(columnDefinitions, result);
    }
    #endregion
  }
}