using UnityEngine;
using ScriptableObjects;


namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IEntityMovement
    {
        
        private PlayerData playerData;
        
        private float speed;
        private float rotSpeed;

        
        void Awake()
        {
            if (playerData == null)
            {
                Debug.LogWarning("PlayerData is null");
            }
            
            speed = playerData.Speed;
            rotSpeed = playerData.RotSpeed;
        }


        void Update()
        {
            
        }
    
        public void Move()
        {

            
        }

        public void Rotate()
        {
        
        }

        public float GetSpeed() => speed;
        public float GetRotSpeed() => rotSpeed;

    }
}
