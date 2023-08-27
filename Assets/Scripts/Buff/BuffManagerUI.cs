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
    public static Transform PlayerBuff;
    public static Transform EnemyBuff;

    [ShowInInspector] private static List<GameObject> buffGameObjects;

    private void Awake()
    {
        buffGameObjects = new List<GameObject>();
        buffPrefab = Resources.Load("Buff_UI").GameObject();
        BuffManager = gameObject;
        //0是BuffManager的Transform，1是PlayerBuff，2是EnemyBuff
        PlayerBuff = BuffManager.GetComponentsInChildren<Transform>()[1];
        EnemyBuff = BuffManager.GetComponentsInChildren<Transform>()[2];
    }

    /// <summary>
    /// 生成一个不可叠加层数的buff预制体
    /// </summary>
    /// <param name="buffObject">buffObject数据</param>
    /// <param name="duration">持续时间</param>
    public static void GenerateBuffPrefab(BuffObject buffObject,int duration,Transform target)
    {
        Buff buff = new Buff();
        buff.ReadBuffData(buffObject);
        GameObject buffGameObject = Instantiate(buffPrefab, target);
        buffGameObjects.Add(buffGameObject);
        buffGameObject.GetComponent<Image>().sprite = buff.GetSprite();
        //textMeshProUGUIs[0]是持续时间，textMeshProUGUIs[1]是叠加层数
        TextMeshProUGUI[] textMeshProUGUIs = buffGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        textMeshProUGUIs[0].text = duration.ToString();
        textMeshProUGUIs[1].gameObject.SetActive(false);
    }

    public static void GenerateStackableBuffPrefab(BuffObject buffObject,int duration,int stackLayers,Transform target)
    {
        Buff buff = new Buff();
        buff.ReadBuffData(buffObject);
        if (buffObject.Stackable)
        {
            GameObject buffGameObject = Instantiate(buffPrefab, target);
            buffGameObjects.Add(buffGameObject);
            buffGameObject.GetComponent<Image>().sprite = buff.GetSprite();
            //textMeshProUGUIs[0]是持续时间，textMeshProUGUIs[1]是叠加层数
            TextMeshProUGUI[] textMeshProUGUIs = buffGameObject.GetComponentsInChildren<TextMeshProUGUI>();
            textMeshProUGUIs[0].text = duration.ToString();
            textMeshProUGUIs[1].text = stackLayers.ToString();
        }
        else
        {
            GenerateBuffPrefab(buffObject, duration,target);
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
