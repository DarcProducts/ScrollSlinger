using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [SerializeField] Vector3 moveVector;
    [SerializeField] float moveSpeed;

    void FixedUpdate() => transform.Translate(moveSpeed * Time.fixedDeltaTime * moveVector.normalized, Space.World);
}
