using UnityEngine;
using AI.Health;
using ScriptableObjects;

namespace Combat
{
    public class TorpedoCollider : MonoBehaviour
    {
            
        [Header("Torpedo's Info")]
        [Space(10)]
        [SerializeField] private PlayerData playerData;
    
        //[Space(10)]
        //[SerializeField] private LayerMask layerMask;
    
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
    
    
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageShip(damage);
            }
            
            
            gameObject.SetActive(false);
        }
    }
}
