using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Move { }
enum Attacks
{

}

namespace ConsoleApp1
{
    public class Fighter
    {
        // Fields
        Stats _stats;
        string _name;
        int _health;
        int _mana;
        int _level;
        List<Move> _knownAttacks;

        public int Mana { get => _mana; set => _mana = value; }
        public string Name { get => _name; set => _name = value; }

        public Fighter(string name, int mana, int damage)
        {
            // setup base stats
            _stats.baseMaxMana = mana;
            _stats.baseAttack = damage;

            _name = name;
            _health = _stats.baseMaxHealth;
            _mana = _stats.baseMaxMana;
            _knownAttacks = new List<Move>();
        }

        public int GetMana()
        {
            return _mana;
        }
    }
}
