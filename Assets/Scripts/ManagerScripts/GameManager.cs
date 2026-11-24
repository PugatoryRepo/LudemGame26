
using System;
using UnityEngine;
using ManagerScripts.Spawning;


namespace ManagerScripts
{
    /// <summary>
    /// Manages the game's state.
    /// </summary>
    [RequireComponent(typeof(SubHunterSpawner))]
    [RequireComponent(typeof(CargoShipSpawner))]
    public class GameManager : Manager
    {
        
        //States:
        public enum States
        {
            Alive,
            Dead,
            WonGame
        }
        
        public static Action OnGameWon;

        //Used to set game up. 
        public static Func<Vector2> OnPlayerSpawnPosRequest;
        public static Action<Vector2?> OnSpawnEnemies;
        
        public static GameManager Instance { get; private set; }
        
        
        [SerializeField] private int thresholdWinning;
        [SerializeField] private GameObject endGameUIDoc;
        
        private int numCSInObjective; // CS means Cargo Ships.
        private int numCargoShipDestroyed;
        
        
        private States currState;

        private int currCargoShips;
        
        //Used as initializer.
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
        
        /// <summary>
        /// Checks the UI Document that shows the game is over.
        /// </summary>
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

        /// <summary>
        /// Calls entity spawners (especially for enemy ships).
        /// </summary>
        void Start()
        {
            callingEntitySpawners();
        }
        
        /// <summary>
        /// Gets player position to get the enemy spawners.
        /// </summary>
        private void callingEntitySpawners()
        {
            var playerPos=OnPlayerSpawnPosRequest?.Invoke();

            if (playerPos != null)
            {
                OnSpawnEnemies?.Invoke(playerPos);
            }

        }

        /// <summary>
        /// Checks if the cargo ships arrived to their destination in order to
        /// determine win condition.
        /// </summary>
        public void CargoShipReachedObj()
        {
            numCSInObjective++;

            if (numCSInObjective >= thresholdWinning)
            {
                print("Game Over");
                SetCurrState(States.Dead);
            }
        }

        /// <summary>
        /// Checks if player won via shooting down all cargo ships.
        /// </summary>
        public void LostCargoShip()
        {
            numCargoShipDestroyed++;

            if (numCargoShipDestroyed >= thresholdWinning)
            { 
                setStateToWon();
            }
            
        }
        
        /// <summary>
        /// Game's state machine.
        /// </summary>
        protected override void stateMachine()
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


        /// <summary>
        /// Getter method for current state of game.
        /// </summary>
        /// <returns>An enum of type States.</returns>
        public States GetCurrState() => currState;

        /// <summary>
        /// Sets the current state which calls method <c>stateMachine<\c>. Used for other methods
        /// to call for things such as if player is pausing.
        /// </summary>
        /// <param name="state"></param>
        /// <remarks>Used for other methods to call for things such as if player is pausing.</remarks>
        public void SetCurrState(States state)
        {
            currState = state;
            stateMachine();
        }

        /// <summary>
        /// Sets the current state of game and calls method <c>stateMachine<\c>.
        /// </summary>
        /// <remarks> Used only within this class as it only knows when the player wins or loses.</remarks>
        private void setStateToWon()
        {
            currState=States.WonGame;
            stateMachine();
        }
    }


}
    

