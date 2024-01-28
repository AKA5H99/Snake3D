using UnityEngine;

public class DontDestroyOnLoadS : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this object
            instance = this.gameObject;
            // Make sure this object is not destroyed when a new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }

    // Your other script logic goes here
    // ...

}