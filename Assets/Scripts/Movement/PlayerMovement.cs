using UnityEngine;
using ScriptableObjects;
using UnityEngine.InputSystem;
using System;


namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour, IEntityMovement
    {
        
        public static event Action<Vector2, Quaternion> PlayerOnMove;
        
        /// <summary>
        /// Property used when modifying the player's position and calling the event if
        /// player is on a mine.
        /// </summary>
        /// <remarks> Can also get player's current position; this part of property is public.</remarks>
        public Vector2 Playerposition
        {
            get => currPosition;
            private set
            {
                currPosition = value;
                PlayerOnMove?.Invoke(currPosition, this.transform.rotation);
            }
        }

        [Space(10)]
        [Header("Movement info for player")]
        [SerializeField] private PlayerData playerData;

        [Space(10)]
        [SerializeField] private Camera cam;
        
        private Vector2 currPosition;
        
        private Rigidbody2D rB;
        
        private float speed;
        private float rotAngle;

        private Vector2 dir;
        private Vector2 turnDir;

        private float maxDegDel;
        private float speedRate;
        
        //Initial variables
        void Awake()
        {
            if (playerData == null)
            {
                Debug.LogWarning("PlayerData is null");
            }

            else
            {
                speed = playerData.Speed;
                rotAngle = playerData.RotSpeed;
            }
            
            //Initial rigidbody and some of its fields.
            rB ??= GetComponent<Rigidbody2D>();
            rB.gravityScale = 0f; //Default to zero to not make player fall off-screen.
            
        }


        //Get non-physics related stuff (e.g. player's input).
        void Update()
        {
            dir = InputSystem.actions["Move"].ReadValue<Vector2>();
            turnDir= InputSystem.actions["Look"].ReadValue<Vector2>();
            
            print(turnDir);
        }


        void FixedUpdate()
        {
            
            Playerposition = transform.position;
            
            Move();
            
            Rotate();
        }
    
        /// <summary>
        /// Moves player in x and y planes (i.e. in 2D).
        /// </summary>
        public void Move()
        {
            if (dir == Vector2.zero)
            {
                return;
            }
            speedRate = speed * Time.deltaTime;
            
            dir *= speedRate;
            
            rB.MovePosition(rB.position+(-dir));
        }
        
        
        /// <summary>
        /// Rotates player based on player's mouse position.
        /// </summary>
        public void Rotate()
        {
            if(turnDir == Vector2.zero)
            {
                return;
            }

            maxDegDel = rotAngle * Time.deltaTime;
            if (turnDir.x > 0f || turnDir.y > 0f)
            {
                transform.Rotate(Vector3.back, maxDegDel);
            }
            
            if (turnDir.x < 0f || turnDir.y < 0f)
            {
                transform.Rotate(Vector3.forward, maxDegDel);
            }
            
           
          
        }



        /// <summary>
        /// Get method for speed.
        /// </summary>
        /// <returns>A float representing speed.</returns>
        public float GetSpeed() => speed;
        
        /// <summary>
        /// Get method for rotation speed.
        /// </summary>
        /// <returns>A float representing the speed of rotation.</returns>
        public float GetRotSpeed() => rotAngle;
    }
}
