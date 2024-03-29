public class Move
{ }

internal enum Attacks
{
}

namespace ConsoleApp1
{
    public class Fighter
    {
        // Fields
        private Stats _stats;

        private string _name;
        private int _health;
        private int _mana;
        private int _level;
        private List<Move> _knownAttacks;

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