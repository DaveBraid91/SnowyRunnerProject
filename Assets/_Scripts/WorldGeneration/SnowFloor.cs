using UnityEngine;

namespace _Scripts.WorldGeneration
{
    public class SnowFloor : MonoBehaviour
    {
        [SerializeField] private Transform playerTF;
        [SerializeField] private Material material;
        [SerializeField] private float offsetSpeed = 0.5f;
        private static readonly int SnowOffset = Shader.PropertyToID("snowOffset");


        private void Update()
        {
            //Moves the snow ground with the player and moves the offset of the texture so
            //it gives the impression of moving faster
            transform.position = Vector3.forward * playerTF.position.z;
            material.SetVector(SnowOffset, new Vector2(0, -transform.position.z * offsetSpeed));
        }
    }
}