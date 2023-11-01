using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
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
    [SerializeField] private TMP_Text PlayerManaTextUI = null; // 상단바 HP 텍스트

    [Header("=====> Player Mana Slider <=====")]
    [SerializeField] private GameObject IntegratedManaObject = null; // 통합 마나 오브젝트
    [SerializeField] private Slider PlayerManaSlider = null; // 마나 슬라이더
    [SerializeField] private TMP_Text PlayerManaText = null; // 마나 텍스트

    [Header("=====> Player Sprite Parts <=====")]
    [SerializeField] private SpriteRenderer Body = null;
    [SerializeField] private SpriteRenderer Head = null;
    [SerializeField] private SpriteRenderer Arms = null;
    [SerializeField] private SpriteRenderer ArmRight = null;

    [Header("=====> Player Position <=====")]
    [SerializeField] private Vector3 BasicPlayerPos = new Vector3(-5.0f, 0.7f, 0f); // 플레이어 고정 위치

    private HitRender HitRender = null;

    private PlayerData oPlayerData;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        oPlayerData = GetComponent<PlayerData>();
        IntegratedManaObject.SetActive(true); // 객체 활성화
        PlayerSlider.SetActive(true);
        HitRender = GetComponent<HitRender>();
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

    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Enemy_Disappear_Type"))
        {
            HitRender.HitRenderer(Body, Head, Arms, ArmRight, this.transform.position + Vector3.left, 0.1f);
        }
        else if (collision.gameObject.CompareTag("Enemy_Continuous_Type"))
        {
            var Particle = collision.gameObject.GetComponent<ParticleSystem>();
            var ParticleMain = Particle.main;
            var ParticleDuration = ParticleMain.duration;
            HitRender.UseHitContinuousRenderer(Body, Head, Arms, ArmRight, ParticleDuration, 0.1f);
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Enemy_Disappear_Type"))
        {
            HitRender.ExitHitRenderer(Body, Head, Arms, ArmRight, BasicPlayerPos, 0.1f);
        }

        if (collision.gameObject.CompareTag("Enemy_Continuous_Type"))
        {
            HitRender.ExitHitRenderer(Body, Head, Arms, ArmRight, BasicPlayerPos, 0.1f);
        }
    }

    /** 체력 슬라이더를 세팅한다 */
    private void SetupHpSlider()
    {
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        PlayerHpSliderRect.position = ScreenPos + Distance;

        PlayerHpSlider.maxValue = GameManager.Inst.oPlayerMaxHp; ;
        PlayerHpSlider.value = oPlayerData.oCurrentHp;
        PlayerHpSliderText.text = (oPlayerData.oCurrentHp.ToString() + "/" + GameManager.Inst.oPlayerMaxHp.ToString());
    }

    /** 마나 슬라이더를 세팅한다 */
    private void SetUpManaSlider()
    {
        PlayerManaSlider.maxValue = GameManager.Inst.oPlayerMaxCost;
        PlayerManaSlider.value = oPlayerData.oCurrentCost;
        PlayerManaText.text = (oPlayerData.oCurrentCost.ToString() + "/" + GameManager.Inst.oPlayerMaxCost.ToString());
    }

    /** 상단 바 UI를 세팅한다 */
    private void TopUISetup()
    {
        PlayerHpTextUI.text = (oPlayerData.oCurrentHp.ToString() + "/" + GameManager.Inst.oPlayerMaxHp.ToString());
        PlayerManaTextUI.text = PlayerManaText.text = (oPlayerData.oCurrentCost.ToString() + "/" + GameManager.Inst.oPlayerMaxCost.ToString());
    }
    #endregion // 함수
}
