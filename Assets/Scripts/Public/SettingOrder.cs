using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingOrder : MonoBehaviour
{
    #region 변수
    [Header("=====> 뒤쪽 랜더러 <=====")]
    [SerializeField] private Renderer[] BackRenderers; // 뒤쪽에 있는 랜더러

    [Header("=====> 중앙 랜더러 <=====")]
    [SerializeField] private Renderer[] MiddleRenderers; // 중앙에 있는 랜더러 

    [Header("=====> 표시될 레이어 이름 <=====")]
    [SerializeField] private string oSortingLayerName; // SortingLayer 이름

    private int OriginOrder;
    #endregion // 변수

    #region 함수
    /** 맨 앞에 보이게 하는 OriginOrder 세팅 */
    public void SetOriginOrder(int o_OriginOrder)
    {
        OriginOrder = o_OriginOrder;
        SetOrder(o_OriginOrder);
    }

    /** 카드를 가장 앞에 보이게 설정 >> 확대 오더 */
    public void SetMostFrontOrder(bool IsMostFront)
    {
        // true일 경우 가장앞에 보이게 위치, 아닐 경우 원래 오더
        SetOrder(IsMostFront ? 100 : OriginOrder);
    }

    /** 기본 오더 설정 */
    public void SetOrder(int Order)
    {
        int SumOrder = Order * 5; // 카드 순서의 간격을 5정도 만들어준다

        foreach(var Render in BackRenderers)
        {
            // 뒤쪽에 있는 랜더러 설정
            Render.sortingLayerName = oSortingLayerName;
            Render.sortingOrder = SumOrder;
        }

        foreach (var Render in MiddleRenderers)
        {
            // 앞쪽에 있는 랜더러 설정
            Render.sortingLayerName = oSortingLayerName;
            Render.sortingOrder = SumOrder + 1; // 뒤쪽랜더러 보다 1 앞에 보이게 설정
        }
    }

    #endregion // 함수
}
