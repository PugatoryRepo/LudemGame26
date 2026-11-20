using System;
using ManagerScripts;
using UnityEngine;
using ScriptableObjects;
using Random = UnityEngine.Random;
using Movement;

namespace AI.Movement
{
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
            
            if (rB == null)
            {
                rB = GetComponent<Rigidbody2D>();
                rB.gravityScale = 0f;
            }

        }
        
        
        private void choosingEvacPoint(float[] tMapSize)
        {
            evacPoint = new Vector2(tMapSize[0], tMapSize[1]);
        }
        
        

        void Start()
        {
            var result = OnGetTilemapSize?.Invoke();
            
            if (result != null)
            {
                choosingEvacPoint(result);
            }
            
        }


        void FixedUpdate()
        {
            Move();
        }

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

        public void Rotate()
        {
        }

        public float GetSpeed() => speed;
        
        public float GetRotSpeed() => rotSpeed;
        
        

    }
}
