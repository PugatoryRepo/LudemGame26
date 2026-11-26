using UnityEngine;
using System.Collections.Generic;
using ScriptableObjects;
using AI.Movement;

namespace Combat.Mines
{
    
    /// <summary>
    /// Serves for the subhunters to lay mines.
    /// </summary>
    public class MineLaying : MonoBehaviour
    {

        [Header("Enemy info")]
        [Space(10)]
        [Tooltip("Used to extract mine's info")]
        [SerializeField] private EnemyData enemyData;

        [Space(10)] [Header("Mine")]
        [SerializeField] private SubHunterMovement movement;
        
        [Space(10)]
        [SerializeField] private GameObject mineObj;
        
        
        [Space(10)]
        [SerializeField] private float mineXOffset;
        
        private List<GameObject> mineObjPool;

        private float nextFireTime;
        
        private int limitFire;

        private Vector2 minePos;
        private Quaternion mineRot;

        private readonly float timeAdd = 5f;

        void Awake()
        {
            mineObjPool = new List<GameObject>();

            if (enemyData != null)
            {
                for (int i = 0; i < enemyData.MineMaxLimit; i++)
                {
                    var obj = Instantiate(mineObj);
                    obj.SetActive(false);
                    mineObjPool.Add(obj);
                }
            }

            if (movement == null)
            {
                movement = GetComponent<SubHunterMovement>();
            }
            
        }
        
        void Start()
        {
            nextFireTime = Time.time+timeAdd; //I don't remember what this is used for. 
        }
        

        void OnEnable()
        {
            movement.OnSubHunterMove += layMine;
        }

        void OnDisable()
        {
            movement.OnSubHunterMove -= layMine;
        }
        

        /// <summary>
        /// Based on subhunter's position and rotation, it lays a mine.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        private void layMine(Vector2 pos, Quaternion rot)
        {
            minePos = pos;
            
            mineRot = rot;
            
            var mine = findMine();

            if (mine)
            {
                minePos.x += mineXOffset;
                mine.transform.position = minePos;
                mine.transform.rotation = mineRot;
            
                mine.SetActive(true);
            }
        }
        

        /// <summary>
        /// Finds a mine that isn't unactive and gets it for it to lay mine.
        /// </summary>
        /// <returns>A GameObject which is the mine that subhunter uses.
        /// If it can't find one, it returns null.</returns>
        private GameObject findMine()
        {

            foreach (var obj in mineObjPool)
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;;
                }
            }

            return null;
        }
        
        
    }
    
}
