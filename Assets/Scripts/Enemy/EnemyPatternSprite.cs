using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatternSprite : MonoBehaviour
{
    #region 변수
    [SerializeField] private Renderer thisRend;
    [SerializeField] private List<Sprite> PatternSpriteList = new List<Sprite>();
    private Dictionary<string, Sprite> Dic = new Dictionary<string, Sprite>();
    private SpriteRenderer SpriteRender;
    private PointerEventData Data;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        SpriteRender = GetComponent<SpriteRenderer>();

        foreach (var Sprite in PatternSpriteList)
        {
            Dic.Add(Sprite.name,Sprite);
        }

        switch(EnemyManager.Instance.o_ePatternRandom)
        {
            case EnemyManager.EnemyPatternRandom.Basic:
                SpriteRender.sprite = Dic["Basic"];
                break;  
            case EnemyManager.EnemyPatternRandom.MAGIC:
                SpriteRender.sprite = Dic["Magic"];
                break;
        }
    }

    /** 초기화 >> 마우스를 올렸을 때 */
    private void OnMouseOver()
    {
        Debug.Log("Test");
    }
    #endregion // 함수
}
