using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StateMachine
    {
        public enum State
        {
            MENU,
            MAP,
            COMBAT,
            CHARACTER_SELECT,
        }

        State _curState;

        public StateMachine()
        {
            _curState = State.MENU;
        }

        public State getState()
        {
            return _curState;
        }

        public void ChangeState(State state)
        {
            _curState = state;
        }
    }
}
