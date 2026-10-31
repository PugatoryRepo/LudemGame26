using System;
using UnityEngine;
using ScriptableObjects;
using Movement;
using AI.Movement;
using Random = System.Random;

namespace AI.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class SubHunterMovement : MonoBehaviour, IEntityMovement
    {
        public static Action<Vector2, Quaternion> OnSubHunterMove;

        
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

        }


        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            var des=Vector2.MoveTowards(rB.position, evacPoint, 
                enemyData.SubHunterSpeed * Time.deltaTime);
            
            rB.MovePosition(des);
            
            OnSubHunterMove?.Invoke(transform.position, transform.rotation);
        }

        public void Rotate()
        {
            throw  new NotImplementedException();
        }


        private void OnCollisionStay2D(Collision2D other)
        {
            print(other.collider.gameObject.layer.ToString());
            
            
        }

        public float GetSpeed() => speed;
        public float GetRotSpeed() => rotSpeed;
    }

}