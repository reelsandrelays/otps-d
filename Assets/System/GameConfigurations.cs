using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameConfigurationsData
{
    public LanguageType Language;
    public GraphicLevel Graphics;
    public bool BGMOn;
    public bool SFXOn;
}

[CreateAssetMenu(fileName = "", menuName = "Game Configurations")]
public class GameConfigurations : ScriptableObject
{
    [SerializeField] private GameConfigurationsData configurationsData;

    [Header("Initial Settings")]
    public LanguageType language;
    public bool bGMOn;
    public bool sFXOn;

    [Header("Development")]
    public string version;

    public GameConfigurationsData ConfigurationsData { get { return configurationsData; } }

    public LanguageType Language
    {
        get { return configurationsData.Language; }
        set
        {
            configurationsData.Language = value;
            Save();
        }
    }
    public GraphicLevel Graphics
    {
        get { return configurationsData.Graphics; }
        set
        {
            configurationsData.Graphics = value;
            Save();
        }
    }
    public bool BGMOn
    {
        get { return configurationsData.BGMOn; }
        set
        {
            configurationsData.BGMOn = value;
            Save();
        }
    }
    public bool SFXOn
    {
        get { return configurationsData.SFXOn; }
        set
        {
            configurationsData.SFXOn = value;
            Save();
        }
    }

    private void Save()
    {
        string path = Application.persistentDataPath + "/gameconfig.gcf";
        string contents = JsonUtility.ToJson(configurationsData);
        File.WriteAllText(path, contents);
    }

    private void OnEnable()
    {
        string path = Application.persistentDataPath + "/gameconfig.gcf";

        if (!File.Exists(path))
        {
            configurationsData = new GameConfigurationsData()
            {
                Language = language,
                Graphics = GraphicLevel.Medium,
                BGMOn = bGMOn,
                SFXOn = sFXOn
            };

            string contents = JsonUtility.ToJson(configurationsData);
            File.WriteAllText(path, contents);
        }
        else
        {
            string json = File.ReadAllText(path);
            configurationsData = JsonUtility.FromJson<GameConfigurationsData>(json);
        }
    }
}

public class Attribute_Graphics : PropertyAttribute { }

public enum GraphicLevel
{
    Low,
    Medium,
    High
}
