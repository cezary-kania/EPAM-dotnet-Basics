namespace BinaryGap.Tests;

public class BinaryGapCounterTests
{
    [Theory]
    [InlineData(9, 2)]
    [InlineData(529, 4)]
    [InlineData(20, 1)]
    [InlineData(15, 0)]
    [InlineData(1041, 5)]
    [InlineData(32, 0)]
    public void Solution_WhenCalledWithValidInput_ShouldReturnLongestBinaryGap(int n, int expectedResult)
    {
        // Act
        var result = BinaryGapCounter.Solution(n);
        
        // Assert
        Assert.Equal(expectedResult, result);
    }
}