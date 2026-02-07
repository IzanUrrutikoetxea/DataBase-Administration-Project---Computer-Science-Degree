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
    public void Row_Constructor_ShouldDoNothing_WhenColumnDefinitionsAndValuesCountMismatch()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel"};

      //Act
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);

      //Assert
      Assert.Null(row.Values);
      Assert.Empty(row.GetColumnDefinition());
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
    public void Row_SetValue_ShouldDoNothing_WhenColumnNameDoesNotExist()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      DbManager.Row changedRow = new DbManager.Row(columnDefinitions, values);

      //Act
      changedRow.SetValue("Height", "180");

      //Assert
      Assert.Equal(row.Values, changedRow.Values);
      Assert.Equal(row.GetColumnDefinition(), changedRow.GetColumnDefinition());
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
    public void Row_GetValue_ShouldReturnNull_WhenColumnNameDoesNotExist()
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
      string heightValue = row.GetValue("Height");

      //Assert
      Assert.Null(heightValue);
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

    #region Row IsTrue Tests
    [Fact] 
    public void Row_IsTrue_ShouldReturnTrue_WhenConditionIsSatisfied()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);

      DbManager.Condition condition = new DbManager.Condition("Age", "=", "30");

      //Act
      bool result = row.IsTrue(condition);

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void Row_IsTrue_ShouldReturnFalse_WhenConditionIsNotSatisfied()
    {
      //Arrange
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<string> values = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(columnDefinitions, values);
      DbManager.Condition condition = new DbManager.Condition("Age", ">", "30");
      //Act
      bool result = row.IsTrue(condition);
      //Assert
      Assert.False(result);
    }
    #endregion

    #region AsText Tests
    [Fact]
    public void Row_AsText_ShouldReturnTheRowAsString()
    {
      //Arrange
      var row1 = DbManager.Table.CreateTestTable().GetRow(0);
      var row2 = DbManager.Table.CreateTestTable().GetRow(1);
      var expectedResult1 = "Rodolfo:1.62:25";
      var expectedResult2 = "Maider:1.67:67";

      //Act
      var string1 = row1.AsText();
      var string2 = row2.AsText();

      //Assert
      Assert.Equal(expectedResult1, string1);
      Assert.Equal(expectedResult2, string2);
    }
    #endregion
  }
}