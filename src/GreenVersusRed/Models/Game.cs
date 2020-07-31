using System;
using System.Collections.Generic;

namespace GreenVersusRed
{
    public class Game
    {
        /// <summary>
        /// Constant value for representing Green cells.
        /// </summary>
        private const string Green = "1";
        
        /// <summary>
        /// Constant value for representing Red cells.
        /// </summary>
        private const string Red = "0";
        
        /// <summary>
        /// Grid height property.
        /// </summary>
        public int GridHeight { get; private set; }

        /// <summary>
        /// Grid width property.
        /// </summary>
        public int GridWidth { get; private set; }

        /// <summary>
        /// Number of Generations property.
        /// </summary>
        public int NumberOfGenerations { get; private set; }
        
        /// <summary>
        /// Store for all the generated generations of the game.
        /// </summary>
        public readonly List<Grid> Generations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="gridWidth">Width of the grid.</param>
        /// <param name="gridHeight">Height of the grid.</param>
        /// <param name="numberOfGenerations">The number of generations</param>
        /// <param name="initialGrid">Initial grid (Generation Zero)</param>
        public Game(int gridWidth, int gridHeight, int numberOfGenerations, Grid initialGrid)
        {
            this.GridWidth = gridWidth;
            this.GridHeight = gridHeight;
            this.NumberOfGenerations = numberOfGenerations;
            this.Generations = new List<Grid> {initialGrid};
        }

        /// <summary>
        /// This method starts the game.
        /// </summary>
        public void Start()
        {
            var grid = this.Generations[0];
            
            for (int i = 1; i <= NumberOfGenerations; i++)
            {
                var newGeneration = Grid.GenerateEmptyGrid(GridWidth, GridHeight);

                for (int rowIndex = 0; rowIndex < GridWidth; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < GridHeight; colIndex++)
                    {
                        GetNextGenerationValueForCell(rowIndex, colIndex, grid, newGeneration);
                    }
                }
                
                grid = newGeneration;
    
                Generations.Add(grid);
            }
        }

        /// <summary>
        /// This methods calculates in how many generations a specific cell with coordinates was green.
        /// </summary>
        /// <param name="coordinateX">The coordinate of the cell in the grid based on its col value.</param>
        /// <param name="coordinateY">The coordinate of the cell in the grid based on its row value.</param>
        /// <returns>The count of the calculates based on how many generations a specific cell with coordinates was green.</returns>
        public int CalculateGreenCellsInAllGenerations(int coordinateX, int coordinateY)
        {
            int count = 0;

            var validCoordinates = AreCoordinatesValid(coordinateX, coordinateY);

            if (validCoordinates)
            {
                for (int g = 0; g < this.Generations.Count; g++)
                {
                    if (Generations[g].Cells[coordinateY][coordinateX].Value.Equals(Green))
                    {
                        count++;
                    }
                }
            }
            
            return count;
        }
        
        /// <summary>
        /// Calculates the value of a specific cell for the next generation based on the previous one. 
        /// </summary>
        /// <param name="row">The position of the cell in the grid based on its row value.</param>
        /// <param name="col">The position of the cell in the grid based on its col value.</param>
        /// <param name="grid">The current generation grid</param>
        /// <param name="newGeneration">The grid template for the next generation.</param>
        private void GetNextGenerationValueForCell(int row, int col, Grid grid, Grid newGeneration)
        {
            int left = Math.Max(row - 1, 0);
            int right = Math.Min(row + 1, this.GridWidth - 1);

            int top = Math.Max(col - 1, 0);
            int bottom = Math.Min(col + 1, this.GridHeight - 1);

            int greenCellsCount = 0;

            for (int colIndex = top; colIndex <= bottom; colIndex++)
            {
                for (int rowIndex = left; rowIndex <= right; rowIndex++)
                {
                    if (colIndex == col && rowIndex == row) continue;
                    if (grid.Cells[rowIndex][colIndex].Value.Equals(Green))
                    {
                        greenCellsCount++;
                    }
                }
            }

            bool isCellRed = grid.Cells[row][col].Value == Red;

            bool isGreenCountEqualTo;
            
            if (isCellRed)
            {
                isGreenCountEqualTo = greenCellsCount == 3 || greenCellsCount == 6;
                
                newGeneration.Cells[row][col] =
                    isGreenCountEqualTo ? new Cell(Green) : new Cell(Red);
            }
            else
            {
                isGreenCountEqualTo = greenCellsCount == 2 || greenCellsCount == 3 || greenCellsCount == 6;
                
                newGeneration.Cells[row][col] = isGreenCountEqualTo
                    ? new Cell(Green)
                    : new Cell(Red);
            }
        }

        /// <summary>
        /// This method checks if given coordinates (x, y) are valid.
        /// </summary>
        /// <param name="coordinateX">The coordinate of the cell in the grid based on its col value.</param>
        /// <param name="coordinateY">The coordinate of the cell in the grid based on its row value.</param>
        /// <returns>True if the coordinates are valid</returns>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when coordinates are invalid.</exception>
        private bool AreCoordinatesValid(int coordinateX, int coordinateY)
        {
            if (coordinateY < 0 || coordinateY >= GridWidth || coordinateX < 0 || coordinateX >= GridHeight) {
                throw new IndexOutOfRangeException("The entered coordinates are out of range.");
            }

            return true;
        }
    }
}