using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMain : MonoBehaviour
{
    #region 변수
    [SerializeField] private Vector3 BasicEnemyPos;

    private SpriteRenderer EnemySprite;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        EnemySprite = GetComponent<SpriteRenderer>();
        BasicEnemyPos = this.transform.position;
    }

    /** 초기화 >> 접촉했을 때*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if(collision.gameObject.CompareTag("CardSpell"))
        {
            StartCoroutine(EnemyHitRender(2.0f, 0.1f));
            Debug.Log($"{collision.gameObject.name}");
        }
        else if(collision.gameObject.CompareTag("CardSpellCirCle"))
        {
            StartCoroutine(EnemyHitRender(4.0f, 1.0f));
            Debug.Log($"{collision.gameObject.name}");
        }
    }

    /** 초기화 >> 접촉이 끝났을 때*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 파티클과 접촉 했을 경우
        if (collision.gameObject.CompareTag("CardSpell") || collision.gameObject.CompareTag("CardSpellCirCle"))
        {
            EnemyOriginPos();
        }
    }

    /** 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitRender(float CounTimeEnd, float WaitForseconds)
    {
        int CountTime = 0;
        while (CountTime < CounTimeEnd)
        {
            if (CountTime % 2 == 0)
            {
                EnemySprite.color = new Color32(255, 255, 255, 90);

            }
            else
            {
                EnemySprite.color = new Color32(255, 255, 255, 180);
            }

            yield return new WaitForSeconds(WaitForseconds);
            CountTime++;
        }
        EnemySprite.color = new Color(255, 255, 255, 250);
    }

    /** 적을 원래 위치로 되돌린다 */
    private void EnemyOriginPos()
    {
        transform.DOMove(BasicEnemyPos, 0.1f);
    }
    #endregion // 함수
}
