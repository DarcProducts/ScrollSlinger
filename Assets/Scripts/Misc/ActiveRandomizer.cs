using UnityEngine;

public class ActiveRandomizer : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float chanceToBeActive;
    
    void OnEnable()
    {
        if (Random.value > chanceToBeActive)
            gameObject.SetActive(false);
    }
}
