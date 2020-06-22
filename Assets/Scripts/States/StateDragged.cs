using Assets.Scripts.States;

namespace States
{
    public class StateDragged : State
    {
        // FIELDS

        public static readonly StateDragged Instance = new StateDragged()
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
