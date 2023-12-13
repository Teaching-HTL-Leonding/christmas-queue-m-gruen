namespace ChristmasQueue.Collections.Tests;

public class StackTests
{
    [Fact]
    public void Push_ToEmptyStack_ShouldSucceed()
    {
        // Arrange
        var stack = new Stack(3);

        // Act
        var result = stack.TryPush("item1");

        // Assert
        Assert.True(result);
        Assert.False(stack.IsEmpty);
    }

    [Fact]
    public void Pop_FromNonEmptyStack_ShouldReturnLastElement()
    {
        // Arrange
        var stack = new Stack(3);
        stack.TryPush("item1");

        // Act
        var result = stack.TryPop(out string content);

        // Assert
        Assert.True(result);
        Assert.Equal("item1", content);
    }

    [Fact]
    public void Pop_FromEmptyStack_ShouldFail()
    {
        // Arrange
        var stack = new Stack(3);

        // Act
        var result = stack.TryPop(out string content);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Peek_OnNonEmptyStack_ShouldReturnElementAtDepth()
    {
        // Arrange
        var stack = new Stack(3);
        stack.TryPush("item1");
        stack.TryPush("item2");

        // Act
        var topContent = stack.Peek(0);
        var bottomContent = stack.Peek(1);

        // Assert
        Assert.Equal("item2", topContent);
        Assert.Equal("item1", bottomContent);
    }

    [Fact]
    public void IsEmpty_Initially_ShouldBeTrue()
    {
        // Arrange
        var stack = new Stack(3);

        // Assert
        Assert.True(stack.IsEmpty);
    }

    [Fact]
    public void IsFull_WhenFull_ShouldBeTrue()
    {
        // Arrange
        var stack = new Stack(1);
        stack.TryPush("item1");

        // Assert
        Assert.True(stack.IsFull);
    }

    [Fact]
    public void Push_WhenFull_ShouldFail()
    {
        // Arrange
        var stack = new Stack(1);
        stack.TryPush("item1");

        // Act
        var result = stack.TryPush("item2");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsHomogeneous_WithAllSameElements_ShouldReturnTrue()
    {
        // Arrange
        var stack = new Stack(3);
        stack.TryPush("item");
        stack.TryPush("item");

        // Act
        var result = stack.IsHomogeneous();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsHomogeneous_WithDifferentElements_ShouldReturnFalse()
    {
        // Arrange
        var stack = new Stack(3);
        stack.TryPush("item1");
        stack.TryPush("item2");

        // Act
        var result = stack.IsHomogeneous();

        // Assert
        Assert.False(result);
    }
}
