using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/Localization Data")]
public class LocalizationData : ScriptableObject
{
    [System.Serializable]
    public class Entry
    {
        public string key;
        [TextArea(6, 10)]
        public string english;
        [TextArea(6, 10)]
        public string russian;
    }

    public List<Entry> entries;

    public string GetTranslation(string key, SystemLanguage language)
    {
        var entry = entries.Find(e => e.key == key);
        if (entry == null) return key; // если ключ не найден, возвращаем его

        return language == SystemLanguage.Russian ? entry.russian : entry.english;
    }
}