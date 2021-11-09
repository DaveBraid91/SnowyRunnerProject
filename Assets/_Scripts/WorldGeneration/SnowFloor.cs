using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform playerTF;
    [SerializeField] private Material material;
    [SerializeField] private float offsetSpeed = 0.5f;


    private void Update()
    {
        transform.position = Vector3.forward * playerTF.position.z;
        material.SetVector("snowOffset", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}