using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Linq;

public class SceneSelectorWithShortcut : EditorWindow
{
    [MenuItem("Tools/Scene Selector %&s")] // Ctrl + Alt + Sで開く
    public static void ShowWindow()
    {
        GetWindow<SceneSelectorWithShortcut>("Scene Selector");
    }

    void OnGUI()
    {
        // シーンのGUIDを取得
        string[] sceneGUIDs = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });

        // シーンの名前と順番を定義（ここで順番を指定）
        string[] customOrder = { "Title", "Stage1", "Stage2", "Stage3", "Result" };

        // GUIDをパスに変換して、ファイル名を取得
        var scenePaths = sceneGUIDs
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => new { path, name = Path.GetFileNameWithoutExtension(path) })
            .ToList();

        // 定義した順番に並べ替え
        var sortedScenes = customOrder
            .Select(name => scenePaths.FirstOrDefault(s => s.name == name)) // 名前が一致するシーンを並べ替え
            .Where(scene => scene != null) // nullを除外
            .ToList();

        // 並べ替えたシーンを表示
        foreach (var scene in sortedScenes)
        {
            if (GUILayout.Button(scene.name))
            {
                // 現在のシーンに保存が必要なら保存を促し、選択したシーンを開く
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    if (File.Exists(scene.path))
                    {
                        EditorSceneManager.OpenScene(scene.path);
                    }
                    else
                    {
                        Debug.LogError($"Scene file not found: {scene.path}");
                    }
                }
            }
        }
    }
}
