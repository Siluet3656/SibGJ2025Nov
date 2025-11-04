using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    public static SystemLanguage currentLanguage;

    public LocalizationData data;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public string Translate(string key)
    {
        return data.GetTranslation(key, currentLanguage);
    }

    public void SetLanguage(SystemLanguage language)
    {
        currentLanguage = language;
        // Если нужно, можно триггерить обновление UI
    }
}