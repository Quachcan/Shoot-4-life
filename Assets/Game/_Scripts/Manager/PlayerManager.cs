using System;
using Game._Scripts.Player;
using UnityEngine;

namespace Game._Scripts.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public Movement movement;
        public AnimationManager playerAnimationManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        

        public void InitializePlayer()
        {
            movement.Initialize();
        }
    }
}
