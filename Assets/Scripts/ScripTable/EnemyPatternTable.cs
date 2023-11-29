using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class EnemyPatternData
{
    public GameObject PatternEffectObject;
    public string PatternName;
    [TextArea] public string PatternDesc;
}

[CreateAssetMenu]
public class EnemyPatternTable : ScriptableObject
{
    public EnemyPatternData[] PatternList;
}
