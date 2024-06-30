using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceCount : MonoBehaviour
{
    // Variabel untuk data zona
    [Header("Data Zona")]
    [SerializeField] private ZonaScriptableObject[] zonaData;

    // Referensi game object
    [Header("Gameobject Component")]
    [SerializeField] private GameObject pangkal;
    [SerializeField] private GameObject kapal;  // Referensi ke kapal

    // Komponen UI 
    [Header("UserInterface Compoment")]
    [SerializeField] private GameObject descriptionImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button descriptionButton;
    [SerializeField] private GameObject notification1;
    [SerializeField] private GameObject notification2;

    // Komponen untuk audio
    [Header("Audio Component")]
    [SerializeField] private AudioSource audioSource;
    private AudioClip audioClip;

    // Variabel lainnya
    private int zonaNow;
    public int languageID;
    private bool isMusicPlaying = false; // Menandakan apakah musik sedang diputar


    // Start is called before the first frame update

    private void Awake()
    {
        InitializeComponent();
        InitializeCompomentUI();
    }
    void Start()
    {
        zonaNow = 0;        
    }

    void InitializeCompomentUI()
    {
        descriptionImage = GameObject.Find("Scroll View Description");
        notification1 = GameObject.Find("Notification1");
        notification2 = GameObject.Find("Notification2");

        // Mengatur listener untuk tombol suara
        GameObject soundButtonObject = GameObject.Find("ButtonSoundDescription");
        soundButton = soundButtonObject.GetComponent<Button>();
        soundButton.onClick.AddListener(ToggleMusic);

        // Mengatur listener untuk tombol suara
        GameObject descriptionButtonObject = GameObject.Find("ButtonDescription");
        descriptionButton = descriptionButtonObject.GetComponent<Button>();
        descriptionButton.onClick.AddListener(ToggleDescription);

        // Mengambil komponen TextMeshPro untuk UI
        GameObject distanceTextObject = GameObject.Find("DistanceText");
        distanceText = distanceTextObject.GetComponent<TextMeshProUGUI>();

        GameObject titleTextObject = GameObject.Find("ZonaNameText");
        titleText = titleTextObject.GetComponent<TextMeshProUGUI>();

        GameObject descriptionTextObject = GameObject.Find("ZonaDescription");
        descriptionText = descriptionTextObject.GetComponent<TextMeshProUGUI>();

        descriptionImage.SetActive(false);
    }

    void InitializeComponent()
    {
        kapal = this.gameObject;
        pangkal = GameObject.Find("Pangkal");
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
    }

    // Menghitung jarak antara kapal dan pangkal
    void CalculateDistance()
    {
        float distance = Vector3.Distance(pangkal.transform.position, kapal.transform.position);

        // Menampilkan jarak di UI jika distanceText tidak null
        if (distanceText != null)
        {
            distanceText.text = (distance * 100).ToString("F2") + " mil";
        }
    }

    // Mengubah deskripsi zona yang sedang ditampilkan berdasarkan bahasa
    void ChangeDescriptionZonaNow()
    {
        languageID = PlayerPrefs.GetInt("LocaleKey");
        notification1.SetActive(true);
        notification2.SetActive(true);
        if (languageID == 0)
        {
            DisplayDescriptionZonaEN();
        }
        else
        {
            DisplayDescriptionZonaID();
        }
    }

    // Fungsi untuk menampilkan deskripsi zona dalam bahasa Indonesia
    void DisplayDescriptionZonaID()
    {
        titleText.text = zonaData[zonaNow].namaZona;
        descriptionText.text = zonaData[zonaNow].deskripsiZona;
        audioClip = zonaData[zonaNow].suaraPenjelasanZona;
    }

    // Fungsi untuk menampilkan deskripsi zona dalam bahasa Inggris
    void DisplayDescriptionZonaEN()
    {
        titleText.text = zonaData[zonaNow].zonaName;
        descriptionText.text = zonaData[zonaNow].zonaDescription;
        audioClip = zonaData[zonaNow].soundDescriptionZona;
    }

    // Ketika collider memasuki zona tertentu
    private void OnTriggerEnter(Collider other)
    {
        string gameObjectName = other.gameObject.name;

        // Memeriksa nama game object yang memasuki collider
        if (gameObjectName == "Zona Teritorial")
        {
            zonaNow = 0;
            ChangeDescriptionZonaNow();
        }
        else if (gameObjectName == "Zona Tambahan")
        {
            zonaNow = 1;
            ChangeDescriptionZonaNow();
        }
        else if (gameObjectName == "Zona Ekslusif Ekonomi")
        {
            zonaNow = 2;
            ChangeDescriptionZonaNow();
        }
    }

    // Fungsi untuk mengatur pemutaran atau penghentian musik
    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }

        isMusicPlaying = !isMusicPlaying; // Mengubah status pemutaran musik
    }

    public void ToggleDescription()
    {
        if (descriptionImage.activeSelf)
        {
            descriptionImage.SetActive(false);
        }
        else
        {
            descriptionImage.SetActive(true);
        }
    }
}
