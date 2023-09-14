using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropCard : MonoBehaviour
{
    #region 변수
    [Header("=====> Card ScriptTableList Add Deck <=====")]
    [SerializeField] private List<CardScirptTable> CardTableAdd = new List<CardScirptTable>();
    
    [Header("=====> Card Option <=====")]
    [SerializeField] private Image CardMainImg;
    [SerializeField] private TMP_Text Card_Name;
    [SerializeField] private TMP_Text Card_Desc;
    [SerializeField] private TMP_Text Card_Attack;
    [SerializeField] private TMP_Text Card_Cost;

    private List<CardScirptTable> CardBuffer = new List<CardScirptTable>();
    #endregion // 변수

    #region 함수
    /** 카드 정보를 이미지에 세팅한다 */
    private void Setup(CardScirptTable CardSript)
    {
        CardMainImg.sprite = CardSript.CardSprite;
        Card_Name.text = CardSript.CardName;
        Card_Desc.text = CardSript.CardDesc;
        Card_Attack.text = CardSript.CardAttack.ToString();
        Card_Cost.text = CardSript.CardCost.ToString();
    }

    private void Randome()
    {
        for(int i = 0; i< CardTableAdd.Count; i++)
        {;
            CardScirptTable Data = CardTableAdd[i];
            CardBuffer.Add(Data);
        }
    }
    #endregion // 함수
}
