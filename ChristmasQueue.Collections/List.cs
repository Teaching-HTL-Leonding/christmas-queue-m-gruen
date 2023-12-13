namespace ChristmasQueue.Collections;

/// <summary>
/// Represents a node in a list, holding a stack and a reference to the next node.
/// </summary>
/// <remarks>
/// Note that the class is "internal". That means that it is only visible inside the
/// project. Other projects cannot access it.
/// </remarks>
internal class ListNode
{
    /// <summary>
    /// Gets the stack contained in this node.
    /// </summary>
    public Stack Stack { get; }

    /// <summary>
    /// Gets or sets the next node in the list.
    /// </summary>
    public ListNode? Next { get; set; }

    /// <summary>
    /// Initializes a new instance of the ListNode class with a stack of specified maximum height.
    /// </summary>
    /// <param name="maxStackHeight">The maximum height of the stack contained in this node.</param>
    /// <remarks>
    /// Note that we are using the traditional constructor form here as it works both
    /// in .NET 7 and .NET 8. In .NET 8, you could use the Primary Constructor syntax.
    /// </remarks>
    public ListNode(int maxStackHeight)
    {
        Stack = new(maxStackHeight);
    }
}

/// <summary>
/// Represents a list of stacks.
/// </summary>
public class ListOfStacks
{
    /// <summary>
    /// Gets or sets the first node in the list.
    /// </summary>
    private ListNode? First { get; set; }

    /// <summary>
    /// Initializes a new instance of the ListOfStacks class with the specified number of stacks, each having a specified maximum height.
    /// </summary>
    /// <param name="numberOfStacks">The number of stacks to create in the list.</param>
    /// <param name="maxStackHeight">The maximum height of each stack in the list.</param>
    public ListOfStacks(int numberOfStacks, int maxStackHeight)
    {
        // TODO: Add implementation
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the stack at a specific index in the list.
    /// </summary>
    /// <param name="stackIndex">The index of the stack to retrieve.</param>
    /// <returns>The stack at the specified index, or null if the index is out of range.</returns>
    public Stack? GetAt(int stackIndex)
    {
        // TODO: Add implementation
        throw new NotImplementedException();
    }

    /// <summary>
    /// Determines whether all stacks in the list are homogeneous (i.e., all elements in each stack are the same).
    /// </summary>
    /// <returns>True if all stacks are homogeneous; otherwise, false.</returns>
    public bool AreAllStacksHomogeneous()
    {
        // TODO: Add implementation
        throw new NotImplementedException();
    }
}
