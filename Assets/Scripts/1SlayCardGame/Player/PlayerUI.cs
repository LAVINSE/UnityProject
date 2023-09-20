using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region 변수
    [Header("=====> Player Hp Slider <=====")]
    [SerializeField] private Slider PlayerHpSlider = null; // HP 슬라이더
    [SerializeField] private GameObject PlayerSlider = null; // HP 슬라이더 객체
    [SerializeField] private TMP_Text PlayerHpSliderText = null; // HP 슬라이더 텍스트
    [SerializeField] private RectTransform PlayerHpSliderRect = null; // HP 슬라이더 객체
    [SerializeField] private Vector3 Distance = Vector3.zero;

    [Header("=====> Player TopUI <=====")]
    [SerializeField] private TMP_Text PlayerHpTextUI = null; // 상단바 HP 텍스트
    [SerializeField] private TMP_Text PlayerGoldTextUI = null; // 상단바 Gold 텍스트

    [Header("=====> Player Mana Slider <=====")]
    [SerializeField] private GameObject IntegratedManaObject = null; // 통합 마나 오브젝트
    [SerializeField] private Slider PlayerManaSlider = null; // 마나 슬라이더
    [SerializeField] private TMP_Text PlayerManaText = null; // 마나 텍스트

    private PlayerData oPlayerData;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        oPlayerData = GetComponent<PlayerData>();
        IntegratedManaObject.SetActive(true); // 객체 활성화
        PlayerSlider.SetActive(true);
    }

    /** 상태를 갱신한다 */
    private void Update()
    {
        if(this.gameObject.activeSelf == true)
        {
            SetupHpSlider();
            SetUpManaSlider();
            TopUISetup();
        }  
    }

    /** 체력 슬라이더를 세팅한다 */
    private void SetupHpSlider()
    {
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        PlayerHpSliderRect.position = ScreenPos + Distance;

        PlayerHpSlider.maxValue = oPlayerData.oMaxHp;
        PlayerHpSlider.value = oPlayerData.oCurrentHp;
        PlayerHpSliderText.text = (oPlayerData.oCurrentHp.ToString() + "/" + oPlayerData.oMaxHp.ToString());
    }

    /** 마나 슬라이더를 세팅한다 */
    private void SetUpManaSlider()
    {
        PlayerManaSlider.maxValue = oPlayerData.oMaxCost;
        PlayerManaSlider.value = oPlayerData.oCurrentCost;
        PlayerManaText.text = (oPlayerData.oCurrentCost.ToString() + "/" + oPlayerData.oMaxCost.ToString());
    }

    /** 상단 바 UI를 세팅한다 */
    private void TopUISetup()
    {
        PlayerHpTextUI.text = (oPlayerData.oCurrentHp.ToString() + "/" + oPlayerData.oMaxHp.ToString());
        PlayerGoldTextUI.text = (oPlayerData.oGold.ToString());
    }
    #endregion // 함수
}
