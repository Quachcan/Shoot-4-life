using UnityEngine;

namespace Game._Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")] 
        public float moveSpeed;
        public float runSpeed;
        public float rotationSpeed;
        public float rotationThreshold;
        
        [Header("Jump Settings")] public float jumpHeight;
        
        [Header("Gravity")]
        public float fallGravityMultiplier;
        public float riseGravityMultiplier;
    }
}
