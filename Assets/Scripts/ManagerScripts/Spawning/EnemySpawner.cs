using UnityEngine;


namespace ManagerScripts.Spawning
{
    public abstract class EnemySpawner: MonoBehaviour
    {

        public GameObject enemyPrefab;


        protected Vector2 playerSpawnPos;

        public abstract void GenerateSpawnPoints();
            
        public abstract void Spawning(Vector2? playerSpawnPos);

    }
    
}