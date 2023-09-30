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

    private HitRender HitRender = null;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    public void Awake()
    {
        HitRender = GetComponent<HitRender>();
    }

    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if(collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            HitRender.HitRenderer(Mount, Body, WingRight, WingLeft , this.transform.position + Vector3.right , 0.1f);
        }
        else if(collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            var Particle = collision.gameObject.GetComponent<ParticleSystem>();
            var ParticleMain = Particle.main;
            var ParticleDuration = ParticleMain.duration;
            HitRender.UseHitContinuousRenderer(Mount, Body, WingRight, WingLeft, ParticleDuration, 0.1f);
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("Player_Disappear_Type"))
        {
            HitRender.ExitHitRenderer(Mount, Body, WingRight, WingLeft, BasicEnemyPos, 0.1f);
        }

        if(collision.gameObject.CompareTag("Player_Continuous_Type"))
        {
            HitRender.ExitHitRenderer(Mount, Body, WingRight, WingLeft, BasicEnemyPos, 0.1f);
        }
    }
    #endregion // 함수
}
