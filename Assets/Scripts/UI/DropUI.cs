using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUI : MonoBehaviour
{
    #region 변수
    private RectTransform Rect;
    private DropCard[] oDropCard;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        oDropCard = GetComponentsInChildren<DropCard>();
    }

    /** Ui를 보여준다 */
    public void ShowDropUI()
    {
        for(int i=0; i<oDropCard.Length; i++)
        {
            oDropCard[i].ClearCardBuffer();
        }

        for(int i=0; i<oDropCard.Length; i++)
        {
            oDropCard[i].ShowDropCard();
        }

        Rect.localScale = Vector3.one;
    }

    /** Ui를 숨긴다 */
    public void HideDropUI()
    {
        Rect.localScale = Vector3.zero;
    }

    /** 인스펙터 우클릭 크기 조절을 생성한다 */
    [ContextMenu("ScaleZero")]
    private void ScaleZero()
    {
        transform.localScale = Vector3.zero;
    }

    /** 인스펙터 우클릭 크기 조절을 생성한다 */
    [ContextMenu("ScaleOne")]
    private void ScaleOne()
    {
        transform.localScale = Vector3.one;
    }
    #endregion // 함수
}
