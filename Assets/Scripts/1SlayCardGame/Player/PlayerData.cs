using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    #region 변수
    [Header("=====> Player Data MAX <=====")]
    [SerializeField] private float MaxHp = 20.0f; // 최대 체력
    [SerializeField] private float MaxCost = 5; // 최대 코스트

    [Header("=====> Player Data Current <=====")]
    [SerializeField] private float CurrentHp = 0; // 현재 체력
    [SerializeField] private float CurrentCost = 0; // 현재 코스트
    [SerializeField] private float CurrentGold = 50; // 현재 골드

    [Header("=====> Player Data Position <=====")]
    [SerializeField] private Vector3 BasicPlayerPos; // 플레이어 고정 위치

    [Header("=====> Player Sprite Parts <=====")]
    [SerializeField] private SpriteRenderer Body = null;
    [SerializeField] private SpriteRenderer Head = null;
    [SerializeField] private SpriteRenderer Arms = null;
    [SerializeField] private SpriteRenderer ArmRight = null;
    #endregion //변수

    #region 프로퍼티
    public float oCurrentCost
    {
        // 현재 코스트가 0 아래로 내려가지 않게 설정
        get => CurrentCost;
        set => CurrentCost = Mathf.Max(0, value);
    }

    public float oMaxCost
    {
        // 최대 코스트가 0 아래로 내려가지 않게 설정
        get => MaxCost;
        set => MaxCost = Mathf.Max(0, value);
    }

    public float oGold
    {
        // 골드가 0 아래로 내려가지 않게 설정
        get => CurrentGold;
        set => CurrentGold = Mathf.Max(0, value);
    }

    public float oMaxHp
    {
        // 체력이 0 아래로 내려가지 않게 설정
        get => MaxHp;
        set => MaxHp = Mathf.Max(0, value);
    }

    public float oCurrentHp
    {
        // 체력이 0 아래로 내려가지 않게 설정
        get => CurrentHp;
        set => CurrentHp = Mathf.Max(0, value);
    }

    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        CurrentHp = MaxHp; // 현재 체력을 최대 체력과 같게 설정
        CurrentCost = MaxCost; // 현재 코스트를 최대 코스트와 같게 설정
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float Damage)
    {
        oCurrentHp -= Damage;
    }

    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Enemy_Disappear_Type"))
        {
            PlayerHitRender();
        }
        else if (collision.gameObject.CompareTag("Enemy_Continuous_Type"))
        {
            // Do Somthing
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Enemy_Disappear_Type"))
        {
            PlayerExitHitRender();
        }

        if (collision.gameObject.CompareTag("Enemy_Continuous_Type"))
        {
            // Do Somthing
        }
    }

    // FIXME 피격 효과 부분에서 플레이어 전용으로 수정해야됨
    /** 플레이어 피격 효과를 시작한다 */
    private void PlayerHitRender()
    {
        Body.color = Color.red;
        Head.color = Color.red;
        Arms.color = Color.red;
        ArmRight.color = Color.red;

        transform.DOMove(this.transform.position + Vector3.left, 0.1f);
    }

    /** 플레이어 피격 효과를 종료한다 */
    private void PlayerExitHitRender()
    {
        Body.color = Color.white;
        Head.color = Color.white;
        Arms.color = Color.white;
        ArmRight.color = Color.white;

        transform.DOMove(BasicPlayerPos, 0.1f);
    }
    #endregion // 함수
}
