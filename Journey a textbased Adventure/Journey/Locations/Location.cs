using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey
{
    /***
     * Basklass för platser/scener
     ***/
    class Location
    {
        private string name;
        private List<Prop> locationProps;


        public string Name { get => name; set => name = value; }

        public Location(string name)
        {
            this.name = name;
            this.locationProps = new List<Prop>();
        }

        public virtual void Present()
        {
            Console.Write("På platsen finns: ");
            foreach (Prop prop in this.locationProps)
            {
                prop.Present();
                if (prop != this.locationProps[locationProps.Count - 1])
                    Console.Write(", ");
                else
                    Console.Write("\n");
            }
        }

        public int AddProp(Prop prop)
        {
            this.locationProps.Add(prop);
            return 0;
        }

        public int RemoveProp(Prop prop)
        {
            this.locationProps.Remove(prop);
            return 0;
        }

    }
}
