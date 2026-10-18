using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData: ScriptableObject
    {
        [Space(10)]
        public float Speed;
        
        [Space(10)]
        public float RotSpeed;

    }
}
