using UnityEngine;

public class Coin: MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _audioPlayer = (AudioPlayer)FindObjectOfType(typeof(AudioPlayer));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _audioPlayer.PlaySound(_collectSound);
            Destroy(gameObject);
        }
    }
}