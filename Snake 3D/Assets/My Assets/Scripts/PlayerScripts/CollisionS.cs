using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollisionS : MonoBehaviour
{

    //Settings
    public float GroundScaleValue = 2, SteerSpeedIncreaseValue = 180, MoveSpeedIncreaseValue = 8, SmallSizeValue = 0.5f;
    public GameObject[] UnknownBoxItems;
    public int ShieldValue;
    [SerializeField] public int CurrentScore; 




    //Refrences
    [Space]
    public SnakeController SnakeControllerScript;
    public Player_Gravity player_GravityScript;
    public GameObject Ground,GameOverPanel,FruitPrefab;
    public TextMeshProUGUI CurrentScoreText, HighScoreText;
    public GButtons GButtonsScript;
    public AudioSource EatSound;
    public LevelManagerS LevelManagerScript;
    public Animator PlayerCamAnimator;
    public TimeBody TimeBodyScript;
    public Transform BodySpawnPos;
    public CharactersData CharactersDataScript;
    //
    public GameObject AdsLogo;
    public Text RewindValueTxt;

    //Bools
    [SerializeField]
    public bool DeadBool;
 

    private void Start()
    {
        // CurrentScore = 0 (in Starting)
        CurrentScore = 0;
    }

    private void Update()
    {
        //Updating Score Text
        CurrentScoreText.text = "Score " + CurrentScore.ToString();
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Using Switch To Compaire Tags
        if (!GButtonsScript.isRewinding)
        {
            switch (other.gameObject.tag)
            {

                // Increase Body And Score By eating Fruit
                case "Fruit":
                    SnakeControllerScript.UseNewSpawnPosition = true;
                    SnakeControllerScript.GrowSnake();
                    SpawnFruitAtRandomPlace();

                    //Updating LevelData if Fruit Is A last fruit
                    LevelManagerScript.LevelProgressManager();
                    Destroy(other.gameObject);

                    // Using ScoreManager to update CurrentScore And HighScore
                    ScoreManager();
                    break;

                // Time Stop(Dead) when collision with body
                case "Body":
                    GameOver();
                    break;

                case "Border":
                    GameOver();
                    break;

                case "Obstecle":
                    GameOver();
                    break;

                // Scaling Ground Area You can Adjust Scale Value on top settings Called GroundScaleValue
                case "IncreaseArea":
                    Ground.transform.localScale += new Vector3(GroundScaleValue, 0, GroundScaleValue);
                    Destroy(other.gameObject);
                    break;

                // CanJump = true In Player_GravityScript For Jump Function Activation
                case "Jump":
                    player_GravityScript.CanJump = true;
                    Destroy(other.gameObject);
                    break;

                // Increasing Steer Speed By changing SnakeControllerScript's SteerSpeed
                case "SteerSpeedIncreaser":
                    SnakeControllerScript.SteerSpeed = SteerSpeedIncreaseValue;
                    Destroy(other.gameObject);
                    break;

                // Increasing MoveSpeed
                case "MoveSpeedIncreaser":
                    SnakeControllerScript.MoveSpeed = MoveSpeedIncreaseValue;
                    Destroy(other.gameObject);
                    break;

                // UnknownBox Can Instantiate Random Items
                case "UnknownBox":
                    Instantiate(UnknownBoxItems[Random.Range(0, UnknownBoxItems.Length)], other.transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                    break;

                // Affecting SnakeController Script
                case "OppositeSideController":
                    SnakeControllerScript.SteerChanger = -1;
                    Destroy(other.gameObject);
                    break;

                // Colliding With Shield item will Increase ShieldValue (ShieldValue = Health)
                //currently not working
                case "Shield":
                    ShieldValue += 1;
                    Debug.Log(ShieldValue);
                    Destroy(other.gameObject);
                    break;

                //currently not working
                case "SmallSize":
                    transform.localScale = new Vector3(SmallSizeValue, SmallSizeValue, SmallSizeValue);
                    Destroy(other.gameObject);
                    break;

                case "Magnet":
                    transform.localScale = new Vector3(SmallSizeValue, SmallSizeValue, SmallSizeValue);
                    Destroy(other.gameObject);
                    break;
            }
        }
        
            
    }

    public void ScoreManager()
    {
        // Increasing Score
        CurrentScore += 1;

        // Comparing HighScore with CurrentScore
        if (CurrentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }

        //sound
        EatSound.Play();
        //Vibrate
        Vibration.Vibrate(15);

    }

    public void GameOver()
    {
        if(!GButtonsScript.GameIsPaused )
        {
            //Upadting Rewind Value Text On Every GameOver
            RewindValueTxt.text = PlayerPrefs.GetInt("RewindValue").ToString();

            //If Rewind Value Is 0 Then Show Ads Logo in Bottom
            if (PlayerPrefs.GetInt("RewindValue") == 0)
            {
                AdsLogo.SetActive(true);
                RewindValueTxt.enabled = false;
            }

            GButtonsScript.CanNotRecrord = true;
            Debug.Log("Dead Bruuu!");
            SnakeControllerScript.SteerSpeed = 0;
            SnakeControllerScript.MoveSpeed = 0;
            player_GravityScript.CanJump = false;
            //panel
            GameOverPanel.SetActive(true);

            //CamAnimation
            PlayerCamAnimator.SetBool("GameOver", true);

            //Vibrate
            Vibration.Vibrate(100);


        }
    }

    public void ContinueF()
    {
        if (!GButtonsScript.GameIsPaused)
        {

                GButtonsScript.CanNotRecrord = false;
                Debug.Log("Revived");

                SnakeControllerScript.SteerSpeed = CharactersDataScript.PlayerDefaultSteerSpeed;
                SnakeControllerScript.MoveSpeed = CharactersDataScript.PlayerDefaultMoveSpeed;
                player_GravityScript.CanJump = true;
                //panel
                GameOverPanel.SetActive(false);

                //CamAnimation
                PlayerCamAnimator.SetBool("GameOver", false);

                StartCoroutine(BodyPosAfterReverseF());

        }
    }

    public void SpawnFruitAtRandomPlace()
    {
        Instantiate(FruitPrefab, new Vector3(Random.Range(-13.5f, 13.5f), 0, Random.Range(-13.5f, 13.5f)), Quaternion.identity); //Used Values Are Also Used In FruitScript

    }

    IEnumerator BodyPosAfterReverseF()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < SnakeControllerScript.BodyParts.Count; i++)
        {
            if (i < 8)
                continue;
            else
                SnakeControllerScript.BodyParts[i].transform.position = BodySpawnPos.position;
        }

    }
        

}
