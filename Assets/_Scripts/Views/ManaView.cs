using UnityEngine;
using System.Collections.Generic;

public class ManaView : MonoBehaviour
{
    // Random Spawning could be fixed to circular instead of square

    [SerializeField] GameObject _gemPrefab;
    [SerializeField] float _height = 10f;
    [SerializeField] float _range = 2f;
    List<GameObject> _manaGems = new();
    int _count;

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnRandomly();
        }
    }

    public void Add(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (_count == _manaGems.Count + 1) SpawnRandomly();
            _manaGems[_count].SetActive(true);
            _count += 1;
        }
    }
    public void Subtract(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (_count == 0) break;
            _count -= 1;
            ResetPosition(_manaGems[_count]);
            _manaGems[_count].SetActive(false);
        }
    }

    void SpawnRandomly()
    {
        Vector3 offset = new(Random.Range(-_range, _range), _height, Random.Range(-_range, _range));
        GameObject gem = Instantiate(_gemPrefab, transform.position + offset, Random.rotation);
        gem.SetActive(false);
        _manaGems.Add(gem);
    }

    void ResetPosition(GameObject gem)
    {
        gem.transform.position = transform.position + new Vector3(Random.Range(-_range, _range), _height, Random.Range(-_range, _range));
    }
}
