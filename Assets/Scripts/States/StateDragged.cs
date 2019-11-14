using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
    public class StateDragged : State
    {
        // FIELDS

        public static StateDragged Instance = new StateDragged()
        {
            Priority = 10
        };

        // CONSTRUCTORS

        public StateDragged()
        {

        }


        // METHODS
    }
}
