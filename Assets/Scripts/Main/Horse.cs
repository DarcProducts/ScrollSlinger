using UnityEngine;
using UnityEngine.Events;

public class Horse : MonoBehaviour
{
    [SerializeField] UnityEvent HorseStepped;

    public void OnHorseStep() => HorseStepped?.Invoke();
}
