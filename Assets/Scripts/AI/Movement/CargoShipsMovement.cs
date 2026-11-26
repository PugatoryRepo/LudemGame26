using System;
using ManagerScripts;
using UnityEngine;
using ScriptableObjects;
using Random = UnityEngine.Random;
using Movement;

namespace AI.Movement
{
    /// <summary>
    /// Determines the movement of the cargo ship.
    /// </summary>
    public class CargoShipsMovement : MonoBehaviour, IEntityMovement
    {
        public static Vector2 evacPoint;
        public static Action OnCargoAlive;
        
        [Header("Movement for Cargo Ships")]
        [Space(10)]
        [SerializeField] private EnemyData enemyData;

        [Space(10)] 
        [SerializeField] private Rigidbody2D rB;


        public static event Func<float[]> OnGetTilemapSize;
        
        private float speed;
        private float rotSpeed;
        
        //Initializes data for script.
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
            
            if (rB.gravityScale != 0f)
            {
                rB.gravityScale = 0f;
            }

        }
        

        // Gets the data of the tile map's size.
        void Start()
        {
            var result = OnGetTilemapSize?.Invoke();
            
            if (result != null)
            {
                evacPoint = new Vector2(result[0], result[1]);
            }
            
        }


        void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// Moves the cargo ships towards the assigned destination.
        /// </summary>
        public void Move()
        {
            var des = Vector2.MoveTowards(rB.position, evacPoint,
                speed * Time.deltaTime);
            rB.MovePosition(des);

            if (rB.position.x >= evacPoint.x && rB.position.y >= evacPoint.y)
            {
                ManagerScripts.GameManager.Instance.CargoShipReachedObj();
            }
        }

        /// <summary>
        /// Cargo ship's rotation.
        /// </summary>
        public void Rotate()
        {
        }

        /// <summary>
        /// Getter method of the cargo ship's positional speed.
        /// </summary>
        /// <returns>A float as the speed of cargo ship.</returns>
        public float GetSpeed() => speed;
        
        /// <summary>
        /// Getter method of the cargo ship's rotational speed.
        /// </summary>
        /// <returns>A float as the rotational speed of cargo ship.</returns>
        public float GetRotSpeed() => rotSpeed;
        
        

    }
}
