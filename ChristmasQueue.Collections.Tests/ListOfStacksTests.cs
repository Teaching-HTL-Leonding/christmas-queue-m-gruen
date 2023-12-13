namespace ChristmasQueue.Collections.Tests;

public class ListOfStacksTests
{
    [Fact]
    public void GetAt_WithValidIndex_ShouldReturnStack()
    {
        // Arrange
        var listOfStacks = new ListOfStacks(3, 2);

        // Act
        var stack = listOfStacks.GetAt(1);

        // Assert
        Assert.NotNull(stack);
    }

    [Fact]
    public void GetAt_WithInvalidIndex_ShouldReturnNull()
    {
        // Arrange
        var listOfStacks = new ListOfStacks(2, 2);

        // Act
        var stack = listOfStacks.GetAt(2); // Index out of range

        // Assert
        Assert.Null(stack);
    }

    [Fact]
    public void AreAllStacksHomogeneous_WhenAllStacksAreHomogeneous_ShouldReturnTrue()
    {
        // Arrange
        var listOfStacks = new ListOfStacks(2, 2);
        var stack1 = listOfStacks.GetAt(0);
        Assert.NotNull(stack1);
        stack1.TryPush("item1");
        stack1.TryPush("item1");

        var stack2 = listOfStacks.GetAt(1);
        Assert.NotNull(stack2);
        stack2.TryPush("item2");
        stack2.TryPush("item2");

        // Act
        var result = listOfStacks.AreAllStacksHomogeneous();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AreAllStacksHomogeneous_WhenAnyStackIsNotHomogeneous_ShouldReturnFalse()
    {
        // Arrange
        var listOfStacks = new ListOfStacks(2, 2);
        var stack1 = listOfStacks.GetAt(0);
        Assert.NotNull(stack1);
        stack1.TryPush("item1");
        stack1.TryPush("item1");

        var stack2 = listOfStacks.GetAt(1);
        Assert.NotNull(stack2);
        stack2.TryPush("item2");
        stack2.TryPush("item3");

        // Act
        var result = listOfStacks.AreAllStacksHomogeneous();

        // Assert
        Assert.False(result);
    }
}
