using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.Localization.Settings;

//untuk mengganti bahasa
public class SettingManager : MonoBehaviour
{
    public void ChangeLocale(int _localeId)
    {
        StartCoroutine(SetLocale(_localeId));
    }
    
    IEnumerator SetLocale(int _localeId)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeId];
    }
}
