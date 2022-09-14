using System.Collections.Generic;
using _Scripts.Save;
using UnityEngine;

namespace _Scripts.Shop
{
    public class HatLogic : MonoBehaviour
    {
        [SerializeField] private Transform hatContainer;
        private List<GameObject> hatModels = new List<GameObject>();
        private Hat[] hats;

        private void Start()
        {
            hats = Resources.LoadAll<Hat>("Hats/");
            SpawnHats();
            SelectHat(SaveManager.Instance.saveState.CurrentHatIndex);
        }
        /// <summary>
        /// Spawns all of the hat models on the place where they are supposed to go (Normally a models head)
        /// </summary>
        private void SpawnHats()
        {
            for (int i = 0; i < hats.Length; i++)
            {
                hatModels.Add(Instantiate(hats[i].Model, hatContainer) as GameObject);
            }
        }
        /// <summary>
        /// Disables all of the hats
        /// </summary>
        public void DisableAllHats()
        {
            foreach  (GameObject model in hatModels)
            {
                model.SetActive(false);
            }
        }
        /// <summary>
        /// Enables the selected hat
        /// </summary>
        /// <param name="index">Index of the selected hat</param>
        public void SelectHat(int index)
        {
            DisableAllHats();
            hatModels[index].SetActive(true);
        }
    }
}
