using System;
using System.Numerics;
using AI.Health;
using Movement;
using UnityEngine;
using ScriptableObjects;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Combat
{
    public class Torpedo : MonoBehaviour
    {
        
        [Header("Torpedo's Info")]
        [Space(10)]
        [SerializeField] private PlayerData playerData;
        
        [Space(10)]
        [SerializeField] private LayerMask layerMask;

        private float damage;
        
        private Rigidbody2D rB;
        
        private Vector3 positionToFire;

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


      

        void FixedUpdate()
        {
            transform.position += transform.up *( Time.fixedDeltaTime *playerData.TorpedoSpeed);

        }


        private void disableTorpedos()
        {
            enabled = false;
        }
       

        void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageShip(damage);
                enabled = false;
            }
            
        }
    }
}