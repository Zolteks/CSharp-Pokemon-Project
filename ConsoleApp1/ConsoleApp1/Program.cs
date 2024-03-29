// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Menu;

public class Program
{
    static void Main(string[] args)
    {
        Console.SetBufferSize(500, 500);
        bool gameRunning = true;
        int size = 20;
        //Console.SetWindowSize(size*2, size);
        Console.WriteLine("Game Started");

        StateMachine stateMachine = new();
        Menu menu = new("Main Menu");
        Map map = new(10, 10, 50, 50);
        Combat combat = new();
        CharacterSelection characterSelection = new();
        MainMenu mainMenu = new MainMenu();
        characterSelection.BackToMenu += GoBackToMenu;
        combat.CombatEnded += OnCombatEnded;
        mainMenu.newState += newState;
        combat.newState += newState;
        characterSelection.newState += newState;

        void newState(StateMachine.State state)
        {
            stateMachine.ChangeState(state);

            switch (state)
            {
                case StateMachine.State.CHARACTER_SELECT:
                    characterSelection.InitSelection(ref map.GetEnemy());
                    break;

                case StateMachine.State.MAP:
                    map.RenderMap();
                    break;

                case StateMachine.State.COMBAT:
                    combat.Init(ref characterSelection.SelectedCharacter, ref characterSelection.Enemy);
                    break;

                case StateMachine.State.MENU:
                    mainMenu.Init();
                    break;
            }
        }

        void OnCombatEnded(bool playerWon)
        {
            GoBackToMenu();

            mainMenu.CurMessage = playerWon ? "Player Won" : "Player Lost";
        }

        void GoToMap(string mapName)
        {

        }

        void GoBackToMenu()
        {
            Console.WriteLine("Menu");
            stateMachine.ChangeState(StateMachine.State.MENU);
            menu.Draw();
        }

        void UpdateGame()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                gameRunning = false;
            }

            switch (stateMachine.getState())
            {
                case StateMachine.State.MENU:

                    mainMenu.Update(keyInfo);
                    
                    break;

                case StateMachine.State.MAP:

                    map.Update(keyInfo);

                    break;

                case StateMachine.State.CHARACTER_SELECT:

                    characterSelection.Update(keyInfo);

                    break;

                case StateMachine.State.COMBAT:

                    combat.Update(keyInfo);

                    break;
            }
        }

        void DrawGame()
        {
            switch (stateMachine.getState())
            {
                case StateMachine.State.MENU:

                    mainMenu.Draw();

                    break;

                case StateMachine.State.MAP:

                    break;

                case StateMachine.State.CHARACTER_SELECT:

                    characterSelection.DrawHUD();

                    break;

                case StateMachine.State.COMBAT:

                    combat.Draw();

                    break;
            }
        }

        while (gameRunning)
        {
            UpdateGame();
            DrawGame();
        }
    }
}


/*class Player
{

    int _health;

    int Health { get => _health; set => _health = value; }
    // int Health { get; set; } works fine as well

    event Action OnTakeDamage;

    int[] array = new int[10];

    List<int> list = new(20);

    Dictionary<string, int> dict;

    public void takeDamage(int damage)
    {
        if (damage < 0) { throw new ArgumentException("problème", "valeur problématique") }
        if (OnTakeDamage != null) OnTakeDamage.Invoke();
        OnTakeDamage?.Invoke();
    }

}*/
// properties;
// event Action OnTakeDamage;
// throw and try catch