using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float maxHeight = 10f;

    [SerializeField] private Transform minX;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minZ;
    [SerializeField] private Transform maxZ;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = GameObject.FindObjectOfType<VariableJoystick>();
        slider = GameObject.FindObjectOfType<Slider>();
        // Mengatur nilai slider agar sesuai dengan batasan ketinggian
        slider.minValue = minHeight;
        slider.maxValue = maxHeight;
        slider.value = slider.maxValue;
    }

    private void Update()
    {
        MoveShip();
        LimitArea();
        /*AdjustDepth();*/
    }

    private void MoveShip()
    {
        // Mengambil input dari joystick
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Menghitung arah gerakan berdasarkan arah hadap kapal
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Menghitung sudut rotasi yang diperlukan
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotateSpeed, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Menggerakkan kapal ke depan sesuai arah hadapnya
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void AdjustDepth()
    {
        // Mengambil nilai dari slider dan mengatur targetY
        float targetY = slider.value;

        // Mengatur posisi kapal pada sumbu Y tanpa mengganggu X dan Z
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    private void LimitArea()
    {
        Vector3 position = transform.position;

        // Membatasi posisi X kapal
        position.x = Mathf.Clamp(position.x, minX.position.x, maxX.position.x);

        // Membatasi posisi Z kapal
        position.z = Mathf.Clamp(position.z, minZ.position.z, maxZ.position.z);

        // Memperbarui posisi kapal setelah dibatasi
        transform.position = position;
    }

}
