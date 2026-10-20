using UnityEngine;

namespace Combat
{
    public class MineLaying : MonoBehaviour
    {
    
        [SerializeField] private GameObject mineObj;
        
        
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks> Based on video: https://www.youtube.com/shorts/Cpg3B0gVWB8 </remarks>
        void Update()
        {

            if (Time.time > nextFireTime)
            {
                limitFireProperty++;
                nextFireTime = Time.time + 5f;
            }
            
        }
        
        
        
        
    }
    
}
