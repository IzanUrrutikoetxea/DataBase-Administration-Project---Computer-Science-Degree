using System.Net.WebSockets;
using System.Runtime.InteropServices.Marshalling;
using DbManager;
namespace OurTests
{
  public class ColumnDefinitionsTests
  {
    #region ColumDefinition Constructor Tests
    [Theory]
    [InlineData(ColumnDefinition.DataType.String, "Name")]
    [InlineData(ColumnDefinition.DataType.Double, "Mark")]
    [InlineData(ColumnDefinition.DataType.Int, "Age")]
    public void ColumnDefinition_Constructor_ShouldInitializeAttributes(ColumnDefinition.DataType type, string name)
    {
      // Arrange & Act
      var columnDefinition = new ColumnDefinition(type, name);
      // Assert
      Assert.Equal(type, columnDefinition.Type);
      Assert.Equal(name, columnDefinition.Name);
    }
    [Fact]
    public void ColumnDefinition_Constructor_ShouldDoNothing_WhenNameIsEmpty()
    {
      //Act
      var columnDefinition = new ColumnDefinition(ColumnDefinition.DataType.String, string.Empty);

      //Assert
      Assert.Null(columnDefinition.Name);
    }
    [Fact]
    public void ColumnDefinition_Constructor_ShouldDoNothing_WhenNameIsNull()
    {
      //Act
      var columnDefinition = new ColumnDefinition(ColumnDefinition.DataType.String, null);

      //Assert
      Assert.Null(columnDefinition.Name);
    }
    #endregion

    #region AsText Tests
    [Theory]
    [InlineData("Age",DbManager.ColumnDefinition.DataType.Int)]
    [InlineData("Name",DbManager.ColumnDefinition.DataType.String)]
    [InlineData("Height",DbManager.ColumnDefinition.DataType.Double)]
    public void ColumnDefinition_AsText_ShouldReturnTheCOlumnAsString(string name, DbManager.ColumnDefinition.DataType type)
    {
      //Arrange
      var column = new DbManager.ColumnDefinition(type, name);

      //Act 
      var result = column.AsText();

      //Assert
      Assert.Equal(name + "->" + type.ToString(), result);
    }
    #endregion

    #region Parse Tests

    #endregion
  }
}