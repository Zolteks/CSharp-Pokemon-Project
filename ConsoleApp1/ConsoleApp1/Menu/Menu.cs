namespace ConsoleApp1.Menu
{
    public class Menu
    {
        private string _name;
        private List<int> _displayObjects;
        private List<Button> _buttons;
        private int _curButtonIndex;

        public Menu(string name)
        {
            _name = name;
            _curButtonIndex = 0;
            _buttons = new List<Button>();
            _displayObjects = new List<int>();
        }

        public void Reset()
        {
        }

        public void Draw()
        {
            foreach (Button button in _buttons)
            {
                button.Draw();
            }
        }

        public void NextButton()
        {
            if (_buttons.Count <= 0) return;

            _buttons[_curButtonIndex].Unselect();
            _curButtonIndex++;
            if (_curButtonIndex >= _buttons.Count)
            {
                _curButtonIndex = 0;
            }
            _buttons[_curButtonIndex].Selected();
        }

        public void PrevButton()
        {
            if (_buttons.Count <= 0) return;

            _buttons[_curButtonIndex].Unselect();
            _curButtonIndex--;
            if (_curButtonIndex < 0)
            {
                _curButtonIndex = _buttons.Count - 1;
            }
            _buttons[_curButtonIndex].Selected();
        }

        public void AddButton(int x, int y, string name)
        {
            _buttons.Add(new Button(x, y, buttonText: name));
        }

        public void InitMenu()
        {
            foreach (Button button in _buttons)
            {
                button?.Unselect();
            }

            if (_buttons.Count > 0)
            {
                _buttons[0].Selected();
                _curButtonIndex = 0;
            }
        }

        public string PressButton()
        {
            return _buttons[_curButtonIndex].GetName();
        }
    }
}