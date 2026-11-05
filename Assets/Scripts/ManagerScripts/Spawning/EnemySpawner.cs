using UnityEngine;


namespace ManagerScripts.Spawning
{
    public abstract class EnemySpawner: MonoBehaviour
    {

        public GameObject enemyPrefab;
        
        
        public Vector2 PlayerSpawnPos { get; set; }
        

        public abstract void GenerateSpawnPoints(Vector2? playerSpawnPos);
            
        public abstract void Spawning(Vector2? playerSpawnPos);

    }
    
}