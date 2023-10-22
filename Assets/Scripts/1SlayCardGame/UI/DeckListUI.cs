using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListUI : Popup
{
    #region 변수
    #endregion // 변수

    #region 함수
    public void OnClickCancle()
    {
        Destroy(this.gameObject);
    }

    /** 덱 리스트를 설정한다 */
    private void SettingDeckList()
    {
        // TODO : 카드 덱에서 데이터 가져와야함
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
