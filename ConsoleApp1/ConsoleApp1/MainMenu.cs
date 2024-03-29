namespace ConsoleApp1.Menu
{
    public class MainMenu
    {
        private Menu _menu;

        public delegate void UpdateStateMachine(StateMachine.State state);

        public event UpdateStateMachine? newState;

        private string _curMessage;

        public string CurMessage { get => _curMessage; set => _curMessage = value; }

        public MainMenu()
        {
            _menu = new("Main Menu");
            _curMessage = "";
        }

        public void Init()
        {
        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.C:
                    newState?.Invoke(StateMachine.State.CHARACTER_SELECT);
                    break;

                case ConsoleKey.M:
                    newState?.Invoke(StateMachine.State.MAP);
                    break;
            }
        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine(_curMessage);
        }
    }
}