using System;
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
        
        private float speed;
        private float rotSpeed;
        

        private Vector2 evacPoint;

        private static Random rand;


        private void choosingEvacPoint()
        {
            int len = enemyData.EvacPoints.Length;
            
            var randNum=rand.Next(0, len-1);
            
            evacPoint = enemyData.EvacPoints[randNum];
        }
        
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
            choosingEvacPoint();
        }


        void Start()
        {
            rB.position = enemyData.WayPoints[0];
            
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
