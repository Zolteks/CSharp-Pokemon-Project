namespace ConsoleApp1
{
    // Enum for the ground category
    public enum GroundCategory
    {
        Stone,
        Grass,
        Fight,
        Melanchon,
        Royale,
        Ciotti,
        Zemmour
    }

    // Class to represent the walkable surface
    public class WalkableSurface
    {
        // Private fields
        private GroundCategory[,] groundMap;

        // Constructor
        public WalkableSurface(int size)
        {
            // Initialize the ground map
            groundMap = new GroundCategory[size, size];
        }

        // Get the ground map
        public GroundCategory GetGroundMapCell(int row, int col)
        {
            // Check if the row and column are within the bounds of the ground map
            if (row >= 0 && row < groundMap.GetLength(0) && col >= 0 && col < groundMap.GetLength(1))
            {
                // Return the ground category
                return groundMap[row, col];
            }
            else
            {
                Console.WriteLine("1/Invalid cell coordinates.");
                return GroundCategory.Stone;
            }
        }

        // Set the ground map cell
        public void SetGroundMapCell(int row, int col, GroundCategory groundCategory)
        {
            // Check if the row and column are within the bounds of the ground map
            if (row >= 0 && row < groundMap.GetLength(0) && col >= 0 && col < groundMap.GetLength(1))
            {
                // Set the ground category
                groundMap[row, col] = groundCategory;
            }
            else
            {
                Console.WriteLine("2/Invalid cell coordinates.");
            }
        }

        // Load the map data from a file
        public string FightCell(int row, int col)
        {
            // Check if the row and column are within the bounds of the ground map
            if (row >= 0 && row < groundMap.GetLength(0) && col >= 0 && col < groundMap.GetLength(1))
            {
                // Check if the cell is a fight cell
                if (groundMap[row, col] == GroundCategory.Fight)
                {
                    return "You have entered a fight!";
                }
                else
                {
                    return "No fight here.";
                }
            }
            else
            {
                return "Invalid cell coordinates.";
            }
        }
    }
}