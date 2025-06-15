using System;
using UnityEngine;

namespace Game._Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        private PlayerManager _playerManager;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            _playerManager = GetComponentInChildren<PlayerManager>();
            _playerManager.InitializePlayer();
        }
    }
}
