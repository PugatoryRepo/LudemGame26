using UnityEngine;
using Random = UnityEngine.Random;



namespace AI.Movement
{
    
    public class WaypointChooser
    {
        private Vector2 vec;

        private int min;
        private int max;
        
            
        public WaypointChooser(int m = 0, int  mx = 1)
        {
            min = m;
            max = mx;
            setRandomVector();
        }

        private void setRandomVector()
        {
           float x = Random.Range(min, max);
           float y = Random.Range(min, max);
                
            vec = new Vector2(x, y);
        }

        public void SetMinMax(float newMin, float newMax)
        {
            min = (int) newMin;
            max = (int) newMax;
        }
        
        public void SetMinMax(int newMin, int newMax)
        {
            min = newMin;
            max =  newMax;
        }

        public void SetRandomVector() => setRandomVector();

        public Vector2 GetRandomVector() => vec;
    
    }
}
