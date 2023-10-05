using UnityEngine;
using System.IO;
public class ScreenShoter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {

        ScreenCapture.CaptureScreenshot("ScreenShots/" + Application.productName + "_" + Time.captureDeltaTime + Time.time + ".png" );

        Debug.Log("Screenshot saved.");
    }
}
