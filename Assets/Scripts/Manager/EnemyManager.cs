using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
#region 변수
    [Header("=====> Scriptable Objects <=====")]
    [SerializeField] private EnemyDataMain oEnemyDataMain; // 적 스크립트 테이블
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemyOriginRoot;

    [Header("=====> 인스펙터 확인용 <=====")]
    [SerializeField] private List<EnemyDataSetting> EnemyBuffer = new List<EnemyDataSetting>(); // 적을 리스트에 넣는다

    #endregion // 변수

    #region 프로퍼티
    public EnemySetting SelectEnemy { get; set; }
    public static EnemyManager Instance { get; private set; }
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
        RandomEnemyData();
        TurnManager.IsSpawnEnemy += SpawnEnemy;
    }

    /** 초기화 => 삭제될때 호출*/
    private void OnDestroy()
    {
        TurnManager.IsSpawnEnemy -= SpawnEnemy;
    }

    /** 적을 소환한다 */
    private void SpawnEnemy(bool IsCreateEnemy = true)
    {
        CreateEnemy(EnemyPrefab, EnemyOriginRoot, IsCreateEnemy);
    }

    /** 적을 생성한다 */
    private void CreateEnemy(GameObject EnemyPrefab, GameObject EnemyRoot, bool IsCreateEnemy = true)
    {
        if(IsCreateEnemy == false)
        {
            return;
        }

        var Enemy = CFactory.CreateCloneObj("Enemy", EnemyPrefab, EnemyRoot,
                                            Vector3.zero, Vector3.one, Vector3.zero);
        var EnemyComponent = Enemy.GetComponent<EnemySetting>();
        EnemyComponent.EnemySetup(SpawnReadyEnemy());
    }

    /** 적을 선택해 소환할 준비를 한다 */
    private EnemyDataSetting SpawnReadyEnemy()
    {
        if(EnemyBuffer.Count == 0)
        {
            RandomEnemyData();
        }

        EnemyDataSetting Data = EnemyBuffer[0];   
        EnemyBuffer.RemoveAt(0);
        return Data;
    }

    /** 랜덤하게 적을 설정한다 */
    private void RandomEnemyData()
    {
        for (int i = 0; i < oEnemyDataMain.MainList.Length; i++)
        {
            EnemyDataSetting Enemy = oEnemyDataMain.MainList[i];
            EnemyBuffer.Add(Enemy);
        }

        for (int i = 0; i < EnemyBuffer.Count; i++)
        {
            int Rand = Random.Range(i, EnemyBuffer.Count);
            EnemyDataSetting Temp = EnemyBuffer[i];
            EnemyBuffer[i] = EnemyBuffer[Rand];
            EnemyBuffer[Rand] = Temp;
        }
    }

    /** 소환된 적을 선택한다 */
    public void SeletedEnemy(EnemySetting Enemy)
    {
        SelectEnemy = Enemy;
    }
    #endregion // 함수
}
