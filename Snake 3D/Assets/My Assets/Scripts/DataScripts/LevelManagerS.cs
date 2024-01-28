using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerS : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject Player,GameCompletePanel,TargetTxtGameObject,HighScoreTxtGameObject,CongratulationPanel;
    public CollisionS CollisionScript;
    public SnakeController SnakeControllerScript;
    public int TargetScore;



    private void Awake()
    {
        LevelEnable();
    }

    private void Update()
    {
        
        //LevelProgressManager();
    }

    // Used By CollisionS Script
    public void LevelProgressManager()
    {
        if (CollisionScript.CurrentScore == TargetScore -1) // -1 is used because of collision dellay proble
        {
            
            //Time.timeScale = 0;
            SnakeControllerScript.MoveSpeed = 0;
            SnakeControllerScript.SteerSpeed = 0;

            //Doing This To check Open Level Is Last Level or not
            if(PlayerPrefs.GetInt("OpenLevelNo") < 0 || PlayerPrefs.GetInt("OpenLevelNo") == 12)
            {
                CongratulationPanel.SetActive(true);
            }
            else
            {
                GameCompletePanel.SetActive(true);
            }

            // Updating Level Unlocked Number
            if (PlayerPrefs.GetInt("OpenLevelNo")  == PlayerPrefs.GetInt("LevelsUnloked"))
            {
                PlayerPrefs.SetInt("LevelsUnloked", PlayerPrefs.GetInt("OpenLevelNo") + 1);

                Debug.Log(PlayerPrefs.GetInt("LevelsUnloked"));
            }
            
        }
    }

    public void LevelEnable()
    {

        if(PlayerPrefs.GetInt("OpenLevelNo") > 0)
        {
            //SetActive The Selectected Level
            Levels[PlayerPrefs.GetInt("OpenLevelNo") - 1].SetActive(true);
        }
        

        //Making The target Score To complete Level
        switch (PlayerPrefs.GetInt("OpenLevelNo"))
        {
            case 1:
                TargetScore = 10;
                break;

            case 2:
                TargetScore = 15;
                break;

            case 3:
                TargetScore = 25;
                //2 Fruits
                break;

            case 4:
                TargetScore = 8;
                SnakeControllerScript.BodysCountInStarting = 100;
                break;

            case 5:
                TargetScore = 35;
                //2 fruits
                break;

            case 6:
                TargetScore = 40;
                SnakeControllerScript.BodysCountInStarting = 25;
                //2 fruits
                break;

            case 7:
                TargetScore = 5;
                break;

            case 8:
                TargetScore = 15;
                SnakeControllerScript.BodysCountInStarting = 20;
                break;

            case 9:
                TargetScore = 20;
                SnakeControllerScript.BodysCountInStarting = 35;
                break;

            case 10:
                TargetScore = 4;
                break;

            case 11:
                TargetScore = 10;
                break;

            case 12:
                TargetScore = 8;
                SnakeControllerScript.BodysCountInStarting = 200;
                break;

                //Imposible Level
            case -1:
                TargetScore = 18;
                Levels[12].SetActive(true);
                break;

                //Score Level
            case -2:
                TargetScore = 500;
                Levels[13].SetActive(true);
                TargetTxtGameObject.SetActive(false);
                HighScoreTxtGameObject.SetActive(true);
                break;
        }
    }
}
