using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using ScriptableObjects;

namespace Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(TorpedoCollider))]
    [RequireComponent(typeof(TorpedoTimer))]
    public class TorpedoMovement : MonoBehaviour
    {
        
        [Header("Torpedo's Movement Info")]
        [Space(10)]
        [SerializeField] private PlayerData playerData;
        
        private Rigidbody2D rB;

        private float torpedoSpeed;

        private Vector2 torpedoPos;

        private float maxDelDis;

        void Awake()
        {

            if (playerData == null)
            {
                Debug.LogWarning("No playerData assigned!");
            }
            else
            {
                torpedoSpeed = playerData.TorpedoSpeed;
            }
        
            rB ??= GetComponent<Rigidbody2D>();
            rB.gravityScale = (rB.gravityScale == 0) ? rB.gravityScale : 0f;
        }

        void OnEnable()
        {
            torpedoPos = transform.position;
        }
        
        
        void FixedUpdate()
        {
            movement();
        }

        private void movement()
        {
            
            maxDelDis = Time.fixedDeltaTime * torpedoSpeed;

            var right = (Vector2)transform.right;
            
            torpedoPos += right * maxDelDis;
            
            rB.MovePosition(torpedoPos);
            
        }
        
    }
}