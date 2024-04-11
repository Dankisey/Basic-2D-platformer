using UnityEngine;

public class Coin: MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    private AudioSource _audioPlayer;

    public void Init(AudioSource audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _audioPlayer.PlayOneShot(_collectSound);
            Destroy(gameObject);
        }
    }
}