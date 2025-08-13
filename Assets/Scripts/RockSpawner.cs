using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    int max = 10;
    int count = 1;

	private void Start() {
		StartCoroutine(SpawnRockCoroutine());
	}
	IEnumerator SpawnRockCoroutine() {
        if(count <= max) {
			float x = Random.Range(-7, 7);
			float z = Random.Range(-2, 12);
			Instantiate(rockPrefab, new Vector3(x, -.48f, z), Quaternion.identity);
			count++;
		}
        yield return new WaitForSeconds(6);
        StartCoroutine(SpawnRockCoroutine());

    }
}
