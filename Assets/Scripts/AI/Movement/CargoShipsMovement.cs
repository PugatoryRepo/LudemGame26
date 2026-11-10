using System;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Random = System.Random;
using Movement;

namespace AI.Movement
{
    public class CargoShipsMovement : MonoBehaviour, IEntityMovement
    {
        
        [Header("Movement for Cargo Ships")]
        [Space(10)]
        [SerializeField] private EnemyData enemyData;

        [Space(10)] 
        [SerializeField] private Rigidbody2D rB;


        public static event Func<Vector2> OnMoveTowardsEvac;
        
        private float speed;
        private float rotSpeed;
        

        private Vector2 evacPoint;

        private static Random rand;
        
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
            
            rand = new Random();
        }
        private void choosingEvacPoint()
        {
           
        }
        
        

        void Start()
        {
            var result = OnMoveTowardsEvac?.Invoke();

            if (result != null)
            {
                evacPoint = result.Value;
            }
            
        }


        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            var des=Vector2.MoveTowards(rB.position, evacPoint, 
                speed * Time.deltaTime);
            rB.MovePosition(des);
        }

        public void Rotate()
        {
        }

        public float GetSpeed() => speed;
        
        public float GetRotSpeed() => rotSpeed;
        
        

    }
}
