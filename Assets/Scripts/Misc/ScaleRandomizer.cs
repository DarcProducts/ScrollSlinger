using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;

    void OnEnable()
    {
        transform.localScale = new Vector3(
            Random.Range(minScale.x, maxScale.x), 
            Random.Range(minScale.y, maxScale.y), 
            Random.Range(minScale.z, maxScale.z));
    }
}
