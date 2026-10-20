using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Combat
{
    public class PlayerFire : MonoBehaviour
    {

        public static Action StopFiring;
        
        [SerializeField] private GameObject torpedoObj;

        private readonly int limitFireMax = 4;

        private int limitFireProperty
        {
            get => limitFire;
            set
            {
                limitFire=value;
                if (limitFire > limitFireMax)
                {
                    StopFiring?.Invoke();
                    limitFire = 0;
                }
            }
        }
        private int limitFire;

        void Awake()
        {
            if (torpedoObj == null)
            {
                Debug.LogWarning("PlayerFire: No torpedo object found.");
            }

            limitFireProperty = 0;
            
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Instantiate(torpedoObj, transform.position, transform.rotation);
               
            }
            else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                limitFireProperty++;
            }
            
         
            
            
        }


    }

}