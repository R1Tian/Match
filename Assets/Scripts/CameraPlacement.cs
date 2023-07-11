using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
    private void Start()
    {
        // 获取屏幕的宽度和高度
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 将摄像机放置在屏幕中心
        Camera.main.transform.position = new Vector3(screenWidth / 2f, screenHeight / 2f, -10f);
    }
}