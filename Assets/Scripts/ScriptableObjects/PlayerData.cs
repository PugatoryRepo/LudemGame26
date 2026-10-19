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

        [Space(10)] 
        public float TimerTorpedo;

        [Space(10)]
        [Range(25,100)]
        public float TorpedoDmg;
    }
}
