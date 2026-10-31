using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {

        [Header("Enemies' movement info")]
        public float Speed;
        [Range(1, 3)]
        public float SubHunterSpeed;
        public float RotSpeed;
        
        [Header("Enemies' health")]
        [Space(20)]
        public float Health;

        [Space(20)]
        [Header("Positions for enemies")]
        public Vector2[] EvacPoints;
        
        public Vector2[] WayPoints;

        public Vector2[] PatrolPoints;

        [Space(20)]
        [Header("Mines' info")]
        [Range(2, 4)]
        public int MineMaxLimit;
        
        [Range(100, 200)]
        public float Damage;

    }

}