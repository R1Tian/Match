//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Card))]
//public class CardEditor : Editor
//{
//    // 序列化对象
//    private SerializedObject card;

//    // 序列化属性
//    private SerializedProperty id;
//    private SerializedProperty Color;
//    private SerializedProperty ColorType;
//    private SerializedProperty Tetromino;
//    private SerializedProperty skillEffect;

//    //Card card;

//    //SerializedProperty tetromino;

//    private void OnEnable()
//    {
//        // 获取当前的序列化对象（target：当前检视面板中显示的对象）
//        card = new SerializedObject(target);
        

//        // 抓取对应的序列化属性
//        id = card.FindProperty("id");
//        ColorType = card.FindProperty("ColorType");
//        Color = card.FindProperty("Color");
//        //Debug.Log(ColorType);
//        //Tetromino = card.FindProperty("Tetromino");
//        //skillEffect = card.FindProperty("skillEffect");

//    }


//    public override void OnInspectorGUI()
//    {
//        // 从物体上抓取最新的信息
//        card.Update();
//        EditorGUILayout.PropertyField(id);
//        var pop = card.GetIterator();
//        while (pop.NextVisible(true))
//        {
//            Debug.Log(pop.propertyPath);
//        }
//        //EditorGUILayout.EnumPopup()
//        //EditorGUILayout.PropertyField(ColorType);
//        //EditorGUILayout.PropertyField(Color);
//        //if (type.enumValueIndex == 0)
//        //{
//        //    EditorGUILayout.PropertyField(enemyHP);
//        //}
//        //else
//        //{
//        //    EditorGUILayout.PropertyField(playerHP);
//        //}

//        card.ApplyModifiedProperties();

//        //// 显示默认的Card脚本Inspector
//        //DrawDefaultInspector();

//        //EditorGUILayout.Space();

//        //// 自定义Card属性的字段
//        //card.Color = EditorGUILayout.ColorField("颜色", card.Color);
//        ////card.Tetromino = (Tetromino)EditorGUILayout.PropertyField(tetromino)/*("Tetromino", card.Tetromino, typeof(Tetromino), false);*/
//        ////card.SkillEffect = (System.Action)EditorGUILayout.ObjectField("技能效果", card.SkillEffect, typeof(System.Action), false);

//        //if (GUILayout.Button("保存卡牌"))
//        //{
//        //    EditorUtility.SetDirty(card);
//        //    AssetDatabase.SaveAssets();
//        //    AssetDatabase.Refresh();
//        //}
//    }
//}