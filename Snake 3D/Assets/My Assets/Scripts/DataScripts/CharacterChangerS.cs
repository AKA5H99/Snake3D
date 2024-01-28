using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterChangerS : MonoBehaviour
{
    public Text Nametxt,Depscription,SelectBTxt, CharUnlockTipText;
    public int CharNo = 1,CharUnlockLevel;
    public GameObject CharacterHolder;
    public Button SelectButton;
    public bool ClearData;


    private void Awake()
    {
        // Seting Level Unloked Value = 1 if level Unloaked is deleted
        if (PlayerPrefs.GetInt("LevelsUnloked") <= 0)
        {
            PlayerPrefs.SetInt("LevelsUnloked", 1);

            PlayerPrefs.SetInt("SelectedCharNo",1);
        }

        CharNo = PlayerPrefs.GetInt("SelectedCharNo");

        if(ClearData)
        {
            PlayerPrefs.DeleteAll();
        }
        
    }

    private void Start()
    {
        Changer();
        CharacterHolder.transform.position = new Vector3(0,0, PlayerPrefs.GetFloat("CharactersHolderPostion"));

        if(PlayerPrefs.GetInt("SelectedCharNo") == 0)
        {
            CharNo = 1;
        }
        else
        {
            CharNo = PlayerPrefs.GetInt("SelectedCharNo");
        }

        // Selected Or Not in starting
        if (CharNo == PlayerPrefs.GetInt("SelectedCharNo"))
        {
            SelectBTxt.text = "Selected";
        }
        else
        {
            SelectBTxt.text = "Select";
        }

        //PlayerPrefs.DeleteAll();
    }

    public void SelectB()
    {
            PlayerPrefs.SetInt("SelectedCharNo", CharNo);
            SelectBTxt.text = "Selected";
            PlayerPrefs.SetFloat("CharactersHolderPostion", CharacterHolder.transform.position.z);
    }
    public void LeftMoveB()
    {
        if (CharNo == 1)
        {

        }
        else
        {
            CharNo -= 1;
            CharacterHolder.transform.position += new Vector3(0, 0, -4);
            Changer();

            if (CharNo == PlayerPrefs.GetInt("SelectedCharNo"))
            {
                SelectBTxt.text = "Selected";
            }
            else
            {
                SelectBTxt.text = "Select";
            }
        }
    }

    public void RightMoveB()
    {

        if (CharNo == 7)
        {

        }
        else
        {
            CharNo += 1;
            CharacterHolder.transform.position += new Vector3(0, 0, 4);
            Changer();

            if (CharNo == PlayerPrefs.GetInt("SelectedCharNo"))
            {
                SelectBTxt.text = "Selected";
            }
            else
            {
                SelectBTxt.text = "Select";
            }
        }
        
    }

    public void Changer()
    {
        
        switch (CharNo)
        {
            case 1:
                CharUnlockLevel = 0;
                Nametxt.text = "Laalua";
                Depscription.text = "Laal Hai Pura Thela Laal Hai";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 1);
                break;

            case 2:
                CharUnlockLevel = 3;  //Unlock Level
                Nametxt.text = "Chota Chatri";
                Depscription.text = "Naam Hi kaphi hai";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 2);
                break;

            case 3:
                CharUnlockLevel = 5;  //Unlock Level 5
                Nametxt.text = "The Flash";
                Depscription.text = "The Movement";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 3);
                break;

            case 4:
                CharUnlockLevel = 8;  //Unlock Level
                Nametxt.text = "Jump Master";
                Depscription.text = "This cube Has a powr to jump";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 4);
                break;

            case 5:
                CharUnlockLevel = 100;  //Unlock Level
                Nametxt.text = "Hulk";
                Depscription.text = "Hulk!";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 5);
                break;

            case 6:
                CharUnlockLevel = 100;  //Unlock Level
                Nametxt.text = "Magnet";
                Depscription.text = "Magnetic Power";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 6);
                break;

            case 7:
                CharUnlockLevel = 100;  //Unlock Level
                Nametxt.text = "The God";
                Depscription.text = "Too many powers but not all";

                //Seting SelectedCharacterNumber 
                PlayerPrefs.SetInt("SelectedCharacterNumber", 7);
                break;

        }
    }

    private void Update()
    {
        CharacterCheker();
    }

    //This Funclion Check That The Character you are looking at in store is lock or not
    public void CharacterCheker()
    {

        if (CharUnlockLevel < PlayerPrefs.GetInt("LevelsUnloked"))
        {
            SelectButton.interactable = true;
            CharUnlockTipText.text = "";
        }
        else if(CharUnlockLevel == 100)
        {
            SelectButton.interactable = false;
            CharUnlockTipText.text = "Under Development";
        }
        else if(CharUnlockLevel >= PlayerPrefs.GetInt("LevelsUnloked"))
        {
            SelectButton.interactable = false;
            CharUnlockTipText.text = "Unlock At Level " + CharUnlockLevel;
        }
    }

}
