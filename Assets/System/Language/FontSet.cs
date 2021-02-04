using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Language/Font Set", order = 0)]
public class FontSet : ScriptableObject
{
    [SerializeField] private Font normal;
    [SerializeField] private Font bold;

    public Font GetFont(bool bold) { return (bold == true) ? this.bold : normal; }
}
