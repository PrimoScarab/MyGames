using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SimpleCaster))]
[CanEditMultipleObjects]
public class SimpleCasterEditor : Editor 
{
    private SerializedProperty color;
    private SerializedProperty target;
    private SerializedProperty castType;
    private SerializedProperty sphereRadius;
    private SerializedProperty layerMask;
    private SerializedProperty trigger;
    private SerializedProperty triggerButttonName;
    private SerializedProperty origin;
    private SerializedProperty originFromMouse;
    private SerializedProperty camera;
    private SerializedProperty actions;

    private SerializedProperty castIntervalInSeconds;

    private SerializedProperty actionMode;

    private bool advancedFoldedOut = false;
    private bool debugFoldedOut = false;
     // Start is called before the first frame update*/
    
    void OnEnable()
    {
        color = serializedObject.FindProperty("color");
        target = serializedObject.FindProperty("target");
        castType = serializedObject.FindProperty("castType");
        sphereRadius = serializedObject.FindProperty("sphereRadius");
        layerMask = serializedObject.FindProperty("layerMask");
        trigger = serializedObject.FindProperty("trigger");
        triggerButttonName = serializedObject.FindProperty("triggerButttonName");
        origin = serializedObject.FindProperty("origin");
        originFromMouse = serializedObject.FindProperty("originFromMouse");
        camera = serializedObject.FindProperty("camera");
        actions = serializedObject.FindProperty("actions");
        castIntervalInSeconds = serializedObject.FindProperty("castIntervalInSeconds");
        actionMode = serializedObject.FindProperty("actionMode");

    }

    
    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        EditorGUILayout.LabelField("Simple caster v1.0.7", EditorStyles.largeLabel);
         EditorGUILayout.LabelField("© 2022 Jonas Svegland", EditorStyles.label);
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Basic settings", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Set the target for this caster and setup the type of cast: Ray or Sphere.", MessageType.None);
        EditorGUILayout.PropertyField(target);
      
        EditorGUILayout.PropertyField(castType);
        if(castType.enumValueIndex == (int)SimpleCaster.CastType.Sphere)
            EditorGUILayout.PropertyField(sphereRadius);

         
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Origin settings", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Set up the origin of the cast, can be either a transform or the mouse projected into the game world. If using the mouse you also have to setup from which camera the cast will be made.", MessageType.None);
        
        EditorGUILayout.PropertyField(originFromMouse);
        if(originFromMouse.boolValue == true)
        {
            EditorGUILayout.PropertyField(camera);
        }
        else
            EditorGUILayout.PropertyField(origin);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Trigger settings", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Choose what will trigger the cast, can be both application phases or buttons and the different button phases.", MessageType.None);
        EditorGUILayout.PropertyField(trigger);
        if( trigger.enumValueIndex == (int)SimpleCaster.CastTrigger.ButtonHeld ||
            trigger.enumValueIndex == (int)SimpleCaster.CastTrigger.ButtonPressed ||
            trigger.enumValueIndex == (int)SimpleCaster.CastTrigger.ButtonReleased)
                EditorGUILayout.PropertyField(triggerButttonName);
        else if(trigger.enumValueIndex == (int)SimpleCaster.CastTrigger.Interval)
            EditorGUILayout.PropertyField(castIntervalInSeconds);

       

        EditorGUILayout.Separator();
        advancedFoldedOut = EditorGUILayout.Foldout(advancedFoldedOut, "Advanced", EditorStyles.foldoutHeader);
        if(advancedFoldedOut)
        {
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.PropertyField(layerMask);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Setup what will happen if a cast hits the target, can be multiple reactions.", MessageType.None);
        EditorGUILayout.PropertyField(actionMode);
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(actions);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif