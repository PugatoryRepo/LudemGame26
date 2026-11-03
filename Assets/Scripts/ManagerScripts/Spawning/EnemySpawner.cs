using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace ManagerScripts.Spawning
{
    public abstract class EnemySpawner: MonoBehaviour
    {

        public GameObject enemyPrefab;
        
        protected List<Vector2> enemySpawnPoints;
        
        public Vector2 PlayerSpawnPos { get; set; }
        

        public abstract void GenerateSpawnPoints(Vector2 playerSpawnPos);
            
        public abstract void Spawning();

    }
    
}