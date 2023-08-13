using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRenderModeManager : MonoBehaviour
{
    public static Canvas canvas;
    public static Camera camera;

    private void Awake()
    {
        canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public static void ToScreenSpaceCamera()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
    }

    public static void ToOverlay()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
