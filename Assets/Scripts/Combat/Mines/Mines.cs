using UnityEngine;
using ScriptableObjects;
using ManagerScripts;

namespace Combat.Mines
{
    public class Mines : MonoBehaviour
    {

        [Header("Mine's Scriptable Object")] 
        [Space(10)] 
        [SerializeField] private EnemyData enemyData;

        [Header("Mine Timer's offset")] 
        [Space(10)]
        [Range(1, 100)]
        [SerializeField] private float timerOffset;

        private float timer;


        void OnEnable()
        {
            timer = Time.time + timerOffset;
        }

        void Update()
        {
            if (Time.time >= timer)
            {
                deactivateMine();
            }    
            
        }
        
        #region CollisionDetection

        void OnCollisionEnter2D(Collision2D collision)
        {
            var layerMask = collision.gameObject.layer;

            print(layerMask);
            if (layerMask == 7)
            {
                print("I got dead banged."); // Reference to 1994 The Crow.
                Destroy(collision.gameObject);

                deactivateMine();
               GameManager.Instance.SetCurrState(GameManager.States.Dead);
            }
            
        }
        #endregion

        #region DeactivateMine
        private void deactivateMine() => gameObject.SetActive(false);
        
        #endregion

    }

}