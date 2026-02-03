using DbManager;
namespace OurTests
{
  public class ColumnDefinitionsTests
  {
    #region ColumDefinition Constructor Tests
    [Fact]
    public void ColumnDefinition_Constructor_ShouldNotInitializeAttributesWhenNameIsNull()
    {
      // Arrange & Act
      var columnDefinition = new ColumnDefinition(ColumnDefinition.DataType.String, null);
      // Assert
      Assert.NotEqual(ColumnDefinition.DataType.String, columnDefinition.Type);
    }
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
    public void ColumnDefinition_Constructor_ShouldNotInitializeAttributesWhenNameIsEmpty()
    {
      // Arrange & Act
      var columnDefinition = new ColumnDefinition(ColumnDefinition.DataType.String, "");
      // Assert
      Assert.NotEqual(ColumnDefinition.DataType.String, columnDefinition.Type);
    }
    #endregion
  }
}