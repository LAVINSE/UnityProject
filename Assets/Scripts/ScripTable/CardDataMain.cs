using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public enum oCardType
    {
        NONE,
        BUFF,
        ATTACK,
        SHIELD,
    }

    public enum oCardEffect
    { 
        NONE = 0,
        DRAWCARD,
        ATTACKCARD,
        MagicCircleSnowCard,
    }

    public Sprite CardSprite; // 카드 이미지
    public string CardName; // 카드 이름
    public float CardAttack; // 카드 공격력
    public int CardCost; // 카드 코스트 
    public int CardCount; // 카드 수
    public oCardType CardType = oCardType.NONE; // 카드 타입
    public oCardEffect CardEffect = oCardEffect.NONE; // 카드 효과 타입
    public string CardDesc; // 카드 설명
}

[CreateAssetMenu(fileName = "CardDataMain", menuName = "Scriptable object/CardDataMain")]
public class CardDataMain : ScriptableObject
{
    public CardData[] MainData;
}
