using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMain : MonoBehaviour
{
    #region 변수
    [SerializeField] private Vector3 BasicEnemyPos; // 적 고정 위치

    private SpriteRenderer EnemySprite;
    private EnemySetting oEnemySetting;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        EnemySprite = GetComponent<SpriteRenderer>();
        oEnemySetting = GetComponent<EnemySetting>();
    }

    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if(collision.gameObject.CompareTag("Disappear_Type"))
        {
            StartCoroutine(EnemyHitRender(0.05f));
            Debug.Log($"{collision.gameObject.name}");        
        }
        else if(collision.gameObject.CompareTag("Continuous_Type"))
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
        if (collision.gameObject.CompareTag("Disappear_Type"))
        {
            EnemyOriginPos();
        }

        if(collision.gameObject.CompareTag("Continuous_Type"))
        {
            EnemyOriginPos();
        }
    }

    /** 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitRender(float WaitSeconds)
    {
        EnemySprite.color = Color.red;
        yield return new WaitForSeconds(WaitSeconds);
        EnemySprite.color = Color.white;
    }

    /** 파티클 지속시간동안 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitContinuousRender(float ParticleTime, float WaitSeconds)
    {
        while(ParticleTime > 0)
        {
            EnemySprite.color = Color.red;
            yield return new WaitForSeconds(WaitSeconds);
            EnemySprite.color = Color.white;
            yield return new WaitForSeconds(ParticleTime/ ParticleTime);
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
