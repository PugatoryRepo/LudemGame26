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
    
    /// <summary>
    /// Moves the subhunter.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(MineLaying))]
    public class SubHunterMovement : MonoBehaviour, IEntityMovement
    {
        public Action<Vector2, Quaternion> OnSubHunterMove;
        
        public const float kEpsilon = 0.00001F;
        
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
        private Vector2 prevPos;
        
        void Awake()
        {
            
            if (enemyData == null)
            {
                Debug.LogWarning("enemy data is null.");
            }
            else
            {
                speed = enemyData.CargoShipSpeed;
                rotSpeed = enemyData.RotSpeed;
            }
            
            rB ??= GetComponent<Rigidbody2D>();
        }


        void OnEnable()
        {
            initEvacPoints();
        }

        /// <summary>
        /// Initializes both a new position to go towards and generates
        /// new coordinates for enemy subhunters to go towards.
        /// </summary>
        private void initEvacPoints()
        {
            evacPoints = new Vector2[evacPointsLen];
            generateCoords();
        }

        void FixedUpdate()
        {
            Move();
        }
        

        /// <summary>
        /// Moves subhunters towards another coordinate.
        /// </summary>
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

            rB.MovePosition(des);
            
            if (prevPos == currPos)
            {
                StartCoroutine(timerForNextPos());
            }
            else
            {
                prevPos=currPos;
            }
        }

        private void moveNextPos(Vector2 des)
        {
            rB.MovePosition(des);
            StartCoroutine(timerForNextPos());
        }

        /// <summary>
        /// After moving into destination, it waits, and then calls event to
        /// notify all subscribers to lay mine and select next position to go towards.
        /// </summary>
        private async Awaitable timerForNextPos()
        {
    
           await Awaitable.WaitForSecondsAsync(nextPosTimer);
           vecIndex++; 
           callSubHunterEvent();
        }
        

        /// <summary>
        /// Generates new coordinates for subhunter to go towards.
        /// </summary>
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
        
        /// <summary>
        /// Calls an event, used to lay mines.
        /// </summary>
        private void callSubHunterEvent () => OnSubHunterMove?.Invoke(transform.position, transform.rotation);

        /// <summary>
        /// Rotation of the subhunter.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Rotate()
        {
            throw  new NotImplementedException();
        }
        

        /// <summary>
        /// Getter method of the subhunter ship's positional speed.
        /// </summary>
        /// <returns>A float as the speed of the subhunter.</returns>
        public float GetSpeed() => speed;
        
        /// <summary>
        /// Getter method of the subhunter ship's rotational speed.
        /// </summary>
        /// <returns>A float as the rotational speed of the subhunter.</returns>
        public float GetRotSpeed() => rotSpeed;
    }

}