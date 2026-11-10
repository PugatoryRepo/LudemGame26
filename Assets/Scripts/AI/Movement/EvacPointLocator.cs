using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.Movement
{
    
    public class EvacPointLocator
    {

        [SerializeField] private Vector2[] evacPoints;

 
        void OnEnable()
        {
            
        }

        void OnDisable()
        {
            
        }
        
        private Vector2 getEvacPoint()
        {
        
            var index = Random.Range(0, evacPoints.Length-1);
            
            
            return evacPoints[index];
        }


    }
}