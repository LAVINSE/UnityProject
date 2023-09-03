using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDataSetting
{
    public enum EnemyType
    {
        BasicEnemy,
        EliteEnemy,
        BossEnemy,
    }

    public Sprite oEnemySprite;
    public EnemyType oEnemyType;
    public string oEnemyName;
    public float MaxHp;
    public float ATK;
}

[CreateAssetMenu]
public class EnemyDataMain : ScriptableObject
{
    public EnemyDataSetting[] MainList;
}
