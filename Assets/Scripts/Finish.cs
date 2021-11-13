using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip _enterSound;
    [SerializeField] private GameObject _audioPlayerPrefab;

    private AudioPlayer _audioPlayer;
    private bool _finished;

    private void Start()
    {
        var audioPlayer = Instantiate(_audioPlayerPrefab);
        _audioPlayer = audioPlayer.GetComponent<AudioPlayer>();
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