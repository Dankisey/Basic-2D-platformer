using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.transform.position = player.StartPosition;
        }   
    }  
}