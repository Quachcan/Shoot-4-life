using System.Collections.Generic;
using UnityEngine;

namespace Game._Scripts.Player
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance { get; private set; }

        private readonly Dictionary<Animator, Dictionary<string, int>>
            animationHashes = new Dictionary<Animator, Dictionary<string, int>>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }


        public void InitializeAnimator(Animator animator, params string[] parameters)
        {
            if(!animationHashes.ContainsKey(animator))
            {
                var hashes = new Dictionary<string, int>();
                foreach (var parameter in parameters)
                {
                    hashes[parameter] =  Animator.StringToHash(parameter);
                }
                animationHashes[animator] = hashes;
            }
        }

        public void SetBool(Animator animator, string parameter, bool value)
        {
            if (animationHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetBool(hash, value);
            }
        }

        public void SetTrigger(Animator animator, string parameter)
        {
            if(animationHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetTrigger(hash);
            }
        }

        public void SetFloat(Animator animator, string parameter, float value)
        {
            if (animationHashes.TryGetValue(animator,out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetFloat(hash, value);
            }
        }

        public void SetInt(Animator animator, string parameter, int value)
        {
            if (animationHashes.TryGetValue(animator, out var hashes) && hashes.TryGetValue(parameter, out var hash))
            {
                animator.SetInteger(hash, value);
            }
        }
    }
}
