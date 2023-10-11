using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckSetting : MonoBehaviour
{
    #region 변수 
    [SerializeField] private Image CardMainImg = null;
    [SerializeField] private TMP_Text CardNameText = null;
    [SerializeField] private TMP_Text CardDescText = null;
    [SerializeField] private TMP_Text CardAtkText = null;
    [SerializeField] private TMP_Text CardCostText = null;

    [SerializeField] private CardScirptTable CardTable = null;
    #endregion // 변수

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
    }

    /** 카드 덱을 디스폰 한다 */
    public void DespawnCardDeck()
    {
        CardManager.Instance.CardDeckListDespawn(this.gameObject);
    }
    #endregion // 함수
}
