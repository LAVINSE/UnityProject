using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBasicData
{
    public GameObject oEnemyObject;
    public float MaxHp;
    public float ATK;
}

[CreateAssetMenu]
public class EnemyBasic : ScriptableObject
{
    public EnemyBasicData[] MainList;
}