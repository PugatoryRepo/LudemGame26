using UnityEngine;


namespace Combat
{
    public class TorpedoTimer : MonoBehaviour
    {
        [Range(5f, 20f)]
        [SerializeField] private float timerSeed;
    
        private float timer;

        void OnEnable()
        {
            timer = Time.time + timerSeed;
        }

        void Update()
        {
            if (Time.time > timer)
            {
                gameObject.SetActive(false);
                timer = 0f;
            }
        }
    
    }
    
    
}
