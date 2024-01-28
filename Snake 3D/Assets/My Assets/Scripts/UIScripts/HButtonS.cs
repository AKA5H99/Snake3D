using UnityEngine;
using UnityEngine.UI;

public class HButtonS : MonoBehaviour
{
    public float DTWPanelCountdown = 1.5f;

    public GameObject LoadingPanel,StorePanel,MainPanel, DTWPanel,NoticePanel;
    public Animator CamAnimator;
    public Text RewindTxt;

    private void Awake()
    {
        //Debug.LogWarning("Delleting All PlayerPrefs");
        //PlayerPrefs.DeleteAll();


        if (PlayerPrefs.GetInt("NewPlayer") == 0)
        {
            PlayerPrefs.SetInt("RewindValue", 3);
            PlayerPrefs.SetInt("NewPlayer", 1);
        }

        RewindTxt.text = PlayerPrefs.GetInt("RewindValue").ToString();
        LoadingPanel.SetActive(true);

        
    }

    private void Update()
    {
        CountdownFunction();
    }

    public void StoreB()
    {

        //Animation Of Cam
        CamAnimator.SetBool("OnStore", true);

        StorePanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void BackBOfStore()
    {

        //Animation Of Cam
        CamAnimator.SetBool("OnStore", false);

        MainPanel.SetActive(true);
        StorePanel.SetActive(false);
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

    public void OkB()
    {
        PlayerPrefs.SetInt("FirstTimeOpenBool", 1);
    }
}
