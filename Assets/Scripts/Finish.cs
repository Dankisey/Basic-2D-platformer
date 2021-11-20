using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip _enterSound;
    [SerializeField] private AudioPlayer _audioPlayerPrefab;

    private AudioPlayer _audioPlayer;
    private bool _finished;

    private void Start()
    {
        _audioPlayer = Instantiate(_audioPlayerPrefab);
        _finished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && _finished == false)
        {
            _audioPlayer.PlaySound(_enterSound);
            _finished = true;
        }
    }
}