using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField] GameManagerSO GameManagerSOScript;

    //References
    [SerializeField] GameObject NoticePanel;

    private void Awake()
    {
        IsInternetAvailable();
    }

    private void Start()
    {
        if(!GameManagerSOScript.InternetIsOn)
        {
            NoticePanel.SetActive(true);
        }
    }

    private void IsInternetAvailable()
    {
        GameManagerSOScript.InternetIsOn = Application.internetReachability != NetworkReachability.NotReachable;
    }
}
