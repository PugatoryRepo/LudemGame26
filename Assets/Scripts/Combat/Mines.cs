using UnityEngine;
using ScriptableObjects;
using Movement;
using ManagerScripts;

namespace Combat
{
    public class Mines : MonoBehaviour
    {

        [Header("Mines")] [Space(10)] 
        [SerializeField] private EnemyData enemyData;

        

        void OnCollisionEnter2D(Collision2D collision)
        {
            var layerMask = collision.gameObject.layer;

            if (layerMask == 7)
            {
                print("I got dead banged."); // Reference to 1994 The Crow.
            }
            
        }
        


    }

}