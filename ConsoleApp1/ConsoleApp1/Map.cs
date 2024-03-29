namespace ConsoleApp1
{
    public class Map
    {
        // Variables for the map's position and dimensions
        public readonly int _width;
        public readonly int _height;

        private readonly WalkableSurface _walkableSurface;

        // Variables for the player's position
        private int _mapX;
        private int _mapY;

        // Variables for the player's position on the map
        private int _playerX;
        private int _playerY;

        public Fighter _enemy;

        public delegate void UpdateStateMachine(StateMachine.State state);
        public event UpdateStateMachine? newState;

        public delegate void ZemmourCell();
        public event ZemmourCell? OnZemmourCell;

        // Constructor
        public Map(int x, int y, int width, int height)
        {
            // Initialize map position and dimensions
            _mapX = x;
            _mapY = y;
            _playerX = 0;
            _playerY = 0;
            _enemy = new("Vladimir Putine", 10, 10);
            _width = width;
            _height = height;
            _walkableSurface = new WalkableSurface(180);
        }

        public ref Fighter GetEnemy()
        {
            return ref _enemy;
        }

        // Load map data from a text file
        public void LoadMapData()
        {
            try
            {
                // Initialize all cells to empty
                for (int i = 0; i < _height; i++)
                {
                    for (int j = 0; j < _width; j++)
                    {
                        _walkableSurface.SetGroundMapCell(i, j, GroundCategory.Grass);
                    }
                }

                // Create stone path
                int stonePathWidth = 10; // Adjust as needed
                int stonePathStartX = (_width - stonePathWidth) / 2; // Center the path
                int stonePathEndX = stonePathStartX + stonePathWidth;
                for (int i = 0; i < _height; i++)
                {
                    for (int j = stonePathStartX; j < stonePathEndX; j++)
                    {
                        _walkableSurface.SetGroundMapCell(i, j, GroundCategory.Stone);
                    }
                }

                // Create grass around the stone path
                for (int i = 1; i < _height - 1; i++)
                {
                    for (int j = 1; j < _width - 1; j++)
                    {
                        if (j < stonePathStartX || j >= stonePathEndX)
                        {
                            _walkableSurface.SetGroundMapCell(i, j, GroundCategory.Grass);
                        }
                    }
                }

                // Place NPCs

                _walkableSurface.SetGroundMapCell(40, 40, GroundCategory.Zemmour);

                // Place fight cells
                for (int i = 0; i < 10; i++)
                {
                    // Randomly choose coordinates for each fight cell
                    Random rnd = new Random();
                    int fightCellX = rnd.Next(0, _width);
                    int fightCellY = rnd.Next(0, _height);

                    // Ensure the chosen cell is not already occupied
                    while (_walkableSurface.GetGroundMapCell(fightCellY, fightCellX) != GroundCategory.Grass)
                    {
                        fightCellX = rnd.Next(0, _width);
                        fightCellY = rnd.Next(0, _height);
                    }

                    // Place the fight cell
                    _walkableSurface.SetGroundMapCell(fightCellY, fightCellX, GroundCategory.Fight);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating map: {ex.Message}");
            }
        }


        // Render the map
        public void RenderMap()
        {
            // Set console output encoding to UTF-8 to display Unicode characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Clear the console window
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    int consoleX = _mapX + j;
                    int consoleY = _mapY + i;

                    // Check if the position is within the console window
                    if (consoleX >= 0 && consoleX < _width && consoleY >= 0 && consoleY < _height)
                    {
                        // Check the ground type at the current position
                        GroundCategory category = _walkableSurface.GetGroundMapCell(i, j);

                        // Check if the category is Fight and player is not on this position
                        if (category == GroundCategory.Fight && !(i == _playerY && j == _playerX))
                        {
                            // Render grass tile on top of fight tile
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\u2588");
                        }
                        else
                        {
                            // Set color based on ground type
                            switch (category)
                            {
                                case GroundCategory.Grass:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;

                                case GroundCategory.Stone:
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;

                                case GroundCategory.Melanchon:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;

                                case GroundCategory.Royale:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;

                                case GroundCategory.Ciotti:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;

                                case GroundCategory.Zemmour:
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    break;

                                default:
                                    // Reset color to default
                                    Console.ResetColor();
                                    break;
                            }
                        }

                        // Render the pixel
                        Console.SetCursorPosition(consoleX, consoleY);
                        Console.WriteLine("\u2588");
                    }
                    else
                    {
                        // If the position is outside the console window, display an exclamation mark
                        Console.Write("!");
                    }
                }

                Console.WriteLine();
            }

            // Reset color to default after rendering the map
            Console.ResetColor();
        }

        // Get the terrain symbol at the specified position
        private string GetTerrainSymbol(int row, int col)
        {
            // Get the terrain type at the specified position
            ConsoleColor color = ConsoleColor.White; // Default color
            switch (_walkableSurface.GetGroundMapCell(row, col))
            {
                case GroundCategory.Grass:
                    color = ConsoleColor.Green;
                    break;

                case GroundCategory.Stone:
                    color = ConsoleColor.Gray;
                    break;

                case GroundCategory.Melanchon:
                    color = ConsoleColor.DarkRed;
                    break;

                case GroundCategory.Royale:
                    color = ConsoleColor.Red;
                    break;

                case GroundCategory.Ciotti:
                    color = ConsoleColor.Blue;
                    break;

                case GroundCategory.Zemmour:
                    color = ConsoleColor.DarkBlue;
                    break;

                default:
                    break;
            }

            // Set console text color
            Console.ForegroundColor = color;

            // Return colored pixel using Unicode FULL BLOCK character
            return "\u2588";
        }

        // Move the player on the map
        public void MovePlayer(int dx, int dy)
        {
            int newX = _playerX + dx;
            int newY = _playerY + dy;

            if (newX >= 0 && newX < _width && newY >= 0 && newY < _height)
            {
                if (_walkableSurface.GetGroundMapCell(newY, newX) != GroundCategory.Fight)
                {
                    // Clear player's current position
                    Console.SetCursorPosition(_mapX + _playerX, _mapY + _playerY);
                    Console.Write(GetTerrainSymbol(_playerY, _playerX));

                    // Update player's position
                    _playerX = newX;
                    _playerY = newY;

                    // Render player's new position
                    Console.SetCursorPosition(_mapX + _playerX, _mapY + _playerY);
                    Console.Write("@");
                }
                else if (_walkableSurface.GetGroundMapCell(newY, newX) == GroundCategory.Zemmour)
                {
                    OnZemmourCell?.Invoke();
                }
                else
                {

                    newState?.Invoke(StateMachine.State.CHARACTER_SELECT);
                }
            }
        }


        // Handle player input
        public void Update(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MovePlayer(0, -1);// Move player up
                    break;

                case ConsoleKey.DownArrow:
                    MovePlayer(0, 1);// Move player down
                    break;

                case ConsoleKey.LeftArrow:
                    MovePlayer(-1, 0);// Move player left
                    break;

                case ConsoleKey.RightArrow:
                    MovePlayer(1, 0);// Move player right
                    break;

                case ConsoleKey.Escape:
                    return;

                default:
                    return;
            }
        }

        // Properties
        public WalkableSurface WalkableSurface
        {
            get { return _walkableSurface; }
        }
    }
}