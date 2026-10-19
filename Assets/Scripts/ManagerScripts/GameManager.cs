using UnityEngine;


namespace ManagerScripts
{
    public class GameManager : Manager
    {
        public enum States
        {
            Alive,
            Dead,
            WonGame

        }


        public static GameManager Instance { get; private set; }

        private States currState;
        

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                SetCurrState(States.Alive);
            }
            else
            {
                Destroy(this);
            }

        }


        protected override void Statemachine()
        {
            switch (currState)
            {
                case States.Dead:
                    print("Game Over");
                    break;
                default:
                    print("Player's alive.");
                    break;
            }
        }

        public States GetCurrState() => currState;

        public void SetCurrState(States state)
        {
            currState = state;
            
        } 
    }


}
    

