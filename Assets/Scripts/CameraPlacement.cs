using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
    private void Start()
    {
        // ��ȡ��Ļ�Ŀ�Ⱥ͸߶�
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ���������������Ļ����
        Camera.main.transform.position = new Vector3(screenWidth / 2f, screenHeight / 2f, -10f);
    }
}