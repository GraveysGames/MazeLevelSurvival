using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
 
[CustomEditor(typeof(GameItems))]

public class ItemBase_EditorScript : Editor
{
    GameItems t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    void OnEnable()
    {
        t = (GameItems)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("Items"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Resize our list
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        ListSize = ThisList.arraySize;

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        if (GUILayout.Button("Add New Item"))
        {
            t.Items.Add(new GameItems.Item());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("ITEMS");

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);

            SerializedProperty MyString = MyListRef.FindPropertyRelative("itemName");

            SerializedProperty MyItemObject = MyListRef.FindPropertyRelative("itemObject");

            SerializedProperty MyItemSprite = MyListRef.FindPropertyRelative("ItemSprite");

            SerializedProperty MyStatTypes = MyListRef.FindPropertyRelative("statTypes");
            SerializedProperty MyStatUpdateMethod = MyListRef.FindPropertyRelative("statUpdateMethod");
            SerializedProperty MyValueOfChange = MyListRef.FindPropertyRelative("valueOfChange");

            EditorGUILayout.PropertyField(MyString);
            EditorGUILayout.PropertyField(MyItemSprite);
            EditorGUILayout.PropertyField(MyItemObject);

            if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(120), GUILayout.MaxHeight(20)))
            {
                MyValueOfChange.InsertArrayElementAtIndex(MyValueOfChange.arraySize);
                MyValueOfChange.GetArrayElementAtIndex(MyValueOfChange.arraySize - 1).intValue = 0;

                MyStatTypes.InsertArrayElementAtIndex(MyStatTypes.arraySize);
                MyStatTypes.GetArrayElementAtIndex(MyStatTypes.arraySize - 1).intValue = 0;

                MyStatUpdateMethod.InsertArrayElementAtIndex(MyStatUpdateMethod.arraySize);
                MyStatUpdateMethod.GetArrayElementAtIndex(MyStatUpdateMethod.arraySize - 1).intValue = 0;
            }
            var indent = EditorGUI.indentLevel;
            for (int a = 0; a < MyValueOfChange.arraySize; a++)
            {
                
                EditorGUILayout.LabelField("Attribute: " + a);

                
                EditorGUI.indentLevel += 5;

                EditorGUILayout.PropertyField(MyStatTypes.GetArrayElementAtIndex(a), GUIContent.none);
                EditorGUILayout.PropertyField(MyStatUpdateMethod.GetArrayElementAtIndex(a), GUIContent.none);
                EditorGUILayout.PropertyField(MyValueOfChange.GetArrayElementAtIndex(a), GUIContent.none);

                if (GUILayout.Button("Remove  (" + a.ToString() + ")" , GUILayout.MaxWidth(90), GUILayout.MaxHeight(15)))
                {
                    MyValueOfChange.DeleteArrayElementAtIndex(a);
                    MyStatTypes.DeleteArrayElementAtIndex(a);
                    MyStatUpdateMethod.DeleteArrayElementAtIndex(a);
                }


                EditorGUI.indentLevel = indent;
            }

            EditorGUILayout.Space();

            //Remove this index from the List
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}


//Save for reference

/*
        //Update our list

        GetTarget.Update();

        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space();

        //Resize our list
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Define the list size with a number");
        ListSize = ThisList.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        if (GUILayout.Button("Add New Item"))
        {
            t.Items.Add(new ItemsBase.Item());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyString = MyListRef.FindPropertyRelative("itemName");
            SerializedProperty MyStatTypes = MyListRef.FindPropertyRelative("statTypes");
            SerializedProperty MyStatUpdateMethod = MyListRef.FindPropertyRelative("statUpdateMethod");
            SerializedProperty MyValueOfChange = MyListRef.FindPropertyRelative("valueOfChange");

            EditorGUILayout.PropertyField(MyString);
            EditorGUILayout.PropertyField(MyStatTypes);
            EditorGUILayout.PropertyField(MyStatUpdateMethod);
            EditorGUILayout.PropertyField(MyValueOfChange);



            EditorGUILayout.Space();

            //Remove this index from the List
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
*/

/*

    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    ItemsBase t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    void OnEnable()
    {
        t = (ItemsBase)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("Items"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("", DisplayFieldType);

        //Resize our list
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Define the list size with a number");
        ListSize = ThisList.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Or");
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.Items.Add(new ItemsBase.Item());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyString = MyListRef.FindPropertyRelative("itemName");
            SerializedProperty MyStatTypes = MyListRef.FindPropertyRelative("statTypes");
            SerializedProperty MyStatUpdateMethod = MyListRef.FindPropertyRelative("statUpdateMethod");
            SerializedProperty MyValueOfChange = MyListRef.FindPropertyRelative("valueOfChange");


            // Display the property fields in two ways.

            if (DisplayFieldType == 0)
            {// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
                //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
                EditorGUILayout.LabelField("Automatic Field By Property Type");
                EditorGUILayout.PropertyField(MyString);
                EditorGUILayout.PropertyField(MyStatTypes);
                EditorGUILayout.PropertyField(MyStatUpdateMethod);
                EditorGUILayout.PropertyField(MyValueOfChange);

                // Array fields with remove at index
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Array Fields");

                if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    MyValueOfChange.InsertArrayElementAtIndex(MyValueOfChange.arraySize);
                    MyValueOfChange.GetArrayElementAtIndex(MyValueOfChange.arraySize - 1).intValue = 0;
                }

                for (int a = 0; a < MyValueOfChange.arraySize; a++)
                {
                    EditorGUILayout.PropertyField(MyValueOfChange.GetArrayElementAtIndex(a));
                    if (GUILayout.Button("Remove  (" + a.ToString() + ")", GUILayout.MaxWidth(100), GUILayout.MaxHeight(15)))
                    {
                        MyValueOfChange.DeleteArrayElementAtIndex(a);
                    }
                }
            }
            else
            {
                //Or

                //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                EditorGUILayout.LabelField("Customizable Field With GUI");
                //MyGO.objectReferenceValue = EditorGUILayout.ObjectField("My Custom Go", MyGO.objectReferenceValue, typeof(GameObject), true);
                //MyString.stringValue = EditorGUILayout.("My Custom Int", MyString.stringValue);
                //MyFloat.floatValue = EditorGUILayout.FloatField("My Custom Float", MyFloat.floatValue);
                //MyVect3.vector3Value = EditorGUILayout.Vector3Field("My Custom Vector 3", MyVect3.vector3Value);


                // Array fields with remove at index
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Array Fields");

                if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    MyValueOfChange.InsertArrayElementAtIndex(MyValueOfChange.arraySize);
                    MyValueOfChange.GetArrayElementAtIndex(MyValueOfChange.arraySize - 1).intValue = 0;
                }

                for (int a = 0; a < MyValueOfChange.arraySize; a++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("My Custom Int (" + a.ToString() + ")", GUILayout.MaxWidth(120));
                    MyValueOfChange.GetArrayElementAtIndex(a).intValue = EditorGUILayout.IntField("", MyValueOfChange.GetArrayElementAtIndex(a).intValue, GUILayout.MaxWidth(100));
                    if (GUILayout.Button("-", GUILayout.MaxWidth(15), GUILayout.MaxHeight(15)))
                    {
                        MyValueOfChange.DeleteArrayElementAtIndex(a);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space();

            //Remove this index from the List
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
*/

