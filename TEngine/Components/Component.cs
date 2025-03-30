using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Components
{
    public class Component
    {

        public GameObject GameObject;
        public Component()
        {

        }

        public Component GetComponent<T>()
        {
            return this;
        }
    }
}
