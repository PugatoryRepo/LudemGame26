using System;
using AI.Health;
using UnityEngine;
using ScriptableObjects;

namespace Combat
{
    public class Torpedo : MonoBehaviour
    {
        
        [Header("Torpedo's Info")]
        [Space(10)]
        [SerializeField] private PlayerData playerData;

        private float damage;


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
              enabled=false;
            }
            
            
        }
    }
}