using UnityEngine;

namespace Game._Scripts.Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private Animator _animator;

        
        
        public void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}
