using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    public bool InternetIsOn;

    private void Awake()
    {
        IsInternetAvailable();
    }
    private void IsInternetAvailable()
    {
        InternetIsOn = Application.internetReachability != NetworkReachability.NotReachable;
    }

}
