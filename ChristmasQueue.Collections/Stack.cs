namespace ChristmasQueue.Collections;

/// <summary>
/// Represents a node in a stack, holding the content and a reference to the next node.
/// </summary>
/// <remarks>
/// Note that the class is "internal". That means that it is only visible inside the
/// project. Other projects cannot access it.
/// </remarks>
internal class StackNode
{
    /// <summary>
    /// Gets the content of the stack node.
    /// </summary>
    public string Content { get; }

    /// <summary>
    /// Gets or sets the next node in the stack.
    /// </summary>
    public StackNode? Next { get; set; }

    /// <summary>
    /// Initializes a new instance of the StackNode class with the specified content.
    /// </summary>
    /// <param name="content">The content to store in the node.</param>
    /// <remarks>
    /// Note that we are using the traditional constructor form here as it works both
    /// in .NET 7 and .NET 8. In .NET 8, you could use the Primary Constructor syntax.
    /// </remarks>
    public StackNode(string content)
    {
        Content = content;
    }
}

/// <summary>
/// Represents a simple stack data structure with a maximum height.
/// </summary>
public class Stack
{
    /// <summary>
    /// Gets or sets the first node in the stack.
    /// </summary>
    private StackNode? First { get; set; }
    private int MaxHeight { get; set; }
    private int CurrentHeight { get; set; } = 0;

    /// <summary>
    /// Initializes a new instance of the Stack class with the specified maximum height.
    /// </summary>
    /// <param name="maxHeight">The maximum number of elements the stack can hold.</param>
    /// <remarks>
    /// Note that we are using the traditional constructor form here as it works both
    /// in .NET 7 and .NET 8. In .NET 8, you could use the Primary Constructor syntax.
    /// </remarks>
    public Stack(int maxHeight)
    {
        MaxHeight = maxHeight;
    }

    /// <summary>
    /// Attempts to add a new item to the top of the stack.
    /// </summary>
    /// <param name="content">The content to add to the stack.</param>
    /// <returns>True if the item was successfully added; otherwise, false.</returns>
    public bool TryPush(string content)
    {
        if (IsFull) { return false; }

        var newNode = new StackNode(content) { Next = First };
        First = newNode;
        CurrentHeight++;
        return true;
    }

    /// <summary>
    /// Attempts to remove the item at the top of the stack.
    /// </summary>
    /// <param name="content">The content of the removed item, if successful.</param>
    /// <returns>True if the item was successfully removed; otherwise, false.</returns>
    public bool TryPop(out string content)
    {
        if (IsEmpty)
        {
            content = "";
            return false;
        }

        content = First!.Content;
        First = First.Next;
        CurrentHeight--;
        return true;
    }

    /// <summary>
    /// Peeks at a specific depth in the stack without removing the item.
    /// </summary>
    /// <param name="depth">The depth to peek at, where 0 is the top of the stack.</param>
    /// <returns>The content at the specified depth, or null if the depth exceeds the stack size.</returns>
    public string? Peek(int depth)
    {
        var current = First;
        for (int i = 0; i < depth; i++)
        {
            if (current is null) { return null; }
            current = current.Next;
        }
        return current?.Content; // Here was the error. I was returning current!.Content
    }

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    public bool IsEmpty => First is null; // Done!

    /// <summary>
    /// Gets a value indicating whether the stack is full.
    /// </summary>
    public bool IsFull => CurrentHeight == MaxHeight; // Done!

    /// <summary>
    /// Checks if all elements in the stack are the same.
    /// </summary>
    /// <returns>True if all elements are the same, or the stack is empty; otherwise, false.</returns>
    public bool IsHomogeneous()
    {
        for (var current = First; current is not null; current = current.Next)
        {
            if (current.Content != First!.Content) { return false; }
        }
        return true;
    }
}
