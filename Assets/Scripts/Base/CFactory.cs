using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SetParent > WorldPositionStays 인자가 참일 경우 개체가 이전과 동일한 월드 위치, 각도, 스케일을 
 * 유지하도록 부모오브젝트와 관계에 의해 상대적으로 수정된다
 */

public static class CFactory
{
    #region 클래스 함수
    // 원본 객체를 생성한다
    public static GameObject CreateObject(string Name, GameObject ParentObject,
        Vector3 Pos, Vector3 Scale, Vector3 Rotate, bool WorldPositionStays = false)
    {
        var Gameobj = new GameObject(Name);
        Gameobj.transform.SetParent(ParentObject?.transform, WorldPositionStays);

        Gameobj.transform.localPosition = Pos;
        Gameobj.transform.localScale = Scale;
        Gameobj.transform.localEulerAngles = Rotate;

        return Gameobj;
    }

    // 사본 객체를 생성한다
    public static GameObject CreateCloneObj(string Name, GameObject OriginObject, GameObject ParentObject,
        Vector3 Pos, Vector3 Scale, Vector3 Rotate, bool WorldPositionStays = false)
    {
        var Gameobj = GameObject.Instantiate(OriginObject, Vector3.zero, Quaternion.identity);
        Gameobj.name = Name;
        Gameobj.transform.SetParent(ParentObject?.transform, WorldPositionStays);

        Gameobj.transform.localPosition = Pos;
        Gameobj.transform.localScale = Scale;
        Gameobj.transform.localEulerAngles = Rotate;

        return Gameobj;
    }
    #endregion // 클래스 함수

    #region 제네릭 함수
    // 원본 객체를 생성한다
    public static T CreateObject<T>(string Name, GameObject ParentObject,
        Vector3 Pos, Vector3 Scale, Vector3 Rotate, bool WorldPositionStays = false) where T : Component
    {
        var Gameobj = CFactory.CreateObject(Name, ParentObject, Pos, Scale, Rotate, WorldPositionStays);
            
        // new로 생성되었기 때문에 컴포넌트가 없다
        return Gameobj.GetComponent<T>() ?? Gameobj.AddComponent<T>();
    }

    // 사본 객체를 생성한다
    public static T CreateCloneObj<T>(string Name, GameObject OriginObject, GameObject ParentObject,
        Vector3 Pos, Vector3 Scale, Vector3 Rotate, bool WorldPositionStays = false) where T : Component
    {
        var Gameobj = CFactory.CreateCloneObj(Name, OriginObject, ParentObject, Pos, Scale, Rotate, WorldPositionStays);

        return Gameobj.GetComponent<T>() ?? Gameobj.AddComponent<T>();
    }

    // Fisher-Yates shuffle, 배열 셔플 알고리즘
    public static T[] ShuffleArray<T>(T[] Array, int Seed)
    {
        // Prng = Pseudorandom Number Generator 의 약자 (유사난수생성기)
        System.Random Prng = new System.Random(Seed);

        for(int i = 0; i < Array.Length - 1; i++) 
        {
            int RandomIndex = Prng.Next(i, Array.Length);
            T TempItem = Array[RandomIndex];
            Array[RandomIndex] = Array[i];
            Array[i] = TempItem;
        }

        return Array;
    }
    #endregion // 제네릭 함수
}
