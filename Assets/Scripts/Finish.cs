using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip _enterSound;

    private AudioPlayer _audioPlayer;
    private bool _finished;

    private void Start()
    {
        _audioPlayer = (AudioPlayer)FindObjectOfType(typeof(AudioPlayer));
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