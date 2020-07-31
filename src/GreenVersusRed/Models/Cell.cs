namespace GreenVersusRed
{
    /// <summary>
    /// This class represents the cells in the grid.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Value property
        /// </summary>
        public string Value { get; private set; }
        
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Cell()
        {
            
        }
        
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="value">The value of the cell.</param>
        public Cell(string value)
        {
            this.Value = value;
        }
    }
}