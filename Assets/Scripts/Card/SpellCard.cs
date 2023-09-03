using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;

public class SpellCard : MonoBehaviour
{
    #region 변수
    [Header("=====> Player <=====")]
    [SerializeField] private GameObject PlayerObject; // 플레이어 오브젝트
    [SerializeField] private GameObject PlayerSpellRoot; // 플레이어 스펠위치 오브젝트

    [Header("=====> Enemy <=====")]
    [SerializeField] private EnemySetting oEnemyDataSetting; // 적 데이터를 가져오는 변수  
    [SerializeField] private GameObject EnemyObject; // 적 오브젝트


    [Header("=====> 파티클 <=====")]
    [SerializeField] private GameObject AttackDamageParticle;
    [SerializeField] private GameObject MagicCircleSnowParticle;

    private Vector3 ParticleBasicPos;
    #endregion // 변수

    #region 함수
    private void Awake()
    {
        ParticleBasicPos = this.transform.position;
    }

    /** 카드 한장을 뽑는다 */
    public void DrawCard()
    {
        TurnManager.IsOnAddCard?.Invoke(true);
    }

    /** 카드 기본 공격 */
    public void AttacDamageCard()
    {
        StartCoroutine(AttackDamage());
    }

    /** 카드 얼음마법진을 사용한다 */
    public void MagicCircleSnowCard()
    {
        StartCoroutine(MagicCircleSnow());
    }

    /** 기본 공격 생성 */
    private IEnumerator AttackDamage()
    {
        int oCardAttack = CardManager.Inst.SelectCard.CardSettingData.CardAttack;
        var oParticle = CFactory.CreateCloneObj("AttackDamage", AttackDamageParticle, PlayerSpellRoot,
            Vector3.zero, Vector3.one, Vector3.zero);

        ParticleMove(oParticle, EnemyObject.transform.position, true, 0.2f);

        // 0.2초 후 데미지 주고 비활성화
        yield return new WaitForSeconds(0.2f);

        oEnemyDataSetting.oCurrentHp -= oCardAttack;

        Destroy(oParticle);
        ParticleOriginPos(oParticle);
    }

    /** 얼음마법진 생성 */
    private IEnumerator MagicCircleSnow()
    {
        var oCardAttack = CardManager.Inst.SelectCard.CardSettingData.CardAttack;
        var oParticle = CFactory.CreateCloneObj("MagicCircleSnow", MagicCircleSnowParticle, PlayerSpellRoot,
            Vector3.zero, Vector3.one, Vector3.zero);
        Vector3 EnemyPos = EnemyObject.transform.position + Vector3.down * 1.3f;
        int CountTime = 0;

        ParticleMove(oParticle, EnemyPos, true, 0.2f);

        // 파티클 실행 후 모션 나오기까지 대기시간
        yield return new WaitForSeconds(0.5f);

        // 4초 동안 지속 피해
        while (CountTime < 4)
        {
            oEnemyDataSetting.oCurrentHp -= oCardAttack;
            yield return new WaitForSeconds(1f);
            CountTime++;
        }

        Destroy(oParticle);
        ParticleOriginPos(oParticle);     
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

    /** 파티클을 원래 위치로 되돌린다 */
    private void ParticleOriginPos(GameObject ParticleObject)
    {
        ParticleObject.transform.position = ParticleBasicPos;
    }
    #endregion // 함수
}
