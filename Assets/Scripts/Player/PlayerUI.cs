using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region 변수
    [Header("=====> Player Hp Slider <=====")]
    [SerializeField] private GameObject SliderHpObject = null; // 슬라이더 HP 오브젝트
    [SerializeField] private Slider PlayerHpSlider = null; // HP 슬라이더
    [SerializeField] private TMP_Text PlayerHpText = null; // HP 텍스트
    [SerializeField] private GameObject PlayerImg = null; // 대상으로 설정할 플레이어
    [SerializeField] private float HpDistancefloat = 0; // 슬라이더 거리 조정 변수
    [SerializeField] private Vector3 HpDistance = Vector3.zero; // 플레이어와 HP슬라이더 사이의 거리 

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
        SliderHpObject.SetActive(true); // 객체 활성화
    }

    /** 상태를 갱신한다 */
    private void Update()
    {
        SliderHpObject.GetComponent<SliderPositionAuto>().Setup(PlayerImg.transform, SetupHpSliderDistance());
        SetUpManaSlider();
        SetupHpSlider();
        TopUISetup();
    }

    /** 체력 슬라이더 위치를 조정한다 */
    public Vector3 SetupHpSliderDistance()
    {
        HpDistance = Vector3.down * HpDistancefloat;
        return HpDistance;
    }

    /** 마나 슬라이더를 세팅한다 */
    private void SetUpManaSlider()
    {
        PlayerManaSlider.maxValue = oPlayerData.oMaxCost;
        PlayerManaSlider.value = oPlayerData.oCurrentCost;
        PlayerManaText.text = (oPlayerData.oCurrentCost.ToString() + "/" + oPlayerData.oMaxCost.ToString());
    }

    /** 체력 슬라이더를 세팅한다 */
    private void SetupHpSlider()
    {
        PlayerHpSlider.maxValue = oPlayerData.oMaxHp;
        PlayerHpSlider.value = oPlayerData.oCurrentHp;
        PlayerHpText.text = (oPlayerData.oCurrentHp.ToString() + "/" + oPlayerData.oMaxHp.ToString());
    }

    /** 상단 바 UI를 세팅한다 */
    private void TopUISetup()
    {
        PlayerHpTextUI.text = (oPlayerData.oCurrentHp.ToString() + "/" + oPlayerData.oMaxHp.ToString());
        PlayerGoldTextUI.text = (oPlayerData.oGold.ToString());
    }
    #endregion // 함수
}
