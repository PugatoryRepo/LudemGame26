using System;
using UnityEngine;

namespace ManagerScripts.Spawning
{
    public class PlayerSpawner: MonoBehaviour
    {
        
        public static event Action<Vector2> OnPlayerSpawn;
        
        [Header("Spawn Point For Player")]
        [SerializeField] private Vector2 playerSpawnPos;

        [Space(10)]
        [Header("Player Gameobject")]
        [SerializeField] private GameObject playerPrefab;

        void Awake()
        {
            var obj = Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
            obj.SetActive(true);
            OnPlayerSpawn?.Invoke(playerSpawnPos);
        }
        
        
    }
    
}
