using System;
using AI.Health;
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
        
        public static Action OnGameWon;
        
        public static GameManager Instance { get; private set; }

        [NonSerialized] public int MaxEnemyCargo;

        private int currCargoProperty
        { 
            get => currCargoNum;
            set
            {
                currCargoNum=value;

                if (currCargoNum <= 0)
                {
                    print(currCargoNum);
                    setStateToWon();
                }
                
            }
        }
        
        [SerializeField] private GameObject endGameUIDoc;
        
        private States currState;

        private int currCargoNum; 
        

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

            if (endGameUIDoc == null)
            {
                Debug.LogWarning("UI document is null.");
            }
            else
            {
                endGameUIDoc.SetActive(false);
            }

        }

        void OnEnable()
        {
            EnemyHealth.OnCargoShipCount += addCargoShip;
            EnemyHealth.OnCargoShipDeath += decCargoShip;
        }

        void OnDisable()
        {
            EnemyHealth.OnCargoShipCount -= addCargoShip;
            EnemyHealth.OnCargoShipDeath -= decCargoShip;
        }
        
        private void addCargoShip() => currCargoProperty++;
        
        private void decCargoShip() => currCargoProperty--;
        

        protected override void Statemachine()
        {
            switch (currState)
            {
                case States.Dead:
                    print("Game Over");
                    
                    endGameUIDoc.SetActive(true);
                    
                    break;
               case States.WonGame:
                    print("Game Won");
                    
                   endGameUIDoc.SetActive(true);
                    
                    OnGameWon?.Invoke();
                    
                   break;
                
            }
        }


        public States GetCurrState() => currState;

        public void SetCurrState(States state)
        {
            currState = state;
            Statemachine();
        }


        private void setStateToWon()
        {
            currState=States.WonGame;
            Statemachine();
        }
    }


}
    

