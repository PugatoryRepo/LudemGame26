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
        [Header("Movement for player")]
        [SerializeField] private Rigidbody2D rB;
        
        [SerializeField] private PlayerData playerData;
        
        private Vector2 currPosition;
        
        private float speed;
        private float rotSpeed;

        private Vector2 dir;
        private Vector3 mousePos;
        
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
                rotSpeed = playerData.RotSpeed;
            }
            
            //Initial rigidbody and some of its fields.
            rB ??= GetComponent<Rigidbody2D>();
            rB.gravityScale = 0f; //Default to zero to not make player fall off-screen.
            
        }


        //Get non-physics related stuff (e.g. player's input).
        void Update()
        {
            dir = InputSystem.actions["Move"].ReadValue<Vector2>();
            mousePos = Mouse.current.position.ReadValue();
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
            
            rB.position += (-dir) * (speed * Time.deltaTime);
        }
        
        
        /// <summary>
        /// Rotates player based on player's mouse position.
        /// </summary>
        public void Rotate()
        {
            if(mousePos == Vector3.zero)
            {
                return;
            }

            mousePos *= rotSpeed * Time.fixedDeltaTime;
            
            //print(mousePos);
            
            transform.up += mousePos.normalized;

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
        public float GetRotSpeed() => rotSpeed;
    }
}
