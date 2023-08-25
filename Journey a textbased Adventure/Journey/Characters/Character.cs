using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    /***
     * Basklass för karaktärer
     ***/
    class Character
    {
        private int index;
        private string name;
        private int age;
        private int height;
        private string haircolor;
        private string bodyType;
        private int fastAttack;
        private int heavyAttack;
        private int hp;

        public Character(int index, string name, int age, int height, string haircolor, string bodyType, int fastAttack, int heavyAttack, int hp)
        {
            this.index = index;
            this.name = name;
            this.age = age;
            this.height = height;
            this.haircolor = haircolor;
            this.bodyType = bodyType;
            this.fastAttack = fastAttack;
            this.heavyAttack = heavyAttack;
            this.hp = hp;
        }
        public int Index { get => index; set => index = value; }
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }

        public int Height { get => height; set => height = value; }
        public string Haircolor { get => haircolor; set => haircolor = value; }
        public string BodyType { get => bodyType; set => bodyType = value; }
        public int FastAttack { get => fastAttack; set => fastAttack = value; }
        public int HeavyAttack { get => heavyAttack; set => heavyAttack = value; }
        public int Hp { get => hp; set => hp = value; }

    }
}
