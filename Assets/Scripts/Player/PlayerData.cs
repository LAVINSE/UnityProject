using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    #region 변수
    [Header("=====> Player Data <=====")]
    [SerializeField] private float MaxHp = 20.0f; // 최대 체력
    [SerializeField] private float CurrentHp = 0; // 현재 체력
    [SerializeField] private float MaxCost = 5; // 최대 코스트
    [SerializeField] private float CurrentCost = 0; // 현재 코스트
    [SerializeField] private float CurrentGold = 50; // 현재 골드
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

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {

    }
    #endregion // 함수
}
