using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellManager : CSingleton<SpellManager>
{
    #region 변수
    [SerializeField] private SpellCard PlayerSpell;
    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    #region 함수 
    /** 효과 카드 사용이 되었는지 확인하고 사용한다 */
    public void EffectCardSpawn(CardData.oCardEffect CardEffect)
    {
        switch(CardEffect)
        {
            case CardData.oCardEffect.NONE:
                break;
            case CardData.oCardEffect.DRAWCARD:
                PlayerSpell.DrawCard();
                break;
            case CardData.oCardEffect.ATTACKCARD:
                PlayerSpell.AttacDamageCard();
                break;
            case CardData.oCardEffect.MagicCircleSnowCard:
                PlayerSpell.MagicCircleSnowCard();
                break;
            default:
                break;
        }
    }
    #endregion // 함수
}
