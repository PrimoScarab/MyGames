using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    class Field : Location
    {
        enum FieldStatus
        {
            blåsigt
        }
        private FieldStatus status;

        public Field(string name) : base(name)
        {
            this.status = FieldStatus.blåsigt;
        }

        public override void Present()
        {
            base.Present();
            Console.WriteLine("Det är " + this.status + " på " + Name + "et och molningt");
        }
    }
}
