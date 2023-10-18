using DG.Tweening;
using DG.Tweening.Core.Easing;
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

    [Header("=====> 적 데이터 셋업 후 보여지는 데이터 <=====")]
    [SerializeField] private EnemyBasicData EnemyDataSet = null;

    private bool IsEnemyDie = false;
    #endregion // 변수

    #region 프로퍼티
    public float oMaxHp
    {
        get => MaxHp;
        set => MaxHp = Mathf.Max(0, value);
    }

    public float oCurrentHp
    {
        get => CurrentHp;
        set => CurrentHp = Mathf.Max(0, value);
    }

    public bool oIsEnemyDie
    {
        get => IsEnemyDie;
        set => IsEnemyDie = value;
    }

    public EnemyBasicData oEnemyDataSet => EnemyDataSet;
    #endregion // 프로퍼티

    #region 함수
    private void Awake()
    {
        // 적 설정
        EnemyManager.Instance.SeletedEnemy(this);
        oIsEnemyDie = false;
    }

    /** 적 데이터를 세팅한다 */
    public void EnemySetup(EnemyBasicData EnemyDataSetup)
    {
        this.EnemyDataSet = EnemyDataSetup;

        // 체력 세팅
        this.MaxHp = EnemyDataSetup.MaxHp;
        this.CurrentHp = this.MaxHp;
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float Damage)
    {
        oCurrentHp -= Damage;

        if(CurrentHp <= 0)
        {
            IsEnemyDie = true;
            StartCoroutine(EnemyOnDie());
        }
    }

    /** 적 죽음 처리를 한다 */
    private IEnumerator EnemyOnDie()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySFX(AudioManager.SFXEnum.GameOver);

        yield return new WaitForSeconds(2.0f);
        // 드랍 아이템 창 보여주기
        if (IsEnemyDie == true)
        {
            UIManager.Instance.ShowDropUI();
        }

        // 사망 애니메이션

        // 객체 비활성화
        this.gameObject.SetActive(false);
    }
    #endregion // 함수
}
