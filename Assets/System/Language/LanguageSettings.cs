using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    English,
    Korean,

    Count
}

public class Attribute_Language : PropertyAttribute { }

[CreateAssetMenu(fileName = "", menuName = "Language/Language Settings", order = 0)]
public class LanguageSettings : ScriptableObject
{
    [SerializeField] private GameConfigurations gameConfigurations;
    [SerializeField] [Attribute_Language] private FontSet[] fontSet;

    public FontSet FontSet { get { return fontSet[(int)CurrentLanguage]; } }
    public LanguageType CurrentLanguage { get { return gameConfigurations.ConfigurationsData.Language; } }
}
