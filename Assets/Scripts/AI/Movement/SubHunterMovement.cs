using UnityEngine;
using ScriptableObjects;

namespace AI.Movement
{
    public class SubHunterMovement : MonoBehaviour
    {
        [Header("Movement for Cargo Ships")]
        [Space(10)]
        [SerializeField] private EnemyData enemyData;

        [Space(10)] 
        [SerializeField] private Rigidbody2D rB;
        
        private float speed;
        private float rotSpeed;
        

        private Vector2 evacPoint;


        void Awake()
        {
            if (enemyData == null)
            {
                Debug.LogWarning("enemy data is null.");
            }
            else
            {
                speed = enemyData.Speed;
                rotSpeed = enemyData.RotSpeed;
            }
            
            if (rB == null)
            {
                rB = GetComponent<Rigidbody2D>();
                rB.gravityScale = 0f;
            }
            

   
        }


        void Start()
        {
            rB.position = enemyData.WayPoints[1];
            
        }


        void FixedUpdate()
        {
            movement();
        }

        protected virtual void movement()
        {
            var des=Vector2.MoveTowards(rB.position, evacPoint, 
                enemyData.SubHunterSpeed * Time.deltaTime);
            
            rB.MovePosition(des);
        }
    }

}