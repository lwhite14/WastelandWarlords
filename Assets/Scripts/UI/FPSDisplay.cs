using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsText;
    public float pollingTime = 0.25f;
    float time;
    int frameCount;

    void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingTime) 
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
