using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

/** 싱글톤 적용 */
public class CardManager : MonoBehaviour
{
    private enum ECardState
    { 
        NOTHING = 0, // 아무것도 할 수 없다
        CANMOUSEOVER, // 마우스만 올릴 수 있다
        CANMOUSEDRAG, // 드래그까지 가능하다
    }

    #region 변수
    [Header("=====> 카드 <=====")]
    [SerializeField] private GameObject CardPrefab; // 카드 원본 객체
    [SerializeField] private GameObject CardOriginRoot; // 카드가 오브젝트 하위에 생성될 위치
    [SerializeField] private GameObject CardSpawnPoint; // 카드가 생성될 위치
    [SerializeField] private List<CardSetting> FrontCards; // 앞면 카드가 담길 리스트
    [SerializeField] private List<CardSetting> BackCards; // 뒷면 카드가 담길 리스트
    [SerializeField] private ECardState CardState; // 카드 상태

    [Header("=====> 카드 정렬 <=====")]
    [SerializeField] private Transform PlayerCardLeft;
    [SerializeField] private Transform PlayerCardRight;

    [Header("=====> 플레이어 데이터 <=====")]
    [SerializeField] private PlayerData oPlayerData;
    [SerializeField] private SpellCard PlayerSpellCard;

    [Header("=====> 플레이어 덱 <=====")]
    [SerializeField] private GameObject CardListGroupRoot; // 카드 덱 오브젝트 하위에 생성될 위치
    [SerializeField] private GameObject CardDeckPrefab; // 카드 덱 원본 객체

    [Header("=====> 인스펙터 확인용 <=====")]
    [SerializeField] private List<CardScirptTable> CardBuffer; // 카드 데이터에 들어있는 카드를 리스트에 넣는다
    [SerializeField] private List<CardSetting> DespawnCard; // 사용한 카드를 리스트에 넣는다

    private PRS OriginPRS; // 위치,크기,회전을 편리하게 사용하는 변수
    private CardPanel oCardPanel;
    private bool IsMyCardDrag = false; // 카드를 잡고 있는지 확인하는 변수
    private bool IsOnMyCardArea; // 카드 영역인지 확인하는 변수
    #endregion // 변수

    #region 프로퍼티
    public static CardManager Instance { get; private set; }
    public CardSetting SelectCard { get; set; } // 선택된 카드
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        oCardPanel = GetComponent<CardPanel>();
        Instance = this;
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {       
        // 마우스 오른쪽 버튼을 눌렀을 경우
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D HitData = Physics2D.Raycast(CUtils.MousePosition, Vector3.forward);

            // 태그가 카드일 경우
            if (HitData.transform != null && HitData.transform.CompareTag("Card"))
            {
                oCardPanel.CardInfoPanelTrue();
            }
            else
            {
                oCardPanel.CardInfoPanelFalse();
            }
        }

        if (IsMyCardDrag == true)
        {
            CardDrag();
        }

        DetectCardArea(); // 카드 영역 감지 함수
        SetECardState(); // 카드 상태 확인 함수
    }

    /** 초기화 */
    private void Start()
    {
        SetupCardBuffer();
        TurnManager.IsOnAddCard += AddCard;
    }

    /** 초기화 >> 제거 되었을 경우 */
    private void OnDestroy()
    {
        TurnManager.IsOnAddCard -= AddCard;
    }

    /** 카드를 뽑을 준비를 한다 */
    public CardScirptTable PopCard()
    {
        // 카드 데이터 리스트에 아무것도 없다면
        if (CardBuffer.Count == 0)
        {
            SetupCardBuffer();
        }

        // 맨 앞에 있는 카드를 뽑는다
        // FIXME : Queue에 넣어서 사용해도됨
        CardScirptTable Data = CardBuffer[0];
        CardBuffer.RemoveAt(0);
        return Data;
    }

    /** oCardData에 있는 카드 데이터를 셔플한다 >> 덱 생성 */
    public void SetupCardBuffer()
    {
        CardBuffer = new List<CardScirptTable>();

        // CardData에 있는 리스트 수 만큼 반복
        for(int i = 0; i< GameManager.Inst.oCardBasicTableDeck.Count; i++)
        {
            CardScirptTable Data = GameManager.Inst.oCardBasicTableDeck[i];
            for(int j = 0; j< Data.CardCount; j++)
            {
                CardBuffer.Add(Data);
            }
        }

        // 중복으로 나오는걸 방지
        for(int i = 0; i< CardBuffer.Count; i++)
        {
            int Rand = Random.Range(i, CardBuffer.Count);
            CardScirptTable Temp = CardBuffer[i]; 
            CardBuffer[i] = CardBuffer[Rand]; 
            CardBuffer[Rand] = Temp;
        }
    }

    /** 카드를 뽑는다 */
    public void AddCard(bool IsFront = true)
    {
        CreateCard(CardPrefab, CardOriginRoot, IsFront);
    }

    /** 카드를 생성한다 */
    public void CreateCard(GameObject OriginCard, GameObject CardRoot, bool IsFront)
    {
        var CardObject = CardObjectPool(OriginCard, CardRoot);
        CardObject.transform.position = CardSpawnPoint.transform.position;
        var Card = CardObject.GetComponent<CardSetting>();
        Card.CardSetup(PopCard(), IsFront);
        // 앞면인지 뒷면인지 판단
        (IsFront ? FrontCards : BackCards).Add(Card);

        CardOrderSetting(IsFront);
        CardAlignment(IsFront);
    }

    /** 카드 오더 세팅 */
    private void CardOrderSetting(bool IsFront)
    {
        int Count = IsFront ? FrontCards.Count : BackCards.Count;
        for(int i =0; i< Count; i++)
        {
            var TargetCard = IsFront ? FrontCards[i] : BackCards[i];
            TargetCard?.GetComponent<SettingOrder>().SetOriginOrder(i);
        }
    }

    /** 카드를 정렬한다 */
    private void CardAlignment(bool IsFront)
    {
        List<PRS> OriginCardPRS = new List<PRS>();

        // FIXME : 뒷면 카드 위치 조정 필요
        // 앞면 일 경우
        if(IsFront == true)
        {
            OriginCardPRS = RoundAlignment(PlayerCardLeft, PlayerCardRight, FrontCards.Count, 0.5f, Vector3.one);
        }
        else
        {
            OriginCardPRS = RoundAlignment(PlayerCardLeft, PlayerCardRight, BackCards.Count, 0.5f, Vector3.one);
        }

        var TargetCards = IsFront ? FrontCards : BackCards; // 앞면 뒷면 판단
        
        for(int i = 0; i< TargetCards.Count; i++)
        {
            var Cards = TargetCards[i];

            Cards.OriginPRS = OriginCardPRS[i]; // 반복해서 둥글게 정렬
            Cards.MoveTransform(Cards.OriginPRS, true, 0.7f); // 설정한 위치와 크기를 DoTween을 사용해 움직인다
        }
    }

    /** 카드를 확대한다 */
    private void EnlargeCard(bool IsEnlarge, CardSetting Card, float CardSclaeValue)
    {
        if(IsEnlarge == true)
        {
            // 선택된 카드가 확대 되었을때 이상하게 선택 안되게 값 설정, Y축은 카드핸드 위치보다 살짝 높게 설정
            Vector3 EnlargePos = new Vector3(Card.OriginPRS.Position.x, Card.OriginPRS.Position.y + 0.7f, -10.0f);
            // DOTween 사용 X
            Card.MoveTransform(new PRS(EnlargePos, Vector3.one * CardSclaeValue, Quaternion.identity), false);
        }
        else
        {
            Card.MoveTransform(Card.OriginPRS, false);
        }

        Card.GetComponent<SettingOrder>().SetMostFrontOrder(IsEnlarge);
    }

    /** 카드를 움직일 수 있는 상태를 정한다 */
    private void SetECardState()
    {
        if(TurnManager.Instane.bIsLoading == true)
        {
            CardState = ECardState.NOTHING;
        }
        else if(TurnManager.Instane.bIsMyTurn == false)
        {
            CardState = ECardState.CANMOUSEOVER;
        }
        else if(TurnManager.Instane.bIsMyTurn == true)
        {
            CardState = ECardState.CANMOUSEDRAG;
        }
    }

    /** 카드 영역을 감지한다 */
    private void DetectCardArea()
    {
        // 마우스에서 충돌한 모든 레이케스트 히트를 가져온다, 카메라에서 봤을때 z방향
        RaycastHit2D[] Hits = Physics2D.RaycastAll(CUtils.MousePosition, Vector3.forward);
        // 레이어 번호를 가져온다
        int Layer = LayerMask.NameToLayer("MyCardArea");

        // MyCardArea와 겹치는게 있으면 true, 없으면 false
        IsOnMyCardArea = Array.Exists(Hits, x => x.collider.gameObject.layer == Layer);
    }
   
    /** 카드를 드래그 한다 */
    private void CardDrag()
    {
        if(CardState != ECardState.CANMOUSEDRAG)
        {
            return;
        }

        if(!IsOnMyCardArea == true)
        {
            // 마우스 포지션으로 이동하고, 기본 크기, Dotween X
            SelectCard.MoveTransform
            (new PRS(CUtils.MousePosition, SelectCard.OriginPRS.Scale, Quaternion.identity), false);
        }
    }

    /** 카드에 마우스를 올린다 */
    public void CardMouseOver(CardSetting Card)
    {
        // 아무것도 할 수 없을 경우
        if (CardState == ECardState.NOTHING)
        {
            // 함수를 종료한다
            return;
        }

        SelectCard = Card;
        EnlargeCard(true, Card, 1.2f);
    }

    /** 카드를 누른다 */
    public void CardMouseDown()
    {
        if(CardState != ECardState.CANMOUSEDRAG)
        {
            return;
        }

        IsMyCardDrag = true;
    }

    /** 카드를 누르고 있다가 때는 순간 */
    public void CardMouseUp()
    {
        IsMyCardDrag = false;

        if(CardState !=ECardState.CANMOUSEDRAG)
        {
            return;
        }

        if(!IsOnMyCardArea)
        {
            // 플레이어 현재 마나보다 카드코스트가 높으면 종료
            if (SelectCard.CardSettingData.CardCost > oPlayerData.oCurrentCost)
            {
                return;
            }

            // 카드 코스트 만큼 현재 코스트 감소
            oPlayerData.oCurrentCost -= SelectCard.CardSettingData.CardCost;
            // 카드 효과를 사용한다
            PlayerSpellCard.EffectCardSpawn(SelectCard.CardSettingData.CardEffect);
            // 카드를 디스폰 시킨다
            CardDelete();
        }
    }

    /** 카드에서 마우스가 나갔다 */
    public void CardMouseExit(CardSetting Card)
    {
        EnlargeCard(false, Card, 1.0f);
    }

    /** 카드 사용 후 빈 공간을 정렬하고 리스트에 추가한다 */
    public void CardDelete()
    {
        CardDespawn();
        FrontCards.Remove(SelectCard);
        DespawnCard.Add(SelectCard);

        SelectCard.transform.DOKill();
        SelectCard = null;
        CardAlignment(true);
    }

    /** 둥글게 정렬한다 */
    private List<PRS> RoundAlignment(Transform CardLeftPos, Transform CardRightPos,
                                    int CardListCount, float Height /** 반지름 */, Vector3 Scale)
    {
        float[] CardCountLerp = new float[CardListCount]; // 현재 위치가 어느정도 되어야하는지 정해주는 변수 0~1 사이의 값을 가짐
        List<PRS> Results = new List<PRS>(CardListCount); // 미리 용량을 설정

        switch (CardListCount)
        {
            case 1:
                CardCountLerp = new float[] { 0.5f };
                break;
            case 2:
                CardCountLerp = new float[] { 0.27f, 0.73f };
                break;
            case 3:
                CardCountLerp = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            default:
                float Interval = 1f / (CardListCount - 1); // 0~1 사이의 값
                for (int i = 0; i < CardListCount; i++)
                {
                    CardCountLerp[i] = Interval * i;
                }
                break;
        }

        for (int i = 0; i < CardListCount; i++)
        {
            var TargetPos = Vector3.Lerp(CardLeftPos.position, CardRightPos.position, CardCountLerp[i]);
            var TargetRot = Quaternion.identity;

            if (CardListCount >= 4)
            {
                // 0.5를 뺀 이유는 CardcountLerp의 값이 0~1의 값을 가지기 때문에 그 중심인 0.5를 뺀다
                float Curve = Mathf.Sqrt(Mathf.Pow(Height, 2) - Mathf.Pow(CardCountLerp[i] - 0.5f, 2));
                Curve = Height >= 0 ? Curve : -Curve; // 양수 음수 설정
                TargetPos.y += Curve;
                // 일정 시간을 두고 목표하는 방향으로 회전하는 코드 
                TargetRot = Quaternion.Slerp(CardLeftPos.rotation, CardRightPos.rotation, CardCountLerp[i]);
            }

            Results.Add(new PRS(TargetPos, Scale, TargetRot));
        }

        return Results;
    }

    /** 사용한 카드를 디스폰 시킨다 */
    public void CardDespawn()
    {
        GameManager.Inst.PoolManager.DeSpawnObj<CardSetting>(SelectCard.gameObject, CardCompleteDespawn);
    }

    /** 카드 비활성화가 완료 되었을 경우 */
    private void CardCompleteDespawn(object Obj)
    {
        (Obj as GameObject).SetActive(false);
    }

    /** 카드 오브젝트 풀링 */
    private CardSetting CardObjectPool(GameObject OriginCard, GameObject CardRoot)
    {
        var Card = GameManager.Inst.PoolManager.SpawnObj<CardSetting>(() =>
        {
            return CFactory.CreateCloneObj("Card", OriginCard, CardRoot, Vector3.zero, Vector3.one, Vector3.zero);
        }) as GameObject;

        Card.SetActive(true);
        return Card.GetComponent<CardSetting>();
    }

    /** 카드 덱을 생성한다 */
    public void CardDeckCreate()
    {
        var oCardDeck = GameManager.Inst.oCardBasicTableDeck;

        if (oCardDeck != null)
        {
            for (int i = 0; i < oCardDeck.Count; i++)
            {
                var CardDeckObject = CardDeckObjectPool(CardDeckPrefab, CardListGroupRoot);
                var Card = CardDeckObject.GetComponent<CardDeckSetting>();
                Card.SettingCardDeck(oCardDeck[i]);
            }
        }
    }

    /** 카드 덱을 디스폰 시킨다 */
    public void CardDeckListDespawn(GameObject CardDeck)
    {
        GameManager.Inst.PoolManager.DeSpawnObj<CardDeckSetting>(CardDeck, CardDeckCompleteDespawn);
    }

    /** 카드 덱 비활성화가 완료 되었을 경우 */
    private void CardDeckCompleteDespawn(object Obj)
    {
        (Obj as GameObject).SetActive(false);
    }

    /** 카드 덱을 오브젝트 풀링한다 */
    public CardDeckSetting CardDeckObjectPool(GameObject CardDeckPrefab, GameObject CardListGroupRoot)
    {
        var CardDeckObejct = GameManager.Inst.PoolManager.SpawnObj<CardDeckSetting>(() =>
        {
            return CFactory.CreateCloneObj("CardDeck", CardDeckPrefab, CardListGroupRoot, Vector3.zero, Vector3.one, Vector3.zero);
        }) as GameObject;
        
        CardDeckObejct.SetActive(true);
        return CardDeckObejct.GetComponent<CardDeckSetting>();
    }
    #endregion // 함수
}
