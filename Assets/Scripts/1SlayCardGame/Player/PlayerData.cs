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

    private bool IsPlayerDie = false;
    private Animator PlayerAnim;
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

    public bool oIsPlayerDie
    {
        get => IsPlayerDie;
        set => IsPlayerDie = value;
    }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        PlayerDataSetting();
        PlayerAnim = GetComponent<Animator>();
        PlayerAnim.SetBool("IsLive", true);
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float Damage)
    {
        oCurrentHp -= Damage;

        if (CurrentHp <= 0)
        {
            IsPlayerDie = true;
            StartCoroutine(PlayerDie());
        }
    }

    /** 플레이어 죽음 처리를 한다 */
    private IEnumerator PlayerDie()
    {
        AudioManager.Inst.StopBGM();
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.GameOver);
        PlayerAnim.SetBool("IsLive", false);

        // 플레이어가 죽었을 경우
        if (IsPlayerDie)
        {
            PlayerAnim.SetTrigger("Die");
            // TODO : 다시하기 창 띄우기
            // TODO : 메인메뉴로 돌아가는 창 띄우기
        }

        yield return null;
    }

    /** 플레이어를 데이터를 세팅한다 */
    public void PlayerDataSetting()
    {
        CurrentHp = MaxHp; // 현재 체력을 최대 체력과 같게 설정
        CurrentCost = MaxCost; // 현재 코스트를 최대 코스트와 같게 설정
    }
    #endregion // 함수
}
