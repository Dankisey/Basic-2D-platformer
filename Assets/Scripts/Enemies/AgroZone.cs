using UnityEngine;
using UnityEngine.Events;

public class AgroZone : MonoBehaviour
{
    public event UnityAction PlayerLost;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            PlayerLost?.Invoke();
    }
}
