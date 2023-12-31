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
    [SerializeField] private float MaxHp = 0.0f; // 최대 체력
    [SerializeField] private float CurrentHp = 0.0f; // 현재 체력
    [SerializeField] private float Atk = 0.0f;
    [SerializeField] private CardDropScirptTable EnemyDrop = null;

    [Header("=====> 적 데이터 셋업 후 보여지는 데이터 <=====")]
    [SerializeField] private EnemyBasicData EnemyDataSet = null;
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

    public float oAtk
    {
        get => Atk;
        set => Atk = Mathf.Max(0, value);
    }

    public CardDropScirptTable oEnemyDrop => EnemyDrop;
    #endregion // 프로퍼티

    #region 함수
    private void Awake()
    {
        // 적 설정
        EnemyManager.Instance.SeletedEnemy(this);
    }

    /** 적 데이터를 세팅한다 */
    public void EnemySetup(EnemyBasicData EnemyDataSetup)
    {
        this.EnemyDataSet = EnemyDataSetup;

        // 체력 세팅
        this.MaxHp = EnemyDataSetup.MaxHp;
        this.CurrentHp = this.MaxHp;
        this.Atk = EnemyDataSetup.ATK;
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float Damage)
    {
        oCurrentHp -= Damage;

        if(CurrentHp <= 0)
        {
            TurnManager.Instance.oIsEnemyDie = true;
            StartCoroutine(EnemyOnDie());
        }
    }

    /** 적 죽음 처리를 한다 */
    private IEnumerator EnemyOnDie()
    {
        AudioManager.Inst.StopBGM();
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.GameOver);

        // 사망 애니메이션 >> 상태 바꾸는 함수에서 실행 예정

        yield return new WaitForSeconds(2.0f);

        // 드랍 아이템 창 보여주기
        if (TurnManager.Instance.oIsEnemyDie == true)
        {
            CSceneManager.Instance.DropUIShow();
        }      
    }
    #endregion // 함수
}
