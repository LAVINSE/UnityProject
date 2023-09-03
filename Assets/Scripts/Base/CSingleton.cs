using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CSingleton<T> : CComponent where T : CSingleton<T>
{
    #region 변수
    private static T o_tInst = null;
    #endregion // 변수

    #region 클래스 프로퍼티
    public static T Inst
    {
        get
        {
            // 인스턴스가 없을 경우
            if(CSingleton<T>.o_tInst == null)
            {
                var Gameobj = new GameObject(typeof(T).Name);
                CSingleton<T>.o_tInst = Gameobj.AddComponent<T>();
            }

            return CSingleton<T>.o_tInst;
        }
    }
    #endregion // 클래스 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        Debug.Assert(CSingleton<T>.o_tInst == null);

        CSingleton<T>.o_tInst = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion // 함수

    #region 클래스 함수
    /** 인스턴스를 생성한다 */
    public static T Create()
    {
        return CSingleton<T>.Inst;
    }
    #endregion // 클래스 함수
}
