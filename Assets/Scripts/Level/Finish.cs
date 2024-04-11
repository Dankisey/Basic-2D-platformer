using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip _enterSound;
    [SerializeField] private AudioSource _audioPlayerPrefab;

    private AudioSource _audioPlayer;
    private bool _finished;

    private void Start()
    {
        _audioPlayer = Instantiate(_audioPlayerPrefab);
        _finished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && _finished == false)
        {
            _audioPlayer.PlayOneShot(_enterSound);
            _finished = true;
        }
    }
}