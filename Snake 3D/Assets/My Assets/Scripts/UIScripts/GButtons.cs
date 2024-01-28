using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GButtons : MonoBehaviour
{
    public bool isRewinding = false, CanNotRecrord = false;
    //References
    public GameObject LoadingPanelWTD;
    public GameObject GameCompletePanel;
    public SnakeController SnakeControllerScipt;
    public Player_Gravity Player_GravityScript;
    public TimeBody TimeBodyScript;
    public CollisionS CollisionScript;
    public AdmobAdsManager AdmobAdsManagerScript;

    [SerializeField] public bool GameIsPaused;

    private void Awake()
    {
        GameIsPaused = false;

    }


    public void PauseB()
    {
        Time.timeScale = 0;
        GameIsPaused = true;

    }

    public void ResumeB()
    {
        
        Time.timeScale = 1;
        //GameCompletePanel.SetActive(false);
        GameIsPaused = false;

    }

    public void ContinueB()
    {
        
        //If Rewind Value Is Bigger Then 0 Then Player Can Be Revived
        if (PlayerPrefs.GetInt("RewindValue") !> 0)
        {
            PlayerPrefs.SetInt("RewindValue", PlayerPrefs.GetInt("RewindValue") - 1);
            Revieve();
        }
        else
        {
            AdmobAdsManagerScript.ShowRewardedAd();
        }
    }

    public void Revieve()
    {
        CollisionScript.ContinueF();
        StartCoroutine(ReverseTime());
    }


    public void NextB()
    {

        Time.timeScale = 1;
        PlayerPrefs.SetInt("OpenLevelNo", PlayerPrefs.GetInt("OpenLevelNo") + 1);
        SceneManager.LoadScene("GameScene");

        GameCompletePanel.SetActive(false);

    }

    public void RestartB()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }


    public void HomeB()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadFunction());
        

    }

    public void JumpB()
    {
        Player_GravityScript.Jump();


    }

    IEnumerator LoadFunction()
    {
        
        LoadingPanelWTD.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("1HomeScene");
        
    }

    IEnumerator ReverseTime()
    {
        isRewinding = true;
        yield return new WaitForSeconds(1.5f);
        isRewinding = false;
    }
}
