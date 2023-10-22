using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListUI : Popup
{
    #region 변수
    [Header("=====> 플레이어 덱 <=====")]
    [SerializeField] private GameObject CardListGroupRoot; // 카드 덱 오브젝트 하위에 생성될 위치
    [SerializeField] private GameObject CardDeckPrefab; // 카드 덱 원본 객체
    #endregion // 변수

    #region 함수
    public void OnClickCancle()
    {
        Destroy(this.gameObject);
    }

    /** 덱 리스트를 설정한다 */
    private void SettingDeckList()
    {
        CardDeckCreate();
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
                // TODO : 카드 덱 중복시 인덱스 증가
                // 인벤토리 구현 함수 참고
            }
        }
    }

    /** 카드 덱 오브젝트를 생성한다 */
    public CardDeckSetting CardDeckObjectPool(GameObject CardDeckPrefab, GameObject CardListGroupRoot)
    {
        var CardDeckObejct = CFactory.CreateCloneObj("CardDeck", CardDeckPrefab, CardListGroupRoot,
            Vector3.zero, Vector3.one, Vector3.zero);

        return CardDeckObejct.GetComponent<CardDeckSetting>();
    }

    /** 덱 리스트를 생성한다 */
    public static DeckListUI CreateDeckList(GameObject RootObject)
    {
        var CreateDeckList = CFactory.CreateCloneObj<DeckListUI>("DeckList",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/DeckListShow_UI"), RootObject,
            Vector3.zero, Vector3.one, Vector3.zero);

        CreateDeckList.SettingDeckList();
        return CreateDeckList;
    }
    #endregion // 함수
}
