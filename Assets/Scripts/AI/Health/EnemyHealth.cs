using ManagerScripts;
using UnityEngine;
using ScriptableObjects;


namespace AI.Health
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Enemy's Scriptable object")] [Space(10)]
        [SerializeField] private EnemyData enemyData;

        private int layerMaskVal;
        
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
            
            layerMaskVal = gameObject.layer;
            
            
        }


        public void DamageShip(float damage)
        {
            currHealth -= damage;

            if (currHealth <= 0)
            {
                enabled=false;

                if (layerMaskVal == 8)
                {
                    GameManager.Instance.CurrNumEnemyCargo--;
                }
            }
        }
        
        


    }

}