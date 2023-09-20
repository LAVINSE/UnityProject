using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class SpellCard : MonoBehaviour
{
    #region 변수
    [Header("=====> Player <=====")]
    [SerializeField] private GameObject PlayerObject; // 플레이어 오브젝트
    [SerializeField] private GameObject PlayerSpellRoot; // 플레이어 스펠위치 오브젝트

    [Header("=====> 파티클 <=====")]
    [SerializeField] private GameObject AttackDamageParticle;
    [SerializeField] private GameObject MagicCircleSnowParticle;
    #endregion // 변수

    #region 함수
    /** 효과 카드 사용이 되었는지 확인하고 사용한다 */
    public void EffectCardSpawn(CardScirptTable.oCardEffect CardEffect)
    {
        switch (CardEffect)
        {
            case CardScirptTable.oCardEffect.NONE:
                break;
            case CardScirptTable.oCardEffect.DRAWCARD:
                DrawCard();
                break;
            case CardScirptTable.oCardEffect.ATTACKCARD:
                AttacDamageCard();
                break;
            case CardScirptTable.oCardEffect.MagicCircleSnowCard:
                MagicCircleSnowCard();
                break;
            default:
                break;
        }
    }

    /** 카드 한장을 뽑는다 */
    public void DrawCard()
    {
        TurnManager.IsOnAddCard?.Invoke(true);
    }

    /** 카드 기본 공격 */
    public void AttacDamageCard()
    {
        StopCoroutine(AttackDamage());
        StartCoroutine(AttackDamage());
    }

    /** 카드 얼음마법진을 사용한다 */
    public void MagicCircleSnowCard()
    {
        StopCoroutine(MagicCircleSnow());
        StartCoroutine(MagicCircleSnow());
    }

    /** 기본 공격 생성 */
    private IEnumerator AttackDamage()
    {
        var oCardAttack = CardManager.Instance.SelectCard.CardSettingData.CardAttack;
        var oParticle = CFactory.CreateCloneObj("Player_Disappear_Type", AttackDamageParticle, PlayerSpellRoot,
            Vector3.zero, Vector3.one, Vector3.zero);
        var EnemyPosition = EnemyManager.Instance.SelectEnemy.transform.position;

        ParticleMove(oParticle, EnemyPosition, true, 0.2f);

        yield return new WaitForSeconds(0.2f);
        EnemyManager.Instance.SelectEnemy.TakeDamage(oCardAttack);

        Destroy(oParticle);
    }

    /** 얼음마법진 생성 */
    private IEnumerator MagicCircleSnow()
    {
        var oCardAttack = CardManager.Instance.SelectCard.CardSettingData.CardAttack;
        var oParticle = CFactory.CreateCloneObj("Player_Continuous_Type", MagicCircleSnowParticle, PlayerSpellRoot,
            Vector3.zero, Vector3.one, Vector3.zero);

        // 파티클 위치 세팅
        var EnemyPosition = EnemyManager.Instance.SelectEnemy.transform.position;
        Vector3 EnemyPos = EnemyPosition + Vector3.down * 1.3f;

        // 파티클 움직이기
        ParticleMove(oParticle, EnemyPos, true, 0.2f);

        // 4초 동안 지속 피해
        int CountTime = 0;
        while (CountTime < 4)
        {
            EnemyManager.Instance.SelectEnemy.TakeDamage(oCardAttack);
            yield return new WaitForSeconds(1f);
            CountTime++;
        }

        Destroy(oParticle);
    }

    /** 파티클을 두트윈을 사용해 움직인다 */
    private void ParticleMove(GameObject ParticleObject, Vector3 EndVector3, bool IsDotween, float Time = 0)
    {
        if(IsDotween == true)
        {
            ParticleObject.transform.DOMove(EndVector3, Time);
        }
        else
        {
            ParticleObject.transform.position = EndVector3;
        }
    }
    #endregion // 함수
}
