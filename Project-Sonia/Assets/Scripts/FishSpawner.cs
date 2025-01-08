using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish Prefabs")]
    public GameObject fishPrefabs; // Array untuk menyimpan prefab ikan

    [Header("Spawn Settings")]
    public int spawnCount = 5; // Jumlah ikan yang ingin di-spawn
    public float spawnDelay = 2f; // Waktu delay antara spawn ikan
    public float spawnHeight = 1f; // Ketinggian spawn ikan dari ground

    void Start()
    {
        // Mulai coroutine untuk spawn ikan
        StartCoroutine(SpawnFish());
    }

    // Coroutine untuk spawn ikan secara berkala
    IEnumerator SpawnFish()
    {
        for (int i = 0; i < spawnCount; i++)
        {

            // Gunakan posisi objek ini sebagai titik spawn (transform.position)
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnHeight, transform.position.z);

            // Spawn ikan pada posisi yang ditentukan
            Instantiate(fishPrefabs);

            // Tunggu beberapa detik sebelum spawn ikan berikutnya
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
