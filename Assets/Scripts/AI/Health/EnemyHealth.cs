using System;
using ManagerScripts;
using UnityEngine;
using ScriptableObjects;


namespace AI.Health
{
    /// <summary>
    /// Serves the basis of all enemy's health. Also, it's the health of the subhunters.
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        
        [Header("Enemy's Scriptable object")] [Space(10)]
        [SerializeField] private EnemyData enemyData;
        
        private float currHealth;


        void Awake()
        {

            if (enemyData == null)
            {
                Debug.LogWarning("Enemy data in enemy's health is null.");
            }
            else
            {
                currHealth = enemyData.Health;
            }
            
        }


        /// <summary>
        /// Ship takes damage when torpedo hits and sets ship to
        /// inactive when destroyed.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void DamageShip(float damage)
        {
            currHealth -= damage;
            
            if (currHealth <= 0)
            {
               this.gameObject.SetActive(false);
                
            }
        }
        
        


    }

}