using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersScript : MonoBehaviour
{
    public Camera PlayerCam;
    public Player_Gravity Player_GravityScript;
    public SnakeController SnakeControllerScript;
    public float MagnetRange = 5;
    public Material[] AllPlayerMaterials;
    public Renderer PlayerRenderer, BodyPrefabRenderer;
    public GameObject BodyGameObject,JumpBGameObject;
    public CharactersData CharactersDataScript;


    private void OnEnable()
    {
        PlayerRenderer.sharedMaterial = AllPlayerMaterials[PlayerPrefs.GetInt("SelectedCharNo") - 1];
        BodyPrefabRenderer.sharedMaterial = AllPlayerMaterials[PlayerPrefs.GetInt("SelectedCharNo") - 1];
        if (PlayerPrefs.GetInt("SelectedCharNo") != 2)
        {
            BodyGameObject.transform.localScale = transform.localScale = new Vector3(1, 1, 1);
        }

        CharactersPowerInstantiate();
    }


    private void Update()
    {
        MagnetFunction();
    }

    public void MagnetFunction()
    {
        if (gameObject.name == "6")
        {
            Collider[] Colliders = Physics.OverlapSphere(transform.position, MagnetRange);
            foreach (Collider NearbyObjects in Colliders)
            {

                if (NearbyObjects.gameObject.tag == "Fruit")
                {
                    Debug.Log("Under Redios");
                    NearbyObjects.transform.position = Vector3.MoveTowards(NearbyObjects.transform.position, transform.position + new Vector3(0,0,0.6f), 5.5f * Time.deltaTime);
                }
            }
        }
    }

    //This Function Wasnt Used In This Script It Was used By GSAwakeScript (Affected By GSAwakeScript)
    public void CharactersPowerInstantiate()
    {
        switch (PlayerPrefs.GetInt("SelectedCharNo"))
        {
            case 1:
                //Normal
                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;
                break;

            case 2:

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;

                transform.localScale = transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                BodyGameObject.transform.localScale = transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                PlayerCam.fieldOfView = 80;

                break;

            case 3:

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 150;
                break;

            case 4:

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;

                Player_GravityScript.CanJump = true;
                JumpBGameObject.SetActive(true);
                break;

            case 5:

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;

                transform.localScale = transform.localScale = new Vector3(2, 2, 2);
                BodyGameObject.transform.localScale = transform.localScale = new Vector3(2, 2, 2);
                SnakeControllerScript.Gap = 25;
                transform.position += new Vector3(0, 0.5f, 0);
                break;

            case 6:
                //Magnet

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;
                break;

            case 7:

                CharactersDataScript.PlayerDefaultMoveSpeed = SnakeControllerScript.MoveSpeed = 5;
                CharactersDataScript.PlayerDefaultSteerSpeed = SnakeControllerScript.SteerSpeed = 100;

                Player_GravityScript.CanJump = true;
                //Magnet Also
                break;
        }
    }
}
