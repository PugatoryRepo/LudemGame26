using System;
using UnityEngine;
using ScriptableObjects;
using Movement;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Combat.Mines;


namespace AI.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(MineLaying))]
    public class SubHunterMovement : MonoBehaviour, IEntityMovement
    {
        public Action<Vector2, Quaternion> OnSubHunterMove;
        
        [Header("Movement for Cargo Ships")]
        [Space(10)]
        [SerializeField] private EnemyData enemyData;

        [Space(10)] 
        [Header("Rigidbody for movement")]
        [SerializeField] private Rigidbody2D rB;

        [Space(10)] 
        [Header("Locations to go towards")] 
        [SerializeField]
        [Range(1f, 5f)]
        private int evacPointsLen;
        
        [Space(10)]
        [Range(1, 100)]
        [SerializeField] private float disTowardsDes;

        [Space(10)] 
        [Range(1, 100)]
        [SerializeField] private float nextPosTimer;
        [Space(10)]
        
        private float speed;
        private float rotSpeed;
        private float maxDisDel;
        

        private Vector2 evacPoint;
        private Vector2[] evacPoints;

        private int vecIndex;
        private Vector2 currPos;
        
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


            initEvacPoints();
        }

        private void initEvacPoints()
        {
            evacPoints = new Vector2[evacPointsLen];
            generateCoords();
        }

        void FixedUpdate()
        {
            Move();
        }
        

        public void Move()
        {
            maxDisDel = enemyData.SubHunterSpeed * Time.fixedDeltaTime;
            
            if (vecIndex >= evacPoints.Length)
            {
                generateCoords();
                vecIndex = 0;
            }
            
            
            currPos = rB.position;
            var des=Vector2.MoveTowards(currPos, evacPoints[vecIndex], maxDisDel);
            
            
            if (areVecsEqual(des))
            {
                
               StartCoroutine(timerToLayMine());
               StartCoroutine(timerForNextPos());
            }
            else
            {
                rB.MovePosition(des);
            }
        }

        private bool areVecsEqual(Vector2 a)
        {

            Vector2 offset = a - currPos;
            
            offset.x = Mathf.Abs(offset.x);
            offset.y = Mathf.Abs(offset.y);
            
            return offset == Vector2.zero;
        }


        private async Awaitable timerToLayMine()
        {
            await Awaitable.WaitForSecondsAsync(nextPosTimer/100f);
            callSubHunterEvent();
        }
        
        private async Awaitable timerForNextPos()
        {
           await Awaitable.WaitForSecondsAsync(nextPosTimer);
           
            vecIndex++; 
        }
        

        private void generateCoords()
        {
            Vector2 vec;
            bool isEntity;
            bool isInMap; 
            
            for (int i = 0; i < evacPointsLen; i++)
            {
                vec.x = Random.Range(0, 50);
                vec.y = Random.Range(0, 50);
                

                var hit = Physics2D.Raycast(currPos, vec.normalized, 
                    distance: disTowardsDes);

                if (hit)
                {
                    bool isNotBoundary = hit.collider.gameObject.layer != 6;
                    bool isFriendly = (hit.collider.gameObject.layer == 8 
                                       || hit.collider.gameObject.layer == 9) ;
                    isInMap = hit.collider.gameObject.layer == 12;
                    isEntity = !isFriendly && isNotBoundary;
                    
                    if (isInMap && isEntity)
                    {
                        evacPoints[i] = vec;
                    }
                    else if (!isInMap && isEntity)
                    {
                        print("divided");
                        vec /= 2;
                        evacPoints[i]= vec;
                    }
                    
                }
                
            }
        }
        
        
        private void callSubHunterEvent () => OnSubHunterMove?.Invoke(transform.position, transform.rotation);
        
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