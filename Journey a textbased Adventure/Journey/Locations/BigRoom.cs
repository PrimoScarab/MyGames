using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    class BigRoom : Location
    {
        enum BigRoomStatus
        {
            kallt
        }
        private BigRoomStatus status;

        public BigRoom(string name) : base(name)
        {
            this.status = BigRoomStatus.kallt;
        }

        public override void Present()
        {
            base.Present();
            Console.WriteLine("Det stora rummet är väldigt " + this.status + " även fast det finns facklor");
        }
    }
}
