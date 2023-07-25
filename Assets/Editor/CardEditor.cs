using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    // ���л�����
    private SerializedObject card;

    // ���л�����
    private SerializedProperty ColorType;
    private SerializedProperty Tetromino;
    private SerializedProperty skillEffect;

    //Card card;

    //SerializedProperty tetromino;

    private void OnEnable()
    {
        // ��ȡ��ǰ�����л�����target����ǰ�����������ʾ�Ķ���
        card = new SerializedObject(target);
        

        // ץȡ��Ӧ�����л�����
        ColorType = card.FindProperty("ColorType");
        Debug.Log(ColorType);
        Tetromino = card.FindProperty("Tetromino");
        //skillEffect = card.FindProperty("skillEffect");

    }


    public override void OnInspectorGUI()
    {
        // ��������ץȡ���µ���Ϣ
        card.Update();

        //EditorGUILayout.PropertyField(ColorType);

        //if (type.enumValueIndex == 0)
        //{
        //    EditorGUILayout.PropertyField(enemyHP);
        //}
        //else
        //{
        //    EditorGUILayout.PropertyField(playerHP);
        //}

        card.ApplyModifiedProperties();

        //// ��ʾĬ�ϵ�Card�ű�Inspector
        //DrawDefaultInspector();

        //EditorGUILayout.Space();

        //// �Զ���Card���Ե��ֶ�
        //card.Color = EditorGUILayout.ColorField("��ɫ", card.Color);
        ////card.Tetromino = (Tetromino)EditorGUILayout.PropertyField(tetromino)/*("Tetromino", card.Tetromino, typeof(Tetromino), false);*/
        ////card.SkillEffect = (System.Action)EditorGUILayout.ObjectField("����Ч��", card.SkillEffect, typeof(System.Action), false);

        //if (GUILayout.Button("���濨��"))
        //{
        //    EditorUtility.SetDirty(card);
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //}
    }
}