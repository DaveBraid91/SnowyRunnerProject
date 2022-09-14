using UnityEngine;

namespace _Scripts.GamePlay
{
    public class Fish : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;

        private void Start()
        {
            anim = GetComponentInParent<Animator>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PickUpFish();
            }
        }
        /// <summary>
        /// Triggers the pickup animation and informs GameStats when a fish is picked up
        /// </summary>
        private void PickUpFish()
        {
            //TODO: play sfx
            anim.SetTrigger("Pickup");
            GameStats.Instance.CollectCollectable();
        }
        /// <summary>
        /// When a new chunk is shown, the Idle animation is activated
        /// </summary>
        public void OnShowChunk()
        {
            anim.SetTrigger("Idle");
        }
    }
}
