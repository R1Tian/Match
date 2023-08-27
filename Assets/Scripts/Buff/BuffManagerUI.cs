using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffManagerUI : MonoBehaviour
{
    static GameObject buffPrefab;
    static GameObject BuffManager;

    [ShowInInspector]
    private static List<GameObject> buffGameObjects = new List<GameObject>();

    private void Awake()
    {
        buffPrefab = Resources.Load("Buff_UI").GameObject();
        BuffManager = gameObject;
    }

    public static void GenerateBuffPrefab(BuffObject buffObject,int duration)
    {
        Buff buff = new Buff();
        buff.ReadBuffData(buffObject);
        GameObject buffGameObject = Instantiate(buffPrefab, BuffManager.transform);
        buffGameObjects.Add(buffGameObject);
        buffGameObject.GetComponent<Image>().sprite = buff.GetSprite();
        //textMeshProUGUIs[0]是持续时间，textMeshProUGUIs[1]是叠加层数
        TextMeshProUGUI[] textMeshProUGUIs = buffGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        textMeshProUGUIs[0].text = duration.ToString();
        textMeshProUGUIs[1].gameObject.SetActive(false);
    }

    public static void GenerateStackableBuffPrefab(BuffObject buffObject,int duration,int stackLayers)
    {
        Buff buff = new Buff();
        buff.ReadBuffData(buffObject);
        if (buffObject.Stackable)
        {
            GameObject buffGameObject = Instantiate(buffPrefab, BuffManager.transform);
            buffGameObjects.Add(buffGameObject);
            buffGameObject.GetComponent<Image>().sprite = buff.GetSprite();
            //textMeshProUGUIs[0]是持续时间，textMeshProUGUIs[1]是叠加层数
            TextMeshProUGUI[] textMeshProUGUIs = buffGameObject.GetComponentsInChildren<TextMeshProUGUI>();
            textMeshProUGUIs[0].text = duration.ToString();
            textMeshProUGUIs[1].text = stackLayers.ToString();
        }
        else
        {
            GenerateBuffPrefab(buffObject, duration);
        }
    }
    
    public static void UpdateBuffPrefab()
    {
        for (int i = buffGameObjects.Count - 1; i >= 0; i--)
        {
            TextMeshProUGUI textMeshProUGUI = buffGameObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            int textToInt = textMeshProUGUI.text.ToInt();
            textToInt--;
            if (textToInt != 0)
            {
                textMeshProUGUI.text = textToInt.ToString();
            }
            else
            {
                Destroy(buffGameObjects[i]);
                buffGameObjects.Remove(buffGameObjects[i]);

            }
        }
    }
    
    public static void UpdateStackableBuffPrefab(BuffObject buffObject,int stackLayers)
    {

        GameObject buffGameObject = GetBuffGameObjectByBuffObject(buffObject);
        //textMeshProUGUIs[0]是持续时间，textMeshProUGUIs[1]是叠加层数
        TextMeshProUGUI[] textMeshProUGUIs = buffGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        textMeshProUGUIs[1].text = stackLayers.ToString();
    }
    
    public static void ResetBuffPrefabDuration(BuffObject buffObject, int duration)
    {
        GameObject buffGameObject = GetBuffGameObjectByBuffObject(buffObject);
        TextMeshProUGUI textMeshProUGUI = buffGameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = duration.ToString();
    }

    #region tool

    static GameObject GetBuffGameObjectByBuffObject(BuffObject buffObject)
    {
        return buffGameObjects.Find(c => c.GetComponent<Image>().sprite == buffObject.sprite);
    }

    #endregion
}
