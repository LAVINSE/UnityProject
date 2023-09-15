using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropCard : MonoBehaviour
{
    #region 변수
    [Header("=====> Card ScriptTableList Drop Deck <=====")]
    [SerializeField] private CardDropScirptTable CardDropTable;

    [Header("=====> Card Option <=====")]
    [SerializeField] private Image CardMainImg;
    [SerializeField] private TMP_Text Card_Name;
    [SerializeField] private TMP_Text Card_Desc;
    [SerializeField] private TMP_Text Card_Attack;
    [SerializeField] private TMP_Text Card_Cost;

    [SerializeField]private List<CardScirptTable> CardBuffer = new List<CardScirptTable>();
    #endregion // 변수

    #region 함수
    /** 랜덤으로 선택된 카드를 보여준다 */
    public void ShowDropCard()
    {
        Setup(PickDropCard());
    }
    
    /** 카드 정보를 이미지에 세팅한다 */
    private void Setup(CardScirptTable CardSript)
    {
        CardMainImg.sprite = CardSript.CardSprite;
        Card_Name.text = CardSript.CardName;
        Card_Desc.text = CardSript.CardDesc;
        Card_Attack.text = CardSript.CardAttack.ToString();
        Card_Cost.text = CardSript.CardCost.ToString();
    }

    /** 랜덤 카드를 카드버퍼 리스트에 추가한다 */
    private void RandomDropCardSetting()
    {
        CardBuffer.Add(CardDropTable.ItemDrop());
    }

    /** 카드버퍼에 있는 카드를 선택하고 제거한다 */
    private CardScirptTable PickDropCard()
    {
        if(CardBuffer.Count == 0)
        {
            RandomDropCardSetting();
        }

        CardScirptTable Data = CardBuffer[0];
        return Data;
    }

    /** 선택한 카드를 추가한다 */
    public void DropCardAdd()
    {
        CardScirptTable Data = CardBuffer[0];
        CardBuffer.RemoveAt(0);
        CardManager.Inst.oCardBasicTableDeck.Add(Data);
    }

    /** 카드버퍼를 초기화 한다 */
    public void ClearCardBuffer()
    {
        CardBuffer.Clear();
    }
    #endregion // 함수
}
