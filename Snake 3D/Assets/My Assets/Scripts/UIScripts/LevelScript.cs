using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public int LevelNo;
    public Text SelfText;
    public GameObject LoadingPanelWTD;

    [SerializeField] Button ButtonComponent;
    [SerializeField] Sprite RedSprite;

    //Private
    private Image ButtonImage;


    private void Awake()
    {
        ButtonImage = GetComponent<Image>();

        if (LevelNo % 4 == 0)
        {
            ButtonImage.sprite = RedSprite;
        }
    }

    private void Start()
    {
        SelfText.text = LevelNo.ToString();

        if(LevelNo > PlayerPrefs.GetInt("LevelsUnloked"))
        {
            ButtonComponent.interactable = false;
        }
    }

    private void Update()
    {
    }

    public void OnClickFunction()
    {
        PlayerPrefs.SetInt("OpenLevelNo", LevelNo);
        StartCoroutine(LoadFunction());

    }

    IEnumerator LoadFunction()
    {
        LoadingPanelWTD.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }
}
