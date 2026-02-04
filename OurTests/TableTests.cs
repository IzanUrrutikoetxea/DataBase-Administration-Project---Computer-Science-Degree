namespace OurTests
{
    public class TableTests
    {
    #region Table Constructor Tests
    [Fact]
    public void Table_Constructor_ShouldInitializeMemberVariables()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      //Act
      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      //Assert
      Assert.Equal(tableName, table.Name);
      Assert.Equal(0, table.NumRows());
      Assert.Equal(columnDefinitions.Count, table.NumColumns());
      for (int i = 0; i < columnDefinitions.Count; i++)
      {
        Assert.Equal(columnDefinitions[i], table.GetColumn(i));
      }
    }
    [Fact]
    public void Table_Constructor_ShouldThrowException_WhenTableNameIsEmpty()
    {
      //Arrange
      string tableName = "";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new DbManager.Table(tableName, columnDefinitions)
      );
    }
    [Fact]
    public void Table_Constructor_ShouldThrowException_WhenTableNameIsNull()
    {
      //Arrange
      string? tableName = null;
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new DbManager.Table(tableName, columnDefinitions)
      );
    }
    [Fact]
    public void Table_Constructor_ShouldThrowException_WhenColumnDefinitionsIsEmpty()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition> { };
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new DbManager.Table(tableName, columnDefinitions)
      );
    }
    [Fact]
    public void Table_Constructor_ShouldThrowException_WhenColumnDefinitionsHasDuplicateNames()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Name"))
      };
      //Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new DbManager.Table(tableName, columnDefinitions)
      );
    }
    #endregion

    #region Table HasDuplicatedColumnNames Tests
    [Fact]
    public void Table_HasDuplicatedColumnNames_ShouldReturnTrue_WhenThereAreDuplicateNames()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      List<DbManager.ColumnDefinition> columnDefinitionsCheck = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Name"))
      };
      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      //Act
      bool hasDuplicates = table.HasDuplicatedColumnNames(columnDefinitionsCheck);
      //Assert
      Assert.True(hasDuplicates);
    }
    [Fact]
    public void Table_HasDuplicatedColumnNames_ShouldReturnFalse_WhenThereAreNoDuplicateNames()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };
      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      //Act
      bool hasDuplicates = table.HasDuplicatedColumnNames(columnDefinitions);
      //Assert
      Assert.False(hasDuplicates);
    }
    #endregion
  }
}