using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatternSprite : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<Sprite> PatternSpriteList = new List<Sprite>();
    [SerializeField] private GameObject PatternDescBackground;
    [SerializeField] private TMP_Text PatternNameText;
    [SerializeField] private TMP_Text PatternDescText;

    private Dictionary<string, Sprite> Dic = new Dictionary<string, Sprite>();
    private SpriteRenderer SpriteRender;
    private PointerEventData Data;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        SpriteRender = GetComponent<SpriteRenderer>();

        PatternDescBackground.SetActive(false);

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

    private void Update()
    {
        switch (EnemyManager.Instance.o_ePatternRandom)
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
    private void OnMouseEnter()
    {
        switch(EnemyManager.Instance.o_ePatternRandom)
        {
            case EnemyManager.EnemyPatternRandom.Basic:
                PatternDescBackground.SetActive(true);
                var DisName = EnemyManager.Instance.oEnemyDisappear.PatternList[0].PatternName;
                var DisDesc = EnemyManager.Instance.oEnemyDisappear.PatternList[0].PatternDesc;
                PatternNameText.text = DisName;
                PatternDescText.text = string.Format(DisDesc,EnemyManager.Instance.SelectEnemy.oAtk);
                break;
            case EnemyManager.EnemyPatternRandom.MAGIC:
                PatternDescBackground.SetActive(true);
                var ContiName = EnemyManager.Instance.oEnemyContinuous.PatternList[0].PatternName;
                var ContiDesc = EnemyManager.Instance.oEnemyContinuous.PatternList[0].PatternDesc;
                PatternNameText.text = ContiName;
                PatternDescText.text = string.Format(ContiDesc, EnemyManager.Instance.SelectEnemy.oAtk);
                break;
        }
    }

    /** 초기화 >> 마우스가 나갔을 때 */
    private void OnMouseExit()
    {
        PatternDescBackground.SetActive(false);
    }
    #endregion // 함수
}
