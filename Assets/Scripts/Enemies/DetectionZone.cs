using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public event UnityAction<Transform> PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            PlayerDetected?.Invoke(player.transform);
    }
}