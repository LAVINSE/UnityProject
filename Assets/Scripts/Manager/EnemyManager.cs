using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyManager : MonoBehaviour
{
    public enum EnemyPeturnRandom
    {
        NONE = 0,
        MAGIC,
        SL,
        TL
    }
    #region 변수
    [Header("=====> Player Option <=====")]
    [SerializeField] private PlayerData oPlayerData;
    [SerializeField] private GameObject oPlayer;

    [Header("=====> Enemy Option <=====")]
    [SerializeField] private List<GameObject> ParticleObjectList = new List<GameObject>();
    [SerializeField] private GameObject EnemyOriginRoot;
    [SerializeField] private GameObject EnemySpellRoot;

    [Header("=====> Enemy UI <=====")]
    [SerializeField] private GameObject EnemyHpSlider;
    [SerializeField] private GameObject EnemyHpSliderRoot;
    [SerializeField] private Transform CanvasTransform;
    [SerializeField] private Vector3 Distance;

    [Header("=====> Scriptable Objects <=====")]
    [SerializeField] private EnemyBasic EnemyNormal; // 적 스크립트 테이블
    [SerializeField] private EnemyBasic EnemyElite; // 적 스크립트 테이블
    [SerializeField] private EnemyBasic EnemyBoss; // 적 스크립트 테이블
    
    [Header("=====> 개발자 인스펙터 확인용 <=====")]
    [SerializeField] private EnemyPeturnRandom ePeturnRandom;
    [SerializeField] private bool IsSelectPeturn = false;
    [SerializeField] private List<EnemyBasicData> EnemyBuffer = new List<EnemyBasicData>(); // 적을 리스트에 넣는다
    #endregion // 변수

    #region 프로퍼티
    public static EnemyManager Instance { get; private set; }
    public EnemySetting SelectEnemy { get; set; }
    public GameObject oEnemyHpSlider => EnemyHpSlider;
    public EnemyPeturnRandom o_ePeturnRandom => ePeturnRandom;
    public bool IsEnemyAttackReady { get; set; } = false;
    public bool IsEnemyAlive { get; set; } = true;
    public bool IsPeturn { get; set; } = false;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;
    }

    /** 초기화 */
    private void Start()
    {
        TurnManager.IsSpawnEnemy += SpawnEnemy;
    }

    /** 초기화 => 삭제될때 호출*/
    private void OnDestroy()
    {
        TurnManager.IsSpawnEnemy -= SpawnEnemy;
    }

    /** 적을 소환한다 */
    private void SpawnEnemy(StageInfo.EnemyType StageEnemyTypeInfo)
    {
        CreateEnemy(EnemyOriginRoot, StageEnemyTypeInfo);
    }

    /** 적을 생성한다 */
    private void CreateEnemy(GameObject EnemyRoot, StageInfo.EnemyType StageEnemyTypeInfo)
    {
        var Enemy = CFactory.CreateCloneObj("Enemy", SpawnReadyEnemy(StageEnemyTypeInfo).oEnemyObject, EnemyRoot,
                                            Vector3.zero, Vector3.one, Vector3.zero);
        var EnemyComponent = Enemy.GetComponent<EnemySetting>();
        EnemyComponent.EnemySetup(SpawnReadyEnemy(StageEnemyTypeInfo));
        CreateEnemyHpSlider(Enemy);
    }

    /** 적을 선택해 소환할 준비를 한다 */
    private EnemyBasicData SpawnReadyEnemy(StageInfo.EnemyType StageEnemyTypeInfo)
    {
        if(EnemyBuffer.Count == 0)
        {
            switch (StageEnemyTypeInfo)
            {
                case StageInfo.EnemyType.NORMAL:
                    RandomEnemyData(EnemyNormal);
                    break;
                case StageInfo.EnemyType.ELITE:
                    RandomEnemyData(EnemyElite);
                    break;
                case StageInfo.EnemyType.BOSS:
                    RandomEnemyData(EnemyBoss);
                    break;
            }
        }

        EnemyBasicData Data = EnemyBuffer[0];   
        EnemyBuffer.RemoveAt(0);
        return Data;
    }

    /** 랜덤하게 적을 설정한다 */
    private void RandomEnemyData(EnemyBasic EnemyScirptTable)
    {
        EnemyBuffer.Clear();

        for (int i = 0; i < EnemyScirptTable.MainList.Length; i++)
        {
            EnemyBasicData Enemy = EnemyScirptTable.MainList[i];
            EnemyBuffer.Add(Enemy);
        }

        for (int i = 0; i < EnemyBuffer.Count; i++)
        {
            int Rand = Random.Range(i, EnemyBuffer.Count);
            EnemyBasicData Temp = EnemyBuffer[i];
            EnemyBuffer[i] = EnemyBuffer[Rand];
            EnemyBuffer[Rand] = Temp;
        }
    }

    /** 소환된 적을 선택한다 */
    public void SeletedEnemy(EnemySetting Enemy)
    {
        SelectEnemy = Enemy;
    }

    /** 적 HpSlider 생성한다 */
    private void CreateEnemyHpSlider(GameObject Enemy)
    {
        var HpSlider = CFactory.CreateCloneObj("EnemyHpSlider", EnemyHpSlider, EnemyHpSliderRoot,
                                            Vector3.zero, Vector3.one, Vector3.zero);
        //HpSlider.transform.SetParent(CanvasTransform);
        HpSlider.transform.localScale = Vector3.one;
        HpSlider.GetComponent<SliderPositionAuto>().Setup(Enemy.transform, Distance);
        HpSlider.GetComponent<EnemySliderViewer>().Setup(Enemy.GetComponent<EnemySetting>());
    }

    /** 패턴을 선택한다 */
    public void EnemySelectPeturn()
    {
        if(IsSelectPeturn == true)
        {
            return;
        }

        var Random = RandomPeturnEnum();
        switch (Random)
        {
            case EnemyPeturnRandom.NONE:
                Debug.Log("없음");
                ePeturnRandom = EnemyPeturnRandom.NONE;
                break;
             case EnemyPeturnRandom.MAGIC:
                Debug.Log("MAGIC");
                ePeturnRandom = EnemyPeturnRandom.MAGIC;
                break;
            case EnemyPeturnRandom.SL:
                Debug.Log("SL");
                ePeturnRandom = EnemyPeturnRandom.SL;
                break;
            case EnemyPeturnRandom.TL:
                Debug.Log("TL");
                ePeturnRandom = EnemyPeturnRandom.TL;
                break;
        }

    }

    /** 패턴을 랜덤하게 선택한다 */
    private EnemyPeturnRandom RandomPeturnEnum()
    {
        var EnumValues = System.Enum.GetValues(enumType: typeof(EnemyPeturnRandom));
        return (EnemyPeturnRandom)EnumValues.GetValue(Random.Range(0, EnumValues.Length));
    }

    /** 패턴을 사용한다 */
    public void EnemyExecutePeturn()
    {
        switch (o_ePeturnRandom)
        {
            case EnemyPeturnRandom.NONE:
                StopCoroutine(EnemyNonePeturn());
                StartCoroutine(EnemyNonePeturn());
                break;
            case EnemyPeturnRandom.MAGIC:
                StopCoroutine(EnemyMagicPeturn());
                StartCoroutine(EnemyMagicPeturn());
                break;
            case EnemyPeturnRandom.SL:
                StopCoroutine(EnemySLPeturn());
                StartCoroutine(EnemySLPeturn());
                break;
            case EnemyPeturnRandom.TL:
                StopCoroutine(EnemyTLPeturn());
                StartCoroutine(EnemyTLPeturn());
                break;
        }
    }

    /** NONE 패턴을 사용한다 */
    private IEnumerator EnemyNonePeturn()
    {
        var EnemyATK = SelectEnemy.oEnemyDataSet.ATK;
        var PlayerPosition = oPlayerData.transform.position;

        yield return new WaitForSeconds(3f);
        var oParticle = CFactory.CreateCloneObj("Disappear_Type", ParticleObjectList[0], EnemySpellRoot,
                                                Vector3.zero, Vector3.one, Vector3.zero);

        ParticleMove(oParticle, PlayerPosition, true, 0.2f);

        yield return new WaitForSeconds(0.2f);
        oPlayerData.TakeDamage(EnemyATK);
        Destroy(oParticle);

        yield return new WaitForSeconds(3f);
        
        Debug.Log("NONE");
        IsEnemyAttackReady = false;
    }

    /** Magic 패턴을 사용한다 */
    private IEnumerator EnemyMagicPeturn()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Magic");
        IsEnemyAttackReady = false;
    }

    /** SL 패턴을 사용한다 */
    private IEnumerator EnemySLPeturn()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("SL");
        IsEnemyAttackReady = false;
    }

    /** TL 패턴을 사용한다 */
    private IEnumerator EnemyTLPeturn()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("TL");
        IsEnemyAttackReady = false;
    }

    /** 파티클을 두트윈을 사용해 움직인다 */
    private void ParticleMove(GameObject ParticleObject, Vector3 EndVector3, bool IsDotween, float Time = 0)
    {
        if (IsDotween == true)
        {
            ParticleObject.transform.DOMove(EndVector3, Time);
        }
        else
        {
            ParticleObject.transform.position = EndVector3;
        }
    }
    #endregion // 함수
}
