
using System;
using AI.Health;
using UnityEngine;
using ManagerScripts.Spawning;


namespace ManagerScripts
{
    [RequireComponent(typeof(SubHunterSpawner))]
    public class GameManager : Manager
    {
        
        public enum States
        {
            Alive,
            Dead,
            WonGame
        }
        
        public static Action OnGameWon;

        public static Func<Vector2> OnPlayerSpawnPosRequest;
        public static Action<Vector2?> OnSpawnEnemies;
        
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

        private delegate void initializer();
        private initializer init;
        
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

            init += checkUIGameOverDoc;
            init += callingEntitySpawners;
            
         

        }


        void Start()
        {
            init.Invoke();
        }

        private void checkUIGameOverDoc()
        {
            if (endGameUIDoc == null)
            {
                Debug.LogWarning("UI document is null.");
            }
            else
            {
                endGameUIDoc.SetActive(false);
            }
        }

        private void callingEntitySpawners()
        {
            var playerPos=OnPlayerSpawnPosRequest?.Invoke();

            if (playerPos != null)
            {
                OnSpawnEnemies?.Invoke(playerPos);
            }
            print(playerPos ==null);
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
    

