using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ZonaData", menuName = "Zona Data", order = 1)]
public class ZonaScriptableObject : ScriptableObject
{
    public string zonaID;

    public string zonaName;
    [TextArea(3, 5)]
    public string zonaDescription;
    public AudioClip soundDescriptionZona;

    public string namaZona;
    [TextArea(3, 5)]
    public string deskripsiZona;
    public AudioClip suaraPenjelasanZona;
}
