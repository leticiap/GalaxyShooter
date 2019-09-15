using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] GameObject _enemyShipPrefab;
    [SerializeField] GameObject[] _powerUps;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PoweUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(-7f, 7f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }

    IEnumerator PoweUpSpawnRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(-7f, 7f);
            int randomPU = Random.Range(0, 3);
            Instantiate(_powerUps[randomPU], new Vector3(randomX, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void DestroySpawnManager()
    {
        Destroy(this.gameObject);
    }
}
