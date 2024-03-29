using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Menu
{
    public class Combat
    {
        string _curMessage;

        public Menu _fightingMenu;
        Fighter _player;
        Fighter _curEnemy;
        Random _random;

        public delegate void UpdateStateMachine(StateMachine.State state);
        public event UpdateStateMachine? newState;

        bool _isPlayerTurn;
        int _curTurn;
        int _debateScore;

        public delegate void FighterResult(bool playerWon);
        public event FighterResult? CombatEnded;

        public Combat()
        {
            _curMessage = "";
            _fightingMenu = new Menu("Fighting");
            _random = new Random();
            _debateScore = 0;
            _isPlayerTurn = true;

            _fightingMenu.AddButton(1, 4, "Attack 1");
            _fightingMenu.AddButton(1, 8, "Attack 2");
            _fightingMenu.AddButton(1, 12, "Article 49.3");
        }

        public void SayHello()
        {
            
        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            UpdatePlayer(keyInfo);
            _fightingMenu.Draw();
            CheckVictory();

            if (!_isPlayerTurn && !(_debateScore <= -100 || _debateScore >= 100))
            {
                UpdateEnemy();
                CheckVictory();
            }

        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Combat");
            Console.WriteLine(_curMessage);
            Console.WriteLine($"Debate score is {_debateScore}");
            _fightingMenu.Draw();
        }

        private void UpdatePlayer(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    _fightingMenu.PrevButton();
                    break;

                case ConsoleKey.DownArrow:
                    _fightingMenu.NextButton();
                    break;

                case ConsoleKey.Enter:
                    switch (_fightingMenu.PressButton())
                    {
                        case "Attack 1":
                            _debateScore += 20;
                            _isPlayerTurn = false;

                            _curMessage = "Used attack 1";
                            break;

                        case "Attack 2":
                            _debateScore += 50;
                            _isPlayerTurn = false;
                            _curMessage = "Used attack 2";
                            break;

                        case "Article 49.3":
                            _debateScore = 100;
                            _isPlayerTurn = false;
                            break;
                    }
                    break;
            }
        }

        private void UpdateEnemy()
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(50, 15);

            switch (_random.Next(3))
            {
                case 0:
                    Console.Write("The enemy used a classic argument");
                    _debateScore -= 50;
                    break;
                case 1:
                    Console.Write("The enemy made a mistake");
                    _debateScore += 20;
                    break;
                case 2:
                    Console.Write("The enemy used a STRONG argument");
                    _debateScore -= 70;
                    break;
            }

            Thread.Sleep(2000);

            _curMessage = "Your turn !";
            _isPlayerTurn = true;
        }

        private void CheckVictory()
        {
            if (_debateScore <= -100)
            {
                CombatEnded?.Invoke(false);
            }
            else if (_debateScore >= 100)
            {
                CombatEnded?.Invoke(true);
            }
        }

        public void Init(ref Fighter player, ref Fighter enemy)
        {
            _debateScore = 0;
            _isPlayerTurn = true;
            _curMessage = "Your turn !";
            _fightingMenu.InitMenu();
            _player = player;
            _curEnemy = enemy;
        }
    }
}
