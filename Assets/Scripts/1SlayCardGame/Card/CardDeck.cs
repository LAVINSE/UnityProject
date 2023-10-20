using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : CSingleton<CardDeck>
{
    #region 변수
    [Header("=====> 기본 Scriptable Objects <=====")]
    [SerializeField] private List<CardScirptTable> CardBasicTableDeck = new List<CardScirptTable>();
    #endregion // 변수

    #region 프로퍼티
    public List<CardScirptTable> oCardBasicTableDeck
    {
        get => CardBasicTableDeck;
        set => CardBasicTableDeck = value;
    }
    #endregion // 프로퍼티

    #region 함수
    #endregion // 함수
}