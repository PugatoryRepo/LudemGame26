using System;
using Unity.Cinemachine;
using UnityEngine;
using ManagerScripts;

namespace CineMachine
{
    public class LocatePlayer : MonoBehaviour
    {

        [SerializeField] private CinemachineCamera  cam;
    
        void OnEnable()
        {
            GameManager.OnPlayerSpawn += locatePlayer;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerSpawn -= locatePlayer;
        }


        private void locatePlayer(Transform player)
        {

            if (cam == null || player == null)
            {
                Debug.LogWarning($"Something is null {cam == null} and {player == null}");
                return;
            }


            cam.Target.TrackingTarget = player.transform;
        }
    
    }
}
