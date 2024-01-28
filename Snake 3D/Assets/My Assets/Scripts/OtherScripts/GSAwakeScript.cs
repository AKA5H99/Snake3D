using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSAwakeScript : MonoBehaviour //This Script Run At starting Of GameScene
{
    public float DTWPanelCountdown = 1.5f;
    public Text TargetScoreTxt;

    //Refrences 
    public GameObject LoadingPanel,DTWPanel, TapToBeginePanel, SnakeSpawnPoint,GuidePanel;
    public SnakeController SnakeControllerScript;
    public LevelManagerS LevelManagerScript;
    public CharactersScript ThatCharactersScript;
    public Transform SnakeObject;
    public Animator TargetScoreAnimator;

    private void Awake()
    {
        LoadingPanel.SetActive(true);
        TapToBeginePanel.SetActive(true);

        // Enable GuidePanel if LevelNo is 1
        if (PlayerPrefs.GetInt("OpenLevelNo") == 1)
        {
            GuidePanel.SetActive(true);
        }

    }
    private void Start()
    {
        //finding SpawnPoint By Tag
        SnakeSpawnPoint = GameObject.FindWithTag("BodySpawnPointTag");

        //Doing This For Starting Wait (Affected By TapToBegin Script)
        SnakeControllerScript.MoveSpeed = 0;
        SnakeControllerScript.SteerSpeed = 0;

        //Spawning Snake To The Spawn Point
        SnakeObject.position = SnakeSpawnPoint.transform.position;
    }
    private void Update()
    {
        CountdownFunction();
        //TargetScore Update
        TargetScoreTxt.text = "Target " + LevelManagerScript.TargetScore.ToString();
    }

    // CountDown Function For Destroy DTWPanel
    public void CountdownFunction()
    {
        DTWPanelCountdown -= 1 * Time.deltaTime;
        if (DTWPanelCountdown < 0)
        {
            Destroy(DTWPanel);
            DTWPanelCountdown = 0;
        }
    }

    public void TapToBegineB()
    {
        Destroy(TapToBeginePanel);
        ThatCharactersScript.CharactersPowerInstantiate();
        TargetScoreAnimator.SetBool("BegineBool", true);
    }
}
