namespace OddEven.Tests;

public class OddEvenNumberConverterTest
{
    [Theory]
    [InlineData(1, 20, new[] { "Odd", "2", "3", "Even", "5", "Even", "7", "Even", "Odd", "Even", "11", "Even", "13", "Even", "Odd", "Even", "17", "Even", "19", "Even" })]
    [InlineData(5, 15, new[] { "5", "Even", "7", "Even", "Odd", "Even", "11", "Even", "13", "Even", "Odd" })]
    [InlineData(20, 30, new[] { "Even", "Odd", "Even", "23", "Even", "Odd", "Even", "Odd", "Even", "29", "Even" })]
    [InlineData(5, 5, new[] { "5" })]
    public void GetPrintableNumbers_WhenRangeIsValid_ShouldReturnPrintableCollectionFromRange(int start, int end, string[] expectedOutput)
    {
        var printableNumbers = OddEvenNumberConverter.GetPrintableNumbers(start, end);
        Assert.Equal(expectedOutput, printableNumbers);
    }
    
    [Fact]
    public void GetPrintableNumbers_WhenStartHasHigherValueThanEnd_ShouldThrowException()
    {
        // Arrange
        var action = () => OddEvenNumberConverter.GetPrintableNumbers(50, 20);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
    
    [Fact]
    public void GetPrintableNumbers_WhenStartIsNegative_ShouldThrowException()
    {
        // Arrange
        var action = () => OddEvenNumberConverter.GetPrintableNumbers(-20, 50);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
}