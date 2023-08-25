using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    class Weapon
    {
        private string name;
        private string appearance;


        public Weapon(string name, string appearance)
        {
            this.name = name;
            this.appearance = appearance;
        }

        public string Name { get => name; set => name = value; }
        public string Appearance { get => appearance; set => appearance = value; }

        public virtual void Present()
        {
            Console.Write(name);
        }
    }
}
