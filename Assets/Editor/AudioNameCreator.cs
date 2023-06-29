using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class AudioNameCreator
{
    private const string MENUITEM_PATH = "Tools/Create/Audio Name";
    private const string EXPORT_PATH = "Assets/Stripts/AUDIO.cs";

    private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
    private static readonly string FILENAME_WITHOUT_EXTENTION = Path.GetFileNameWithoutExtension(EXPORT_PATH);

    [MenuItem(MENUITEM_PATH)]
    public static void Create()
    {
        if (!CanCreate())
        {
            return;
        }
        CreateScript();
        EditorUtility.DisplayDialog(FILENAME, "Creation completed!!!", "OK");
    }

    private static void CreateScript()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENTION).AppendLine();
        builder.AppendLine("{");

        object[] musicList = Resources.LoadAll("Audio/Music");
        object[] soundList = Resources.LoadAll("Audio/Sound");

        foreach (AudioClip music in musicList)
        {
            builder.Append("\t").AppendFormat(@" public const string Music_{0} = ""{1}"";", music.name.ToUpper(), music.name).AppendLine();

        }
        builder.AppendLine("\t");
        foreach (AudioClip sound in soundList)
        {
            builder.Append("\t").AppendFormat(@" public const string Sound_{0} = ""{1}"";", sound.name.ToUpper(), sound.name).AppendLine();

        }
        builder.AppendLine("}");

        string dictionaryName = Path.GetDirectoryName(EXPORT_PATH);
        if (!Directory.Exists(dictionaryName))
        {
            Directory.CreateDirectory(dictionaryName);
        }

        File.WriteAllText(EXPORT_PATH, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    [MenuItem(MENUITEM_PATH, true)]
    private static bool CanCreate()
    {
        return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
    }
}
