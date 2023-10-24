using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckSetting : MonoBehaviour
{
    #region 변수 
    [Header("=====> 카드 정보 <=====")]
    [SerializeField] private Image CardMainImg = null;
    [SerializeField] private TMP_Text CardNameText = null;
    [SerializeField] private TMP_Text CardDescText = null;
    [SerializeField] private TMP_Text CardAtkText = null;
    [SerializeField] private TMP_Text CardCostText = null;
    [SerializeField] private TMP_Text CardIndexText = null; // 중복 카드 개수 표시

    [Header("=====> 인스펙터 확인용 <=====")]
    [SerializeField] private CardScirptTable CardTable = null;

    private int Index = 1; // 기본 카드 수량
    #endregion // 변수

    #region 프로퍼티 
    #endregion // 프로퍼티

    #region 함수
    /** 카드 덱을 세팅한다 */
    public void SettingCardDeck(CardScirptTable CardTable)
    {
        this.CardTable = CardTable;

        CardMainImg.sprite = CardTable.CardSprite;
        CardNameText.text = CardTable.CardName;
        CardDescText.text = CardTable.CardDesc;
        CardAtkText.text = CardTable.CardAttack.ToString();
        CardCostText.text = CardTable.CardCost.ToString();
        CardIndexText.text = Index.ToString();
    }

    /** 중복 카드가 있을 경우 증가 */
    public void DupCardIndex(CardScirptTable CardTable)
    {
        if(this.CardTable == CardTable)
        {
            Index += 1;
        }
    }
    #endregion // 함수
}
