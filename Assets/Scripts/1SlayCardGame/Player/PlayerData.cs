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

    [Header("=====> Player Data Option <=====")]
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] private Vector3 BasicEnemyPos; // 플레이어 고정 위치
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
        if (collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            StartCoroutine(EnemyHitRender(0.05f));
            Debug.Log($"{collision.gameObject.name}");
        }
        else if (collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            var Particle = collision.gameObject.GetComponent<ParticleSystem>();
            var ParticleMain = Particle.main;
            var ParticleDuration = ParticleMain.duration;
            StartCoroutine(EnemyHitContinuousRender(ParticleDuration, 0.05f));
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            EnemyOriginPos();
        }

        if (collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            EnemyOriginPos();
        }
    }

    /** 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitRender(float WaitSeconds)
    {
        PlayerSprite.color = Color.red;
        yield return new WaitForSeconds(WaitSeconds);
        PlayerSprite.color = Color.white;
    }

    /** 파티클 지속시간동안 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitContinuousRender(float ParticleTime, float WaitSeconds)
    {
        while (ParticleTime > 0)
        {
            PlayerSprite.color = Color.red;
            yield return new WaitForSeconds(WaitSeconds);
            PlayerSprite.color = Color.white;
            yield return new WaitForSeconds(ParticleTime / ParticleTime);
            ParticleTime--;
        }
    }

    /** 적을 원래 위치로 되돌린다 */
    public void EnemyOriginPos()
    {
        transform.DOMove(BasicEnemyPos, 0.1f);
    }
    #endregion // 함수
}
