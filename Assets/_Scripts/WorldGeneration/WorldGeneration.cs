using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.WorldGeneration
{
    public class WorldGeneration : MonoBehaviour
    {
        //Gameplay
        private float chunkSpawnZ;
        private Queue<Chunk> activeChunks = new Queue<Chunk>();
        private List<Chunk> chunkPool = new List<Chunk>();

        //Configurable fields
        [SerializeField] private int firstSpawnChunkPosition = 5;
        [SerializeField] private int chunksOnScreen = 5;
        [SerializeField] private float despawnDistance = 5.0f;

        [SerializeField] private List<GameObject> chunkPrefabs = new List<GameObject>();
        [SerializeField] private Transform cameraTransform = null;
        // Start is called before the first frame update
        void Start()
        {
            //Check if we have an empty chunkprefabs list
            if(chunkPrefabs.Count == 0)
            {
                Debug.LogError("No chunk prefab found in the world generator");
                return;
            }

            if(cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
                Debug.Log("cameraTransform is assigned to Camera.main");
            }
       
            ResetWorld();

        }
        
        /// <summary>
        /// Scans the position the world generator. Spawns and disables chunks
        /// depending on its position
        /// </summary>
        public void ScanPosition()
        {
            //If the chunk is far enough, spawn a new one and disable the last one
            var cameraZ = cameraTransform.position.z;
            //Peek() returns the object of the highest index of the queue.
            var lastChunk = activeChunks.Peek();

            if (!(cameraZ >= lastChunk.transform.position.z + lastChunk.GetChunkLength() + despawnDistance)) return;
            SpawnNewChunk();
            DisableLastChunk();
        }
        
        /// <summary>
        /// Spawns a new random chunk from the pool. If its not in the pool, instantiates a new one
        /// </summary>
        private void SpawnNewChunk()
        {
            //Get a random index for which prefab spawns
            var randomIndex = Random.Range(0, chunkPrefabs.Count);

            //Is it in the pool?
            var chunk = chunkPool.Find(x => !x.gameObject.activeSelf && x.name.Contains(chunkPrefabs[randomIndex].name));

            //Create a Chunk if you don't find one
            if(chunk == null)
            {
                var chunkInstance = Instantiate(chunkPrefabs[randomIndex], transform);
                chunk = chunkInstance.GetComponent<Chunk>();
            }
            //Place the object and show it
            chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
            chunkSpawnZ += chunk.GetChunkLength();

            //Store the value to reuse in our active pool
            activeChunks.Enqueue(chunk);
            chunk.ShowChunk();
        }
        
        /// <summary>
        /// Disables the oldest chunk from the active pool
        /// </summary>
        private void DisableLastChunk()
        {
            //Removes the oldest object from the queue and returns it. It is stored in chunk.
            var chunk = activeChunks.Dequeue();

            chunk.HideChunk();
            chunkPool.Add(chunk);
        }

        /// <summary>
        /// Resets the world and the active chunk spawn queue
        /// </summary>
        public void ResetWorld()
        {
            //Reset the chunkSpawnZ and reset the queue
            chunkSpawnZ = firstSpawnChunkPosition;

            for(int i = activeChunks.Count; i > 0; i--)
            {
                DisableLastChunk();
            }

            for(int i = 0; i < chunksOnScreen; i++)
            {
                SpawnNewChunk();
            }
        }
    }
}
