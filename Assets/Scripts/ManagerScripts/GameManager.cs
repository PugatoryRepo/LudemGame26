
using System;
using System.Collections.Generic;
using AI.Health;
using AI.Movement;
using UnityEngine;
using ManagerScripts.Spawning;


namespace ManagerScripts
{
    [RequireComponent(typeof(SubHunterSpawner))]
    [RequireComponent(typeof(CargoShipSpawner))]
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
        
        
        [SerializeField] private int thresholdWinning;
        
        private int numCSInObjective; // CS means Cargo Ships.
        private int numCargoShipDestroyed;

        
        
        [SerializeField] private GameObject endGameUIDoc;
        
        private States currState;

        private int currCargoShips;
        
        void Awake()
        {
        
            //Checking Singleton for other references of self.
            if (Instance == null)
            {
                Instance = this;
                SetCurrState(States.Alive);
            }
            else
            {
                Destroy(this);
            }

            checkUIGameOverDoc();
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

        void Start()
        {
            callingEntitySpawners();
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

        public void CargoShipReachedObj()
        {
            numCSInObjective++;

            if (numCSInObjective >= thresholdWinning)
            {
                print("Game Over");
                setStateToWon();
            }
        }

        public void DecCargoShip()
        {
            numCargoShipDestroyed++;

            if (numCargoShipDestroyed >= thresholdWinning)
            {
                SetCurrState(States.Dead);
            }
            
        }
        

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
    

