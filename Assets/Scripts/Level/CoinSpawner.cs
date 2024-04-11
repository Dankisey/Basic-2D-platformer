using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private AudioSource _audioPlayerPrefab;

    private Transform _spawnPlaces;
    private Transform[] _spawners;

    private void Start()
    {
        var audioPlayer = Instantiate(_audioPlayerPrefab);
        _spawnPlaces = transform;
        _spawners = new Transform[_spawnPlaces.childCount];

        for (int i = 0; i < _spawners.Length; i++)      
            _spawners[i] = _spawnPlaces.GetChild(i);
        

        for (int currentSpawner = 0; currentSpawner < _spawners.Length; currentSpawner++)
        {
            var coin = Instantiate(_coinPrefab, _spawners[currentSpawner].position, Quaternion.identity);

            coin.Init(audioPlayer);
        }
    }
}