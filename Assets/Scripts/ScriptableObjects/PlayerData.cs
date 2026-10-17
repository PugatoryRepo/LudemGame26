using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData: ScriptableObject
    {
        public float Speed;
        public float RotSpeed;

    }
}
