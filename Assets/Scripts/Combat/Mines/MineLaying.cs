using UnityEngine;
using System.Collections.Generic;
using ScriptableObjects;
using AI.Movement;

namespace Combat.Mines
{
    public class MineLaying : MonoBehaviour
    {

        [Header("Enemy info")]
        [Space(10)]
        [Tooltip("Used to extract mine's info")]
        [SerializeField] private EnemyData enemyData;
        
        [Space(10)]
        [SerializeField] private GameObject mineObj;
        
        
        [Space(10)]
        [SerializeField] private float mineXOffset;
        
        private List<GameObject> mineObjPool;

        private float nextFireTime;
        
        private int limitFire;

        private Vector2 minePos;
        private Quaternion mineRot;

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
            
        }
        
        
        void Start()
        {
            nextFireTime = Time.time+5f;
        }
        

        void OnEnable()
        {
            SubHunterMovement.OnSubHunterMove += setPositionAndRot;
        }

        void OnDisable()
        {
            SubHunterMovement.OnSubHunterMove -= setPositionAndRot;
        }
        

        private void setPositionAndRot(Vector2 pos, Quaternion rot)
        {
            minePos = pos;
            
            
            mineRot = rot;
        }
        
        void Update()
        {

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 5f; // Based on video: https://www.youtube.com/shorts/Cpg3B0gVWB8
                var mine = findMine();

                if (mine)
                {
                    layMine(mine);
                }
                
            }

        }

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

        private void layMine(GameObject mine)
        {
            minePos.x += mineXOffset;
            mine.transform.position = minePos;
            mine.transform.rotation = mineRot;
            
            mine.SetActive(true);
        }
        
    }
    
}
