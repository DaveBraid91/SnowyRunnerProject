using UnityEngine;

namespace _Scripts.WorldGeneration
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private float chunkLength;

        /// <summary>
        /// Gets how long in Unity Units the chunk is
        /// </summary>
        /// <returns>The chunk length</returns>
        public float GetChunkLength()
        {
            return chunkLength;
        }

        /// <summary>
        /// Activates the chunk and informs the rest of the game
        /// </summary>
        /// <returns>The chunk component of the gameObject</returns>
        public Chunk ShowChunk()
        {
            transform.gameObject.BroadcastMessage("OnShowChunk", SendMessageOptions.DontRequireReceiver);

            gameObject.SetActive(true);

            return this;
        }

        /// <summary>
        /// Disables the chunk
        /// </summary>
        /// <returns>The chunk component of the gameObject</returns>
        public Chunk HideChunk()
        {
            gameObject.SetActive(false);
            return this;
        }
    }
}
