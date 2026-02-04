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

    #region Table GetRow Tests
    [Fact]
    public void Table_GetRow_ShouldReturnTheCorrectRow()
    {
      //Arrange
      string tableName = "People";

      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      List<string> rowValues1 = new List<string> { "Mikel", "30" };
      List<string> rowValues2 = new List<string> { "Ane", "25" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, rowValues1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, rowValues2);

      table.AddRow(row1);
      table.AddRow(row2);

      //Act
      DbManager.Row retrievedRow1 = table.GetRow(0);
      DbManager.Row retrievedRow2 = table.GetRow(1);

      //Assert
      Assert.Equal(row1, retrievedRow1);
      Assert.Equal(row2, retrievedRow2);
    }
    [Fact]
    public void Table_GetRow_ShouldThrowException_WhenIndexIsOutOfRange()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);
      
      List<string> rowValues = new List<string> { "Mikel", "30" };
      
      DbManager.Row row = new DbManager.Row(columnDefinitions, rowValues);
      
      table.AddRow(row);
     
      //Act & Assert
      Assert.Throws<ArgumentException>(() => table.GetRow(-1));
      Assert.Throws<ArgumentException>(() => table.GetRow(1));
    }
    #endregion

    #region Table AddRow Tests
    [Fact]
    public void Table_AddRow_ShouldAddRowToTable()
    {
      //Arrange
      string tableName = "People";

      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      List<string> rowValues1 = new List<string> { "Mikel", "30" };
      List<string> rowValues2 = new List<string> { "Ane", "25" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, rowValues1);
      DbManager.Row row2 = new DbManager.Row(columnDefinitions, rowValues2);

      //Act
      table.AddRow(row1);
      table.AddRow(row2);

      //Assert
      Assert.Equal(row1, table.GetRow(0));
      Assert.Equal(row2, table.GetRow(1));
    }
    [Fact]
    public void Table_AddRow_ShouldThrowException_WhenRowColumnCountDoesNotMatch()
    {
      //Arrange
      string tableName = "People";

      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      List<string> rowValues = new List<string> { "Mikel" }; 

      DbManager.Row row = new DbManager.Row(columnDefinitions.Take(1).ToList(), rowValues);

      //Act & Assert
      Assert.Throws<ArgumentException>(() => table.AddRow(row));
    }
    [Fact]
    public void Table_AddRow_ShouldThrowException_WhenRowColumnDefinitiosDoNotMatch()
    {       
      //Arrange
      string tableName = "People";

      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      List<DbManager.ColumnDefinition> differentColumnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "FullName")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Years"))
      };

      List<string> rowValues = new List<string> { "Mikel", "30" };
      DbManager.Row row = new DbManager.Row(differentColumnDefinitions, rowValues);

      //Act & Assert
      Assert.Throws<ArgumentException>(() => table.AddRow(row));
    }
    #endregion

    #region Table NumRows Tests
    [Fact]
    public void Table_NumRows_ShouldReturnTheCorrectNumberOfRows()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      List<string> rowValues1 = new List<string> { "Mikel", "30" };
      List<string> rowValues2 = new List<string> { "Ane", "25" };

      DbManager.Row row1 = new DbManager.Row(columnDefinitions, rowValues1);

      DbManager.Row row2 = new DbManager.Row(columnDefinitions, rowValues2);

      //Act & Assert
      Assert.Equal(0, table.NumRows());
      table.AddRow(row1);
      Assert.Equal(1, table.NumRows());
      table.AddRow(row2);
      Assert.Equal(2, table.NumRows());
    }
    #endregion

    #region Table GetColumn Tests
    [Fact]
    public void Table_GetColumn_ShouldReturnTheCorrectColumn()
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
      DbManager.ColumnDefinition column1 = table.GetColumn(0);
      DbManager.ColumnDefinition column2 = table.GetColumn(1);

      //Assert
      Assert.Equal(column1, columnDefinitions[0]);
      Assert.Equal(column2, columnDefinitions[1]);
    }
    [Fact]
    public void Table_GetColumn_ShouldThrowException_WhenIndexIsOutOfRange()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      //Act & Assert
      Assert.Throws<ArgumentException>(() => table.GetColumn(-1));
      Assert.Throws<ArgumentException>(() => table.GetColumn(1));
    }
    #endregion

    #region Table NumRows Tests
    [Fact]
    public void Table_NumColumns_ShouldReturnTheCorrectNumberOfColumns()
    {
      //Arrange
      string tableName = "People";
      List<DbManager.ColumnDefinition> columnDefinitions = new List<DbManager.ColumnDefinition>
      {
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.String, "Name")),
        (new DbManager.ColumnDefinition(DbManager.ColumnDefinition.DataType.Int, "Age"))
      };

      DbManager.Table table = new DbManager.Table(tableName, columnDefinitions);

      //Act & Assert
      Assert.Equal(2, table.NumColumns());
    }
    #endregion
  }
}