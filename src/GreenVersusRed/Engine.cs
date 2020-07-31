using System;
using System.Linq;

namespace GreenVersusRed
{
    /// <summary>
    /// This class is used for processing the input and printing of results.
    /// </summary>
    public class Engine
    {
        private Game game;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Engine()
        {
            
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Instance of the Game class</param>
        public Engine(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// This method is used for processing the input and printing the results.
        /// </summary>
        public void Run()
        {
            var gridSizes = Console.ReadLine()
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var gridWidth = gridSizes[0];
            var gridHeight = gridSizes[1];

            if (gridWidth > gridHeight || gridHeight > 1000)
            {
                Console.WriteLine("The grid height should be equal or bigger than the grid width. The width and the height should be in the range 1-999.");
            }
            else
            {
                var initialGrid = CreateInitialGrid(gridWidth, gridHeight);
            
                var arguments = Console.ReadLine()
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int coordinateX = arguments[0];
                int coordinateY = arguments[1];
                int generations = arguments[2];

                game = new Game(gridWidth, gridHeight, generations, initialGrid);

                game.Start();

                try
                {
                    Console.WriteLine(game.CalculateGreenCellsInAllGenerations(coordinateX, coordinateY));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        /// <summary>
        /// This method creates a grid based on user input.
        /// </summary>
        /// <param name="gridWidth">Width of the grid.</param>
        /// <param name="gridHeight">Height of the grid.</param>
        /// <returns>Returns a grid with user input.</returns>
        private Grid CreateInitialGrid(int gridWidth, int gridHeight)
        {
            var grid = new Grid(gridWidth);

            for (int rowIndex = 0; rowIndex < gridWidth; rowIndex++)
            {
                var value = Console.ReadLine().ToCharArray();

                var cells = value.Select(x => new Cell(x.ToString())).ToArray();

                grid.Cells[rowIndex] = cells;
            }

            return grid;
        }
    }
}