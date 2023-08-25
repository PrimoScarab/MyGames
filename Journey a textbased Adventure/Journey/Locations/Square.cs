using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    /***
     * En underklass till basklassen Location.
     * Används för en specifik plats.
     ***/
    class Square : Location
    {
        enum SquareStatus
        {
            livligt
        }
        private SquareStatus status;

        public Square(string name) : base(name)
        {
            this.status = SquareStatus.livligt;
        }

        public override void Present()
        {
            base.Present();
            Console.WriteLine("Det är " + this.status + " på " + Name);
        }
    }
}
