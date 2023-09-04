using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class EnemySetting : MonoBehaviour
{
    #region 변수
    [Header("=====> 적 정보 <=====")]
    [SerializeField] private float MaxHp = 0f; // 최대 체력
    [SerializeField] private float CurrentHp = 0.0f; // 현재 체력
    [SerializeField] private SpriteRenderer EnemySprite; // 적 스프라이트
    [SerializeField] private Vector3 EnemyScale; // 적 크기

    [Header("=====> 적 데이터 셋업 후 보여지는 데이터 <=====")]
    [SerializeField] private EnemyDataSetting EnemyDataSet = null;
    #endregion // 변수

    #region 프로퍼티
    public float oCurrentHp
    {
        get => CurrentHp;
        set => CurrentHp = Mathf.Max(0, value);
    }
    #endregion // 프로퍼티

    #region 함수
    private void Awake()
    {
        // 적 설정
        EnemyManager.Instance.SeletedEnemy(this);
    }

    /** 적 데이터를 세팅한다 */
    public void EnemySetup(EnemyDataSetting EnemyDataSetup)
    {
        this.EnemyDataSet = EnemyDataSetup;

        // 스프라이트 교체, 크기 변경
        this.EnemySprite.sprite = EnemyDataSetup.oEnemySprite;
        this.transform.localScale = EnemyScale;

        // 체력 세팅
        this.MaxHp = EnemyDataSetup.MaxHp;
        this.CurrentHp = this.MaxHp;
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float Damage)
    {
        oCurrentHp -= Damage;
    }
    #endregion // 함수
}
