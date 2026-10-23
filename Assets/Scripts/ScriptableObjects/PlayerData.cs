using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData: ScriptableObject
    {
        [Space(10)]
        public float Speed;
        
        [Space(10)]
        [Range(0f,100f)]
        public float RotSpeed;

        [Space(10)] 
        [Range(1, 3)]
        public float TorpedoSpeed;

        [Space(10)]
        [Range(25,100)]
        public float TorpedoDmg;
    }
}
