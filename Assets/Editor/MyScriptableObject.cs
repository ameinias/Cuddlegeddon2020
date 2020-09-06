using UnityEngine;
using UnityEditor;

static class MyScriptableObject
{

    [MenuItem("Assets/Create/Projectile")]
    public static void CreateYourScriptableObject()
    {
        ScriptableObjectUtility.CreateAsset<ProjectileObject>();
    }

}