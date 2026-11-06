using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ManagerScripts.Spawning
{
    public class SubHunterSpawner : EnemySpawner
    {


        [Header("Number of SubHunters")] 
        [Space(10)] 
        [Min(1)] 
        [SerializeField] private int subHuntersNum;

        [Space(10)]
        [Min(2f)]
        [Tooltip("Used if enemy spawned around the player.")]
        [SerializeField] private float scaler;
        
        private List<Vector2> enemySpawnPoints;


        void OnEnable()
        {
            GameManager.OnSpawnEnemies += Spawning;
        }

        void OnDisable()
        {
            GameManager.OnSpawnEnemies -= Spawning;
        }  
        
        public override void Spawning(Vector2? playerSpawnPos)
        {
            if (!playerSpawnPos.HasValue)
            {
                return;
            }
            
            
            GenerateSpawnPoints(playerSpawnPos.Value);
            spawnSubHunters();
        }


        public override void GenerateSpawnPoints(Vector2 playerSpawnPos)
        {
            enemySpawnPoints = new List<Vector2>();
            
            for (int i = 0; i < subHuntersNum; i++)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(-25, 25), Random.Range(-25, 25));

                var offset = offSetCal(spawnPoint, playerSpawnPos);
                
                if (offset == Vector2.zero || enemySpawnPoints.Contains(spawnPoint))
                {
                    spawnPoint *= scaler;
                }
                
                enemySpawnPoints.Add(spawnPoint);
            }

            
        }
        
        
        
        private Vector2 offSetCal(Vector2 enemyPos, Vector2? playerPos)
        {
            if (!playerPos.HasValue)
            {
                return Vector2.zero;
            }
            
            Vector2 playerVec = playerPos.Value;
            
            enemyPos.x = (int) Mathf.Abs(enemyPos.x);
            enemyPos.y = (int) Mathf.Abs(enemyPos.y);
            
            playerVec.x = (int) Mathf.Abs(playerVec.x);
            playerVec.y = (int) Mathf.Abs(playerVec.y);
            
            
            return enemyPos -  playerVec;
        }
        
       
        
        private void spawnSubHunters()
        {
            foreach(var point in enemySpawnPoints)
            {
                var obj = Instantiate(enemyPrefab, point, Quaternion.identity);
                
                obj.SetActive(true);
            }
                
        }
        
       
    }

}