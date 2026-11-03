using System;
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


        void OnEnable()
        {
            PlayerSpawner.OnPlayerSpawn += GenerateSpawnPoints;
        }

        void OnDisable()
        {
            PlayerSpawner.OnPlayerSpawn -= GenerateSpawnPoints;
        }
        
        public override void GenerateSpawnPoints(Vector2 playerSpawnPos)
        {
            
            for (int i = 0; i < subHuntersNum; i++)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));

                var offset = PlayerSpawnPos  -spawnPoint;
                
                if (offset == Vector2.zero)
                {
                    
                }
                
                enemySpawnPoints.Add(spawnPoint);
            }
        }
        
        public override void Spawning()
        {
            
            foreach(var point in enemySpawnPoints)
            {

                print(point);
                var obj = Instantiate(enemyPrefab, point, Quaternion.identity);
                
                obj.SetActive(true);
            }

        }
        
        void Start()
        {
            Spawning();
        }
    
    }

}