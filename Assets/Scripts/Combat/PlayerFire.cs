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
        public static Func<Vector2> OnPlayerFirePos;
        
        public static Func<Quaternion> OnPlayerFireRot;
        
        public static Action StopFiring;
        
        [SerializeField] private GameObject torpedoObj;
        
        private List<GameObject> torpedoPool;

        private const int MAXTORPEDOS = 4;

        private Vector2 torpedoPosition;

        private Quaternion torpedoRotation;

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

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
               var torpedo = getTorpedo();
               getPlayersPosAndRot();

               if (torpedo)
               {
                   fireTorpedo(torpedo);
               }
            }
        }


        private void getPlayersPosAndRot()
        {
            try
            {
                torpedoPosition = OnPlayerFirePos.Invoke();
                torpedoRotation = OnPlayerFireRot.Invoke();
                
            }
            catch (Exception e)
            {
                print(e.Message);
            }

        }

        private GameObject getTorpedo()
        {

            for (int i = 0; i < torpedoPool.Count; i++)
            {
                if (!torpedoPool[i].activeInHierarchy)
                {
                    return torpedoPool[i];
                }
            }

            return null;
        }


        private void fireTorpedo(GameObject torpedo)
        {
            torpedo.transform.rotation = torpedoRotation;
            torpedo.transform.position = torpedoPosition;
            print(torpedoPosition+", "+ torpedo.transform.position);
            
            torpedo.SetActive(true);
        }

        private Awaitable await()
        {
            return Awaitable.WaitForSecondsAsync(10f);
        }

    }

}