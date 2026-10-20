using System;
using System.Numerics;
using AI.Health;
using UnityEngine;
using ScriptableObjects;
using Vector2 = UnityEngine.Vector2;

namespace Combat
{
    public class Torpedo : MonoBehaviour
    {
        
        [Header("Torpedo's Info")]
        [Space(10)]
        [SerializeField] private PlayerData playerData;

        private float damage;
        
        private Rigidbody2D rB;

        private Vector2 currPos;
        
        

        void Awake()
        {
            if (playerData == null)
            {
                Debug.LogWarning("No player data assigned!");
            }
            else
            {
                damage = playerData.TorpedoDmg;
            }
            
            rB ??= GetComponent<Rigidbody2D>();
        
        }

        void Start()
        {
            //rB.position = transform.position;
        }

        private void OnEnable()
        {
            PlayerFire.StopFiring += onDestroy;
        }

        void OnDisable()
        {
            PlayerFire.StopFiring -= onDestroy;
        }

        void FixedUpdate()
        {
            
            rB.position += Vector2.one;
           
        }


        private void onDestroy()
        {
            Destroy(this.gameObject);
        }
       

        private void OnCollisionEnter2D(Collision2D other)
        {
            var layerMask = other.gameObject.layer;

            if (layerMask == 8 || layerMask == 9) // Enemy cargo ship
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageShip(damage);
            }
            else
            {
                Destroy(this.gameObject);
            }
            
            
        }
    }
}