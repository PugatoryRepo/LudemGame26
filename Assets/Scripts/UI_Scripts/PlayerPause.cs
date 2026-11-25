using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace UI_Scripts
{
    
    
    
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
        
        private void pauseGame(InputAction.CallbackContext e) => OnPause?.Invoke();
    
    }

}