using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private Transform _spawnPlaces;
    private Transform[] _spawners;

    private void Start()
    {
        _spawnPlaces = transform;

        _spawners = new Transform[_spawnPlaces.childCount];

        for (int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i] = _spawnPlaces.GetChild(i);
        }

        for (int currentSpawner = 0; currentSpawner < _spawners.Length; currentSpawner++)
        {
            GameObject coin = Instantiate(_prefab, _spawners[currentSpawner].position, Quaternion.identity);
        }
    }
}