using UnityEngine;
using System.Collections.Generic;

namespace Combat
{
    public class MineLaying : MonoBehaviour
    {
    
        [SerializeField] private GameObject mineObj;

        private Queue<GameObject> mineQueue;
        
        private readonly int limitFireMax = 20;

        private float nextFireTime;
        
        private int limitFireProperty
        {
            get => limitFire;
            set
            {
                limitFire=value;
                if (limitFire > limitFireMax)
                {
                    limitFire = 0;
                }
                else
                {
                    Instantiate(mineObj, transform.position, transform.rotation);
                }
            }
        }
        private int limitFire;


        void Start()
        {
            nextFireTime = Time.time+5f;
            mineQueue = new Queue<GameObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks> Based on video: https://www.youtube.com/shorts/Cpg3B0gVWB8 </remarks>
        void Update()
        {
            if (mineQueue.Count == 0)
            {
                limitFireProperty++;
            }
            
        }
        
        
        
        
    }
    
}
