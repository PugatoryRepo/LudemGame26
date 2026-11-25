using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace UI_Scripts
{
    
    
    /// <summary>
    /// Calls pause menu when player pauses game.
    /// </summary>
    public class PlayerPause: MonoBehaviour
    {
        
        public static event Action OnPause;

        private InputAction input;

        void Start()
        {
           input = InputSystem.actions["Pause"];
           
           input.performed += pauseGame;
           
           input.canceled -= pauseGame;

        } 
        
        /// <summary>
        /// Calls pause menu.
        /// </summary>
        /// <param name="e"></param>
        private void pauseGame(InputAction.CallbackContext e) => OnPause?.Invoke();
    
    }

}