using UnityEngine;
using System.Collections.Generic;
using Movement;
using ManagerScripts;

namespace Combat
{
    public class Mines : MonoBehaviour
    {

        protected struct Point
        {
            
            public readonly int X;
            public readonly int Y;

            public Point(float xCoord, float yCoord)
            {
                X=  (int) Mathf.Round(xCoord);
                Y= (int) Mathf.Round(yCoord);
            }
            
        }
        
        
        [SerializeField] private Texture2D texture;

        private readonly string texturePath = "Assets/map_texture_combined.png";

        private List<Color> greenPixels;

        private Point point;

        void Awake()
        {
            if (texture == null)
            {
                texture = Resources.Load<Texture2D>(texturePath);
            }

            greenPixels = new List<Color>();
            getGreenColorsOnMap();
        }

        private void getGreenColorsOnMap()
        {
            Color[] colors = texture.GetPixels();

            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];

                if (color.g > 0f)
                {
                    greenPixels.Add(color);
                }
                
            }
            
            print(texture.width+", "+texture.height);
            print(greenPixels.Count);
        }
        
        //Need Player to subscribe.
        void OnEnable()
        {
            PlayerMovement.PlayerOnMove += isPlayerOnMine;
        }

        void OnDisable()
        {
            PlayerMovement.PlayerOnMove -= isPlayerOnMine;
        }

        
        
        private void isPlayerOnMine(Vector2 playerPos)
        {
            point = new Point(playerPos.x, playerPos.y);
            
            var pointOnMap = texture.GetPixel( point.X, point.Y);

           // print(pointOnMap+": "+point.X+", "+point.Y);
            if (greenPixels.Contains(pointOnMap))
            {
                GameManager.States deadState = GameManager.States.Dead;
                GameManager.Instance.SetCurrState(deadState);
            }
        }
        
        
    }

}