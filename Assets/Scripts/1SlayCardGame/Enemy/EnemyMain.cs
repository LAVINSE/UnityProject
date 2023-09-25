using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMain : MonoBehaviour
{
    #region 변수
    [Header("=====> Enemy Data Position <=====")]
    [SerializeField] private Vector3 BasicEnemyPos; // 적 고정 위치

    [Header("=====> Player Sprite Parts <=====")]
    [SerializeField] private SpriteRenderer Mount = null;
    [SerializeField] private SpriteRenderer Body = null;
    [SerializeField] private SpriteRenderer WingRight = null;
    [SerializeField] private SpriteRenderer WingLeft = null;
    #endregion // 변수

    #region 함수
    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if(collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            EnemyHitRender();    
        }
        else if(collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            var Particle = collision.gameObject.GetComponent<ParticleSystem>();
            var ParticleMain = Particle.main;
            var ParticleDuration = ParticleMain.duration;
            StartCoroutine(EnemyHitContinuousRender(ParticleDuration, 0.1f));
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            EnemyExitHitRender();
        }

        if(collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            EnemyExitHitRender();
        }
    }

    /** 적 피격 효과를 생성한다 */
    private void EnemyHitRender()
    {
        Mount.color = Color.red;
        Body.color = Color.red;
        WingRight.color = Color.red;
        WingLeft.color = Color.red;

        transform.DOMove(this.transform.position + Vector3.right, 0.1f);
    }

    /** 적 피격 효과를 종료한다 */
    private void EnemyExitHitRender()
    {
        Mount.color = Color.white;
        Body.color = Color.white;
        WingRight.color = Color.white;
        WingLeft.color = Color.white;

        transform.DOMove(BasicEnemyPos, 0.1f);
    }

    /** 파티클 지속시간동안 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitContinuousRender(float ParticleTime, float WaitSeconds)
    {
        while(ParticleTime > 0)
        {
            Mount.color = Color.red;
            Body.color = Color.red;
            WingRight.color = Color.red;
            WingLeft.color = Color.red;
            this.transform.DORotate(new Vector3(0, 0, -45), 0.5f);
            yield return new WaitForSeconds(WaitSeconds);
            this.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            Mount.color = Color.white;
            Body.color = Color.white;
            WingRight.color = Color.white;
            WingLeft.color = Color.white;
            yield return new WaitForSeconds(ParticleTime/ ParticleTime);
            ParticleTime--;
        }
    }
    #endregion // 함수
}
