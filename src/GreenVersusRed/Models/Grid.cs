namespace GreenVersusRed
{
    /// <summary>
    /// This class represents a grid.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Two dimensional Cell array
        /// </summary>
        public Cell[][] Cells { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The width of the grid.</param>
        public Grid(int width)
        {
            Cells = new Cell[width][];
        }

        /// <summary>
        /// Generates empty grid with specific width and height.
        /// </summary>
        /// <returns>
        /// New empty Grid class.
        /// </returns>
        /// <param name="gridWidth">Width of the grid.</param>
        /// <param name="gridHeight">Height of the grid.</param>
        public static Grid GenerateEmptyGrid(int gridWidth, int gridHeight)
        {
            var grid = new Grid(gridHeight);
                
            for (int i = 0; i < gridWidth; i++) 
            {
                grid.Cells[i] = new Cell[gridHeight];
            }

            return grid;
        }
    }
}