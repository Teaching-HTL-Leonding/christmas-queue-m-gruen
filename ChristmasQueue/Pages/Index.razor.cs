using System.Diagnostics;
using BlafettisLib;
using ChristmasQueue.Collections;
using Microsoft.AspNetCore.Components;

namespace ChristmasQueue.Pages;

public partial class Index
{
    #region Parameters from query string
    [SupplyParameterFromQuery]
    public int? Rows { get; set; }

    [SupplyParameterFromQuery]
    public int? Columns { get; set; }

    [SupplyParameterFromQuery]
    public int? Seed { get; set; }
    #endregion

    /// <summary>
    /// Gets or sets the list of stacks holding the data of the game.
    /// </summary>
    private ListOfStacks? Stacks { get; set; }

    /// <summary>
    /// Gets or sets the column index from which an item will be removed.
    /// </summary>
    private int? From { get; set; }

    /// <summary>
    /// Reference to the Blafettis component used to "throw confetti" at the end of the game.
    /// </summary>
    /// <remarks>
    /// For more information see https://github.com/ctrl-alt-d/blazor-dom-confetti.
    /// </remarks>
    private Blafettis? Blafettis { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the game has been won.
    /// </summary>
    public bool Won { get; set; }

    /// <summary>
    /// Gets or sets the number of moves that the user did so far.
    /// </summary>
    public int Moves { get; set; }

    /// <summary>
    /// Method that is called when all all parameters from the query string were set
    /// in the corresponding properties (see e.g. <see cref="Rows"/>).
    /// </summary>
    protected override void OnParametersSet()
    {
        // Set default value for parameters if they were not given in the query string.
        Columns ??= 8;
        Rows ??= 5;
        Seed ??= Random.Shared.Next();
        Moves = 0;

        // Create random number generator. Note that the seed can be used to recreate games.
        var rand = new Random(Seed.Value);

        // Create stacks holding the game's data.
        Stacks = new ListOfStacks(Columns.Value, Rows.Value);

        // Fill stacks randomly
        for (var column = 0; column < Columns; column++)
        {
            // Calculate the degree to which the column should be filled
            // (betweeen 75 and 100%).
            var numberOfItems = (int)(rand.Next(75, 100) / 100d * Rows);

            // Calculate the item that should be in the column. We have images
            // called candy1.png to candy12.png (see "wwwroot/images" folder).
            var content = $"candy{rand.Next(1, 13)}.png";
            for (var row = 0; row < numberOfItems; row++)
            {
                // Add element to the row. If it fails, throw an exception. The
                // exception will be visible in the browser's console.
                if (!(Stacks.GetAt(column)?.TryPush(content) ?? false))
                {
                    throw new InvalidOperationException("Could not add element to stack");
                }
            }
        }

        // Shuffle the elements around
        for (var i = 0; i < 1000; i++)
        {
            // Get source (must not be empty)
            int fromColumn;
            Stack fromStack;
            do
            {
                fromColumn = rand.Next(0, Columns.Value);
                fromStack = Stacks.GetAt(fromColumn) ?? throw new InvalidOperationException($"Could not get stack at position {fromColumn}");
            } while (fromStack.IsEmpty);
            var itemToMove = fromStack.Peek(0) ?? throw new InvalidOperationException("Could not peek into stack although it is not empty");

            // Get destination (must not be full and must not be equal to source)
            int toColumn;
            Stack toStack;
            do
            {
                toColumn = rand.Next(0, Columns.Value);
                toStack = Stacks.GetAt(toColumn) ?? throw new InvalidOperationException($"Could not get stack at position {toColumn}");
            } while (fromColumn == toColumn || toStack.IsFull);

            // Pop from source and push into destination
            if (!fromStack.TryPop(out _)) { throw new InvalidOperationException("Could not pop item from source although it is not empty"); }
            if (!toStack.TryPush(itemToMove)) { throw new InvalidOperationException("Could not push item into destination although it is not full"); }
        }
    }

    public void ItemClicked(int row, int column)
    {
        Debug.Assert(Stacks != null);

        // If user did not click on row with arrows or if game was already won,
        // exit the method early.
        if (row != Rows || Won) { return; }

        if (From == null)
        { 
            // From was empty before, so the user has just chosen the source column.
            From = column; 
        }
        else
        {
            // From was not empty before, so the user has just chosen the destination column.
            var fromStack = Stacks.GetAt(From.Value) ?? throw new InvalidOperationException($"Could not get stack at position {From.Value}");

            // Pop from source stack
            if (!fromStack.TryPop(out var content))
            {
                throw new InvalidOperationException("Could not pop element from stack");
            }

            // Push to destination stack
            var toStack = Stacks.GetAt(column) ?? throw new InvalidOperationException($"Could not get stack at position {column}");
            if (!toStack.TryPush(content))
            {
                throw new InvalidOperationException("Could not push element to stack");
            }

            // Increment moves
            Moves++;

            // Check if game has been won
            if (Stacks.AreAllStacksHomogeneous())
            {
                Won = true;
                Blafettis?.RaiseConfetti();
            }

            // Reset From so that the user can choose the next source column
            From = null;
        }
    }
}