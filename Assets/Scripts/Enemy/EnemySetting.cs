using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    #region 변수
    [Header("=====> 적 정보 <=====")]
    [SerializeField] private float MaxHp = 0f; // 최대 체력
    [SerializeField] private float CurrentHp = 0.0f; // 현재 체력
    [SerializeField] private SpriteRenderer EnemySprite; // 적 스프라이트

    [Header("=====> 적 데이터 셋업 후 보여지는 데이터 <=====")]
    [SerializeField] private EnemyDataSetting EnemyDataSet = null;
    #endregion // 변수

    #region 프로퍼티
    public float oCurrentHp
    {
        get { return CurrentHp; }
        set { CurrentHp = value; }
    }
    #endregion // 프로퍼티

    #region 함수
    /** 적 데이터를 세팅한다 */
    public void EnemySetup(EnemyDataSetting EnemyDataSetup)
    {
        this.EnemyDataSet = EnemyDataSetup;

        // 스프라이트 교체, 크기 변경
        this.EnemySprite.sprite = EnemyDataSetup.oEnemySprite;
        this.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        // 체력 세팅
        this.MaxHp = EnemyDataSetup.MaxHp;
        this.CurrentHp = this.MaxHp;
    }
    #endregion // 함수
}
