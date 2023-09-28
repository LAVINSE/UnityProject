using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitRender : MonoBehaviour
{
    #region 함수
    /** 적 피격 효과를 생성한다 */
    public void EnemyHitRender(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, Vector3 MovePos)
    {
        FirstRender.color = Color.red;
        SecondRender.color = Color.red;
        ThirdRender.color = Color.red;
        FourthRender.color = Color.red;

        transform.DOMove(MovePos, 0.1f);
    }

    /** 적 피격 효과를 종료한다 */
    public void EnemyExitHitRender(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, Vector3 OriginPos)
    {
        FirstRender.color = Color.white;
        SecondRender.color = Color.white;
        ThirdRender.color = Color.white;
        FourthRender.color = Color.white;

        transform.DOMove(OriginPos, 0.1f);
    }

    /** 파티클 지속시간동안 적 피격 효과를 생성한다 */
    private IEnumerator EnemyHitContinuousRender(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, float ParticleTime, float WaitSeconds)
    {
        while (ParticleTime > 0)
        {
            FirstRender.color = Color.red;
            SecondRender.color = Color.red;
            ThirdRender.color = Color.red;
            FourthRender.color = Color.red;
            this.transform.DORotate(new Vector3(0, 0, -45), 0.5f);
            yield return new WaitForSeconds(WaitSeconds);
            this.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            FirstRender.color = Color.white;
            SecondRender.color = Color.white;
            ThirdRender.color = Color.white;
            FourthRender.color = Color.white;
            yield return new WaitForSeconds(ParticleTime / ParticleTime);
            ParticleTime--;
        }
    }
    #endregion // 함수
}
