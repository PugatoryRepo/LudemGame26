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
        
        private List<GameObject> torpedoPool;

        private const int MAXTORPEDOS = 4;

        private readonly float currPlayerOffset = 1f;

        private Vector2 torpedoPosition;

        private Quaternion torpedoRotation;

        private int index;

        void Awake()
        {
            if (torpedoObj == null)
            {
                Debug.LogWarning("PlayerFire: No torpedo object found.");
            }
            
            torpedoPool = new List<GameObject>();

            for (int i = 0; i < MAXTORPEDOS; i++)
            {
                var obj=Instantiate(torpedoObj, transform.position, transform.rotation);
                torpedoObj.SetActive(false);
                torpedoPool.Add(obj);
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

        private void updatePlayerPosition(Vector2 input, Quaternion inputRotation)
        {
            torpedoPosition = input;

            torpedoPosition.x += currPlayerOffset;
            torpedoRotation = inputRotation;
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
               var torpedo = getTorpedo();
               fireTorpedo(torpedo);
            }
        }

        private GameObject getTorpedo()
        {
            if (index >= MAXTORPEDOS)
            {
                index = 0;
            }
            print(index);
            var torpedo = torpedoPool[index];
            index++;

            return torpedo;
        }


        private void fireTorpedo(GameObject torpedo)
        {
            torpedo.transform.rotation = torpedoRotation;
            torpedo.transform.position = torpedoPosition;
            
            torpedo.SetActive(true);
        }

        private Awaitable await()
        {
            return Awaitable.WaitForSecondsAsync(10f);
        }

    }

}