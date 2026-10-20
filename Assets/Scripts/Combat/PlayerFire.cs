using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Movement;


namespace Combat
{
    public class PlayerFire : MonoBehaviour
    {

        public static Action StopFiring;
        
        [SerializeField] private GameObject torpedoObj;
        
        private Queue<GameObject> fireQueue;

        private const int MAXTORPEDOS = 4;

        private readonly float currPlayerOffset = 1f;

        private Vector2 currPlayerPos;

       

        void Awake()
        {
            if (torpedoObj == null)
            {
                Debug.LogWarning("PlayerFire: No torpedo object found.");
            }
            
            fireQueue = new Queue<GameObject>();

            for (int i = 0; i < MAXTORPEDOS; i++)
            {
                fireQueue.Enqueue(torpedoObj);
            }
        }


        void OnEnable()
        {
            PlayerMovement.PlayerOnMove += updatePlayerPosition;
        }

        void OnDisable()
        {
            PlayerMovement.PlayerOnMove -= updatePlayerPosition;
        }

        private void updatePlayerPosition(Vector2 input)
        {
            currPlayerPos = input;

            currPlayerPos.x += currPlayerOffset;
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                shootTropedos();
            }
        }

        private void shootTropedos()
        {

            if (fireQueue.Count > 0)
            {
                Instantiate(fireQueue.Peek(), currPlayerPos, transform.rotation);
                fireQueue.Dequeue();
            }
            else
            {
                StopFiring?.Invoke();

                for (int i = 0; i < MAXTORPEDOS; i++)
                {
                    fireQueue.Enqueue(torpedoObj);
                }
                
            }
        }
        

    }

}