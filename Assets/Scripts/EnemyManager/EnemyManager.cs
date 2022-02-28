using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyManager instance;
    public Transform[] enemySpawnPositions;
    public GameObject easyGlucose;
    public GameObject normalGlucose; // TBD
    public GameObject hardGlucose; // TBD
    public int glucoseCount;
    public Transform playerFollowTarget;
    Dictionary<string, GameObject> enemyGlucoseTypes = new Dictionary<string, GameObject>();

    public void SpawnEasyGlucose()
    {
        foreach (Transform position in enemySpawnPositions)
        {
            var newEnemy = Instantiate(enemyGlucoseTypes["easy"], position.position, Quaternion.identity);
        }
        CountGlucose();
    }
    public void CountGlucose()
    {
        glucoseCount = GameObject.FindGameObjectsWithTag("Glucose").Length;
        if (glucoseCount == 0)
        {
            GameManager.instance.LoadNextScene();
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        enemyGlucoseTypes.Add("easy", easyGlucose);
        enemyGlucoseTypes.Add("normal", normalGlucose);
        enemyGlucoseTypes.Add("hard", hardGlucose);
        glucoseCount = GameObject.FindGameObjectsWithTag("Glucose").Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEasyGlucose();
        }
        CountGlucose();
    }
}