using DbManager;
namespace OurTests
{
  public class ColumnDefinitionsTests
  {
    [Theory]
    [InlineData(ColumnDefinition.DataType.String, "ColumnA")] 
    [InlineData(ColumnDefinition.DataType.Int, "ColumnB")]
    [InlineData(ColumnDefinition.DataType.Double, "")]
    [InlineData(ColumnDefinition.DataType.String, "12345")]
    public void ColumnDefinition_Constructor_SetsPropertiesCorrectly(ColumnDefinition.DataType type, string name)
    {
      // Arrange & Act
      var columnDefinition = new ColumnDefinition(type, name);
      // Assert
      Assert.Equal(type, columnDefinition.Type);
      Assert.Equal(name, columnDefinition.Name);
    }


  }
}