using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    [Header("All Fish")]
    public GameObject[] selectedFish;

    [Header("Patrol Boundaries")]
    public Transform minXLimit; // Objek batas minimum pada sumbu X
    public Transform maxXLimit; // Objek batas maksimum pada sumbu X
    public Transform minZLimit; // Objek batas minimum pada sumbu Z
    public Transform maxZLimit; // Objek batas maksimum pada sumbu Z

    [Header("Movement Settings")]
    public float speed = 2f; // Kecepatan ikan
    public float rotationSpeed = 5f; // Kecepatan rotasi ikan
    private Vector3 targetPosition; // Posisi tujuan ikan

    private GameObject activeFish; // Ikan yang aktif

    void Start()
    {
        SelectRandomFish();
        // Set posisi awal target
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Gerakan ikan menuju target
        MoveTowardsTarget();

        // Jika ikan sudah sampai di target, pilih target baru
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    // Menentukan target posisi ikan secara acak dalam batas transform min dan max untuk X dan Z
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minXLimit.position.x, maxXLimit.position.x);
        float randomZ = Random.Range(minZLimit.position.z, maxZLimit.position.z);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    // Menggerakkan ikan menuju target dengan rotasi yang sesuai
    void MoveTowardsTarget()
    {
        // Hitung arah ke target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rotasi ikan ke arah target
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Gerakkan ikan maju
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Debugging batas area
    private void OnDrawGizmos()
    {
        if (minXLimit != null && maxXLimit != null && minZLimit != null && maxZLimit != null)
        {
            Gizmos.color = Color.green;

            // Gambarkan garis batas area patroli
            Vector3 corner1 = new Vector3(minXLimit.position.x, transform.position.y, minZLimit.position.z);
            Vector3 corner2 = new Vector3(minXLimit.position.x, transform.position.y, maxZLimit.position.z);
            Vector3 corner3 = new Vector3(maxXLimit.position.x, transform.position.y, maxZLimit.position.z);
            Vector3 corner4 = new Vector3(maxXLimit.position.x, transform.position.y, minZLimit.position.z);

            Gizmos.DrawLine(corner1, corner2);
            Gizmos.DrawLine(corner2, corner3);
            Gizmos.DrawLine(corner3, corner4);
            Gizmos.DrawLine(corner4, corner1);
        }
    }

    void SelectRandomFish()
    {
        if (selectedFish.Length > 0)
        {
            // Pilih indeks acak
            int randomIndex = Random.Range(0, selectedFish.Length);

            // Aktifkan ikan yang dipilih dan nonaktifkan yang lain
            for (int i = 0; i < selectedFish.Length; i++)
            {
                if (i == randomIndex)
                {
                    activeFish = selectedFish[i];
                    activeFish.SetActive(true);
                }
                else
                {
                    selectedFish[i].SetActive(false);
                }
            }
        }
    }
}
