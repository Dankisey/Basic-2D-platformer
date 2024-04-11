using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.position = new Vector3(_target.position.x, _target.position.y, _transform.position.z);
    }
}