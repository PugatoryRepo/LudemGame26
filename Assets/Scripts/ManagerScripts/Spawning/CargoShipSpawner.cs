using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
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
        
        
        [Space(20)]
        [SerializeField] private Tilemap tilemap;

        [Space(10)]
        private List<Vector2> spawnPoints;

        private float xBounds;
        private float yBounds;

      
        void Awake()
        {
            if (!tilemap)
            {
                Debug.LogWarning("No tilemap found");
            }
            else
            {
                xBounds = tilemap.GetComponent<TilemapRenderer>().chunkCullingBounds.x;
                yBounds = tilemap.GetComponent<TilemapRenderer>().chunkCullingBounds.y;
            }
            spawnPoints = new List<Vector2>();
        }
        
        
        void OnEnable()
        {
            GameManager.OnSpawnEnemies += Spawning;
            
            AI.Movement.CargoShipsMovement.OnGetTilemapSize += getTileMapSize;
        }

        void OnDisable()
        {
            GameManager.OnSpawnEnemies -= Spawning;
            
            AI.Movement.CargoShipsMovement.OnGetTilemapSize -= getTileMapSize;
        }
        


        public override void Spawning(Vector2? playerSPoint)
        {
            if (!playerSPoint.HasValue)
            {
                return;
            }
            
            playerSpawnPos = playerSPoint.Value;
            
            GenerateSpawnPoints();
            spawnCargoShips();
        }

        public override void GenerateSpawnPoints()
        {   
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 vec;
                vec.x = Random.Range(-xBounds, 0f);
                vec.y = Random.Range(-yBounds, 0f);
                
                if (vec.x < Vector2.one.x && vec.y < Vector2.one.y)
                {
                    vec += (offSet * Vector2.one);
                }
                
                float mag = (playerSpawnPos - vec).magnitude;
                                
                if (mag >= 1f)
                {
                    vec += (offSet * Vector2.one);
                }
                
                addingSpawnPoints(vec);
            }
            
        }


        private void addingSpawnPoints(Vector2 vec)
        {
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

        public int GetNumberOfEnemies() => numberOfEnemies;

        private float[] getTileMapSize()
        {
            return new[] {xBounds, yBounds };
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
