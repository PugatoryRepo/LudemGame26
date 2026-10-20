using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {

        public float Speed;
        [Range(1, 3)]
        public float SubHunterSpeed;
        public float RotSpeed;
        public float Health;
        
        public Vector2[] EvacPoints;

        public float Damage;

        public Vector2[] WayPoints;

        public Vector2[] PatrolPoints;

    }

}