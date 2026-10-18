using UnityEngine;

namespace Combat
{
    public class Mines : MonoBehaviour
    {
        
        [SerializeField] private Texture2D texture;

        private readonly string texturePath = "Assets/map_texture_combined.png";

        void Awake()
        {
            if (texture == null)
            {
                texture = Resources.Load<Texture2D>(texturePath);
            }

            var points = texture.GetPixelBilinear();



        }
        
        
        
        
        
        
        
        
        
    }

}