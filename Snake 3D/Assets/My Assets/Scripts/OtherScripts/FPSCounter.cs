using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    // Text Name Must Be FPSText

    private Text FPSText;
    private float updateInterval = 0.5f;

    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        FindFPSText();


        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0f)
        {
            // Display FPS in the console
            float fps = accum / frames;
            fps = Mathf.RoundToInt(fps);

            FPSText.text = "FPS " + fps.ToString();
            // Reset variables for the next interval
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void FindFPSText()
    {
        if(FPSText == null)
        {
            // Find the GameObject by name
            GameObject foundObject = GameObject.Find("FPSText");

            // Check if the GameObject was found
            if (foundObject != null)
            {
                // Try to get the Text component
                FPSText = foundObject.GetComponent<Text>();

            }
        }
        

    }
}