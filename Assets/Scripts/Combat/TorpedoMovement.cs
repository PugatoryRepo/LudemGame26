using AI.Health;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using ScriptableObjects;

namespace Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
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

        private float dmg;

        void Awake()
        {

            if (playerData == null)
            {
                Debug.LogWarning("No playerData assigned!");
            }
            else
            {
                torpedoSpeed = playerData.TorpedoSpeed;
                dmg = playerData.TorpedoDmg;
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
        
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 8)
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageShip(dmg);
            }
            else if (other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<CargoShipHealth>().DamageShip(dmg);
            }
            
            
            gameObject.SetActive(false);
        }
        
        
    }
}