using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropUI : Popup
{
    #region 변수
    [SerializeField] private GameObject BackGround = null;

    private DropCard[] oDropCard;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        oDropCard = GetComponentsInChildren<DropCard>();
        Option_Background_Img = BackGround;
    }

    /** 나가기 창을 보여준다 */
    public void LeaveShow()
    {
        CSceneManager.Instance.LeavePanelShow();
    }

    /** 드랍 UI를 닫는다 */
    public void DropClose()
    {
        base.PopupClose();
    }

    /** 드랍 UI를 보여준다 */
    public void DropShow()
    {
        base.PopupShow();
    }

    /** 드랍 초기화를 한다 */
    private void ResetDrop()
    {
        for(int i=0; i<oDropCard.Length; i++)
        {
            oDropCard[i].ClearCardBuffer();
        }

        for(int i=0; i<oDropCard.Length; i++)
        {
            oDropCard[i].ShowDropCard();
        }
    }

    /** 드랍 UI를 생성한다 */
    public static DropUI CreateDropUI(GameObject DropUIRoot)
    {
        var CreateDrop = CFactory.CreateCloneObj<DropUI>("DropUI",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/DropItem_UI"), DropUIRoot,
            Vector3.zero, Vector3.one, Vector3.zero);
        CreateDrop.ResetDrop();
        return CreateDrop;
    }
    #endregion // 함수
}
