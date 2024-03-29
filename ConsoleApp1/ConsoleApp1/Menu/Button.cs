using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Menu
{
    public class Button
    {
        int _x, _y, _w, _h;

        string _buttonText;
        int _textColor;
        int _boxColor;
        int _selectedColor;
        bool _isSelected;
        bool _hasBox;
        string _outline;

        public Button(int x = 1, int y = 1, int w = 1, int h = 1, int textColor=1, int boxColor=1, int selectedColor=1, bool isSelected = false, bool hasBox = true, string buttonText = "button")
        {
            _x = x;
            _y = y;
            _w = w;
            _h = h;

            _buttonText = buttonText;
            _textColor = textColor;
            _boxColor = boxColor;
            _selectedColor = selectedColor;
            _isSelected = isSelected;
            _hasBox = hasBox;

            _outline = "";
            for (int i = 0; i < _buttonText.Length; i++)
            {
                _outline += "=";
            }
        }

        public void Selected()
        {
            _isSelected = true;
        }

        public void Unselect()
        {
            _isSelected= false;
        }

        public string GetName()
        {
            return _buttonText;
        }

        public void Draw()
        {
            if (_isSelected ) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition( _x, _y );
            Console.Write("|=" + _outline + "=|");
            Console.SetCursorPosition( _x, _y+1 );
            Console.Write("| " + _buttonText + " |", Console.ForegroundColor);
            Console.SetCursorPosition( _x, _y+2 );
            Console.Write("|=" + _outline + "=|");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
