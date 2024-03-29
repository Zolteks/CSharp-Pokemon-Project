namespace ConsoleApp1.Menu
{
    public class CharacterSelection : Menu
    {
        private string _curMessage;
        private bool _selecting;
        private Fighter _enemy;
        private Fighter _selectedCharacter;

        public delegate void UpdateStateMachine(StateMachine.State state);

        public event UpdateStateMachine? newState;

        public CharacterSelection() : base("CharacterSelection")
        {
            _curMessage = "";
            Selecting = false;
            AddButton(1, 4, "back");
            AddButton(1, 8, "Macron");
            AddButton(1, 12, "LePen");
            AddButton(1, 16, "Le Z");
            AddButton(1, 20, "validate");
            _selectedCharacter = new("temp", 10, 10);
        }

        public event Action? BackToMenu;

        public void SayHello()
        {
            Console.WriteLine("Character Selection");
        }

        public void InitSelection(ref Fighter enemy)
        {
            _curMessage = "";
            _selectedCharacter = new("temp", 10, 10);
            _enemy = enemy;
            SayHello();
            InitMenu();
            Draw();
        }

        public bool Selecting { get => _selecting; set => _selecting = value; }
        public ref Fighter Enemy { get => ref _enemy; }
        public ref Fighter SelectedCharacter { get => ref _selectedCharacter; }

        public void DrawHUD()
        {
            Console.Clear();
            Draw();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Character Selection");
            Console.WriteLine(_curMessage);
        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    PrevButton();
                    break;

                case ConsoleKey.DownArrow:
                    NextButton();
                    break;

                case ConsoleKey.Enter:
                    switch (PressButton())
                    {
                        case "back":
                            BackToMenu?.Invoke();
                            break;

                        case "Macron":
                            _curMessage = ("Selected Macron");
                            _selectedCharacter = new("Macron", 100, 20);
                            break;

                        case "LePen":
                            _curMessage = ("Selected LePen");
                            _selectedCharacter = new("LePen", 10, 70);
                            break;

                        case "Le Z":
                            _curMessage = ("Selected Le Z");
                            _selectedCharacter = new("Le Z", 999, 999);
                            break;

                        case "validate":
                            if (_selectedCharacter.Name == "temp")
                            {
                                _curMessage = "Select a character first";
                                break;
                            }
                            newState?.Invoke(StateMachine.State.COMBAT);
                            break;
                    }
                    break;
            }
        }
    }
}