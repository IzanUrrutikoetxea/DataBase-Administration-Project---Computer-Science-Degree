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
    public void ColumnDefinition_Constructor_ShouldThrow_WhenNameIsEmpty()
    {
      //Arrange & Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new ColumnDefinition(ColumnDefinition.DataType.String, "")
      );
    }
    [Fact]
    public void ColumnDefinition_Constructor_ShouldThrow_WhenNameIsNull()
    {
      //Arrange & Act & Assert
      Assert.Throws<ArgumentException>(() =>
          new ColumnDefinition(ColumnDefinition.DataType.String, null)
      );
    }
    #endregion
  }
}