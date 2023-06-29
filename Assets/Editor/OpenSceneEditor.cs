using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScenEditor : EditorWindow
{
    private static string _scenePath = "Assets/Scenes/{0}.unity";

    [MenuItem("OpenScene/Menu", false, 1)]
    public static void GameScene()
    {

        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Menu"), OpenSceneMode.Single);
    }
    [MenuItem("OpenScene/Level1", false, 1)]
    public static void Demo()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Level1"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/Level2", false, 1)]
    public static void Demo2Scene()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Level2"), OpenSceneMode.Single);
    }
    [MenuItem("OpenScene/Level3", false, 1)]
    public static void Demo3Scene()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

        EditorSceneManager.OpenScene
           (string.Format(_scenePath, "Level3"), OpenSceneMode.Single);
    }
}
