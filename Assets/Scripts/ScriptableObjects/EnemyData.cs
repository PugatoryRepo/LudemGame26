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
        [Range(1, 4)]
        public int MineMaxLimit;

        [Space(10)] 
        [Range(1, 10)]
        public float MineXPosOffset;
        
        [Range(100, 200)]
        public float Damage;

    }

}