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

        public int MaxEnemyCargo;

        public int CurrNumEnemyCargo
        { 
            get => currNumEnemyCargo;
            set
            {
                currNumEnemyCargo--;

                if (currNumEnemyCargo <= 0)
                {
                    SetCurrState(States.WonGame);
                }
                
            }
        }

        
        
        private States currState;

        private int currNumEnemyCargo; 
        

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

        void Start()
        {
            CurrNumEnemyCargo = MaxEnemyCargo;
        }


        protected override void Statemachine()
        {
            switch (currState)
            {
                case States.Dead:
                    print("Game Over");
                    break;
                default:
                    break;
            }
        }

        public States GetCurrState() => currState;

        public void SetCurrState(States state)
        {
            currState = state;
            Statemachine();
            
        } 
    }


}
    

