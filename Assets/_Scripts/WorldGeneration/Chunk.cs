using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private float chunkLength;

    public float GetChunkLength()
    {
        return chunkLength;
    }

    public Chunk ShowChunk()
    {
        transform.gameObject.BroadcastMessage("OnShowChunk", SendMessageOptions.DontRequireReceiver);

        gameObject.SetActive(true);

        return this;
    }

    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
