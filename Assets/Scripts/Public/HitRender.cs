using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitRender : MonoBehaviour
{
    #region 함수
    /** 피격 효과를 생성한다 */
    public void HitRenderer(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, Vector3 MovePos, float DoMoveTime)
    {
        FirstRender.color = Color.red;
        SecondRender.color = Color.red;
        ThirdRender.color = Color.red;
        FourthRender.color = Color.red;

        transform.DOMove(MovePos, DoMoveTime);
    }

    /** 피격 효과를 종료한다 */
    public void ExitHitRenderer(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, Vector3 OriginPos, float DoMoveTime)
    {
        FirstRender.color = Color.white;
        SecondRender.color = Color.white;
        ThirdRender.color = Color.white;
        FourthRender.color = Color.white;

        transform.DOMove(OriginPos, DoMoveTime);
    }

    /** 파티클 지속시간 동안 피격 효과를 사용 생성한다 */
    public void UseHitContinuousRenderer(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
        SpriteRenderer ThirdRender, SpriteRenderer FourthRender, float ParticleTime, float WaitSeconds)
    {
        StartCoroutine(HitContinuousRenderer(FirstRender, SecondRender, ThirdRender, FourthRender, ParticleTime, WaitSeconds));
    }

    /** 파티클 지속시간동안 피격 효과를 생성한다 */
    private IEnumerator HitContinuousRenderer(SpriteRenderer FirstRender, SpriteRenderer SecondRender,
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
