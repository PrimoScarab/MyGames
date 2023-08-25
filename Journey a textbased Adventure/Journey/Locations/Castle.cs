using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    class Castle : Location
    {
        enum CastleStatus
        {
            kusligt
        }
        private CastleStatus status;

        public Castle(string name) : base(name)
        {
            this.status = CastleStatus.kusligt;
        }

        public override void Present()
        {
            base.Present();
            Console.WriteLine(Name + " känns väldigt " + this.status + ".");
        }
    }
}
