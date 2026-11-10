using System;
using UnityEngine;
using ManagerScripts;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace ManagerScripts.Spawning
{
    public class CargoShipSpawner: EnemySpawner
    {
        [Header("Number of cargo ships")]
        [Space(10)]
        [SerializeField] private int numberOfEnemies;

        [Space(10)]
        [Header("Spawn points")]
        [SerializeField] private float xPos;
        [SerializeField] private float yPos;
        
        [Tooltip("For correcting spawn points")]
        [SerializeField] private float offSet;

        [Space(10)]
        private List<Vector2> spawnPoints;
        
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
            
            base.playerSpawnPos = playerSpawnPos.Value;
            
            GenerateSpawnPoints();
            spawnCargoShips();
        }

        public override void GenerateSpawnPoints()
        {   
            spawnPoints = new List<Vector2>();


            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 vec = new Vector2();
                vec.x = Random.Range(-xPos, xPos);
                vec.y = Random.Range(-yPos, yPos);

                var vecEst = vec;
                vecEst.x = (int) vecEst.x;
                vecEst.y = (int) vecEst.y;
                
                
                if (vec == Vector2.zero || vecEst == playerSpawnPos)
                {
                    vec += Vector2.one;
                    vec *= offSet;
                }
                
                if (!spawnPoints.Contains(vec))
                {
                    spawnPoints.Add(vec);
                }
                else
                {
                    vec *= offSet;
                    spawnPoints.Add(vec);
                }
            }
            
        }

        private void spawnCargoShips()
        {
            foreach(var point in spawnPoints)
            {
                var obj = Instantiate(enemyPrefab, point, Quaternion.identity);
                
                obj.SetActive(true);
            }

        }
        
        
    }
    
    
}
