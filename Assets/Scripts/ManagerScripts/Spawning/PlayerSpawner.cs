using System;
using UnityEngine;

namespace ManagerScripts.Spawning
{
    public class PlayerSpawner: MonoBehaviour
    {
        
        
        [Header("Spawn Point For Player")]
        [SerializeField] private Vector2 playerSpawnPos;

        [Space(10)]
        [Header("Player Gameobject")]
        [SerializeField] private GameObject playerPrefab;
        
        
        void OnEnable()
        {
            GameManager.OnPlayerSpawnPosRequest += getPlayerSpawnPos;
        }

        void OnDisable()
        {
            GameManager.OnPlayerSpawnPosRequest -= getPlayerSpawnPos;
        }
        
        
        private Transform getPlayerSpawnPos()
        {
            var obj = spawnPlayer();

            return obj.transform;
        }


        private GameObject spawnPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogWarning("PlayerPrefab is null");
                return null;
            }
            
            var obj = Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
            setPlayerActive(obj);

            return obj;
        }
        
        private void setPlayerActive(GameObject gameObj) => gameObj.SetActive(true);
        
    }
    
}
