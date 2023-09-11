using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class CardSetting : MonoBehaviour
{
    #region 변수
    [Header("=====> 카드 세팅 <=====")]
    [SerializeField] private SpriteRenderer BackGroundCardImg; // 카드 배경 이미지
    [SerializeField] private SpriteRenderer CardMainImg; // 카드 이미지
    [SerializeField] private TMP_Text CardNameText; // 카드 이름 텍스트
    [SerializeField] private TMP_Text CardAttackText; // 카드 공격력 텍스트
    [SerializeField] private TMP_Text CardCostText; // 카드 비용 텍스트
    [SerializeField] private TMP_Text CardDesText; // 카드 설명 텍스트
    [SerializeField] private Sprite CardFront; // 카드 앞면
    [SerializeField] private Sprite CardBack; // 카드 뒷면

    [Header("=====> 카드 셋업 후 보여주는 데이터 <=====")]
    [SerializeField] private CardData oCardData; // 카드에 담겨있는 내용 확인용 변수

    private bool IsFront = true; // 앞면, 뒷면 확인용 변수
    #endregion // 변수

    #region 프로퍼티
    public PRS OriginPRS { get; set; } // 유틸 함수를 이용한 Position, Rotation, Scale
    public CardData CardSettingData => oCardData;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 >> 마우스를 올렸을 때*/
    private void OnMouseOver()
    {
        if (IsFront == true)
        {
            CardManager.Inst.CardMouseOver(this);
        }
    }

    /** 초기화 >> 마우스를 눌렀을 때 */
    private void OnMouseDown()
    {
        if (IsFront == true)
        {
            CardManager.Inst.CardMouseDown();
        }
    }

    /** 초기화 >> 마우스를 눌렀다가 때는 순간 */
    private void OnMouseUp()
    {
        if (IsFront == true)
        {
            CardManager.Inst.CardMouseUp();
        }
    }

    /** 초기화 >> 마우스가 나갔을 때 */
    private void OnMouseExit()
    {
        if(IsFront == true)
        {
            CardManager.Inst.CardMouseExit(this);
        }
    }

    /** 카드를 세팅한다 */
    public void CardSetup(CardData oCardData, bool IsFront)
    {
        this.oCardData = oCardData;
        this.IsFront = IsFront;

        // 카드 앞면, 뒷면 세팅
        if(this.IsFront == true)
        {
            CardMainImg.sprite = this.oCardData.CardSprite;
            CardNameText.text = this.oCardData.CardName;
            CardDesText.text = this.oCardData.CardDesc;
            CardAttackText.text = this.oCardData.CardAttack.ToString();
            CardCostText.text = this.oCardData.CardCost.ToString();
        }
        else if(this.IsFront == false)
        {
            BackGroundCardImg.sprite = CardBack;
            CardNameText.text = "";
            CardDesText.text = "";
            CardAttackText.text = "";
            CardCostText.text = "";
        }
    }

    /** 카드 움직임을 세팅한다 */
    public void MoveTransform(PRS oPRS, bool UseDotween, float DotweenTime = 0)
    {
        // Dotween을 사용해 움직인다
        if (UseDotween == true)
        {
            transform.DOMove(oPRS.Position, DotweenTime);
            transform.DORotateQuaternion(oPRS.Rotation, DotweenTime);
            transform.DOScale(oPRS.Scale, DotweenTime);
        }
        else // 평범하게 움직인다
        {
            transform.position = oPRS.Position;
            transform.rotation = oPRS.Rotation;
            transform.localScale = oPRS.Scale;
        }
    }
    #endregion // 함수
}
