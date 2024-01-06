using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    #region 변수
    [Header("=====> 카드 패널 정보 <=====")]
    [SerializeField] private GameObject CardInfoImgPanel; // 카드 패널 이미지
    [SerializeField] private TMP_Text CardInfoNameText; // 카드 패널 이름 텍스트
    [SerializeField] private TMP_Text CardInfoATKText; // 카드 패널 공격력 텍스트
    [SerializeField] private TMP_Text CardInfoCostText; // 카드 패널 코스트 텍스트
    [SerializeField] private TMP_Text CardInfoDescText; // 카드 패널 설명 텍스트

    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    #region 함수
    /** 카드 패널을 활성화 시킨다 */
    public void CardInfoPanel(bool IsActive)
    {
        if (IsActive)
        {// 선택된 카드가 있을 경우
            if (CardManager.Instance.SelectCard != null)
            {
                CardInfoImgPanel.SetActive(true);
                CardPanelSetup();
            }
        }
        else
        {
            CardInfoImgPanel.SetActive(false);
        }
          
    }

    /** 카드 패널 정보를 업데이트 한다 */
    public void CardPanelSetup()
    {
        CardInfoNameText.text = CardManager.Instance.SelectCard.CardSettingData.CardName;
        CardInfoATKText.text = "공격력 : " + CardManager.Instance.SelectCard.CardSettingData.CardAttack.ToString();
        CardInfoCostText.text = "마나 :" + CardManager.Instance.SelectCard.CardSettingData.CardCost.ToString();
        CardInfoDescText.text = "설명" + "\n" + CardManager.Instance.SelectCard.CardSettingData.CardDesc;
    }
    #endregion // 함수
}
