using System;
using System.Collections.Generic;
using System.Text;

namespace DiceGame
{
    class DiceGame
    {
        public Enums.resultType result { get; set; }
        public int bet { get; set; }
        public int win { get; set; }
        public int pushDie { get; set; }
        public List<int> playerDice { get; set; }
        public int diceAmount { get; set; }
        public int diceType { get; set; }
        public int shoveDie { get; set; }
    }
}
