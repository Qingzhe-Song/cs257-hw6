// ReadmeEditor.cs
// Source: Unity project template.
// Maintainer: Qingzhe-Song
// Summary: Displays the project introduction, loads its layout, and provides tutorial cleanup.

using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(Readme))]
[InitializeOnLoad]
public class ReadmeEditor : Editor
{
    private const string ReadmeSessionStateKey = "ReadmeEditor.showedReadme";

    private const string TutorialDirectory = "Assets/TutorialInfo";

    private const float SectionSpacing = 16f;

    static ReadmeEditor()
    {
        EditorApplication.delayCall += SelectReadmeAutomatically;
    }

    private static void RemoveTutorial()
    {
        if (EditorUtility.DisplayDialog("Remove Readme Assets",
            $"All contents under {TutorialDirectory} will be removed, are you sure you want to proceed?",
            "Proceed",
            "Cancel"))
        {
            if (Directory.Exists(TutorialDirectory))
            {
                FileUtil.DeleteFileOrDirectory(TutorialDirectory);
                FileUtil.DeleteFileOrDirectory(TutorialDirectory + ".meta");
            }
            else
            {
                Debug.Log($"Could not find the Readme folder at {TutorialDirectory}");
            }

            var readmeAsset = SelectReadme();
            if (readmeAsset != null)
            {
                var path = AssetDatabase.GetAssetPath(readmeAsset);
                FileUtil.DeleteFileOrDirectory(path + ".meta");
                FileUtil.DeleteFileOrDirectory(path);
            }

            AssetDatabase.Refresh();
        }
    }

    private static void SelectReadmeAutomatically()
    {
        if (!SessionState.GetBool(ReadmeSessionStateKey, false))
        {
            var readme = SelectReadme();
            SessionState.SetBool(ReadmeSessionStateKey, true);

            if (readme && !readme.loadedLayout)
            {
                LoadLayout();
                readme.loadedLayout = true;
            }
        }
    }

    private static void LoadLayout()
    {
        var assembly = typeof(EditorApplication).Assembly;
        var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
        var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
        method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "TutorialInfo/Layout.wlt"), false });
    }

    private static Readme SelectReadme()
    {
        var ids = AssetDatabase.FindAssets("Readme t:Readme");
        if (ids.Length == 1)
        {
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

            Selection.objects = new UnityEngine.Object[] { readmeObject };

            return (Readme)readmeObject;
        }
        else
        {
            Debug.Log("Couldn't find a readme");
            return null;
        }
    }

    protected override void OnHeaderGUI()
    {
        var readme = (Readme)target;
        InitializeStyles();

        var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

        GUILayout.BeginHorizontal("In BigTitle");
        {
            if (readme.icon != null)
            {
                GUILayout.Space(SectionSpacing);
                GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
            }
            GUILayout.Space(SectionSpacing);
            GUILayout.BeginVertical();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(readme.title, TitleStyle);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }

    public override void OnInspectorGUI()
    {
        var readme = (Readme)target;
        InitializeStyles();

        foreach (var section in readme.sections)
        {
            if (!string.IsNullOrEmpty(section.heading))
            {
                GUILayout.Label(section.heading, HeadingStyle);
            }

            if (!string.IsNullOrEmpty(section.text))
            {
                GUILayout.Label(section.text, BodyStyle);
            }

            if (!string.IsNullOrEmpty(section.linkText))
            {
                if (DrawLinkLabel(new GUIContent(section.linkText)))
                {
                    Application.OpenURL(section.url);
                }
            }

            GUILayout.Space(SectionSpacing);
        }

        if (GUILayout.Button("Remove Readme Assets", ButtonStyle))
        {
            RemoveTutorial();
        }
    }

    private bool _initialized;

    private GUIStyle LinkStyle
    {
        get { return _linkStyle; }
    }

    [Header("Text Styles")]
    [FormerlySerializedAs("m_LinkStyle")]
    [SerializeField]
    private GUIStyle _linkStyle;

    private GUIStyle TitleStyle
    {
        get { return _titleStyle; }
    }

    [FormerlySerializedAs("m_TitleStyle")]
    [SerializeField]
    private GUIStyle _titleStyle;

    private GUIStyle HeadingStyle
    {
        get { return _headingStyle; }
    }

    [FormerlySerializedAs("m_HeadingStyle")]
    [SerializeField]
    private GUIStyle _headingStyle;

    private GUIStyle BodyStyle
    {
        get { return _bodyStyle; }
    }

    [FormerlySerializedAs("m_BodyStyle")]
    [SerializeField]
    private GUIStyle _bodyStyle;

    private GUIStyle ButtonStyle
    {
        get { return _buttonStyle; }
    }

    [FormerlySerializedAs("m_ButtonStyle")]
    [SerializeField]
    private GUIStyle _buttonStyle;

    private void InitializeStyles()
    {
        if (_initialized)
        {
            return;
        }
        _bodyStyle = new GUIStyle(EditorStyles.label);
        _bodyStyle.wordWrap = true;
        _bodyStyle.fontSize = 14;
        _bodyStyle.richText = true;

        _titleStyle = new GUIStyle(_bodyStyle);
        _titleStyle.fontSize = 26;

        _headingStyle = new GUIStyle(_bodyStyle);
        _headingStyle.fontStyle = FontStyle.Bold;
        _headingStyle.fontSize = 18;

        _linkStyle = new GUIStyle(_bodyStyle);
        _linkStyle.wordWrap = false;

        // Match the selection color so links remain readable on light and dark skins.
        _linkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        _linkStyle.stretchWidth = false;

        _buttonStyle = new GUIStyle(EditorStyles.miniButton);
        _buttonStyle.fontStyle = FontStyle.Bold;

        _initialized = true;
    }

    private bool DrawLinkLabel(GUIContent label, params GUILayoutOption[] options)
    {
        var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

        Handles.BeginGUI();
        Handles.color = LinkStyle.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();

        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

        return GUI.Button(position, label, LinkStyle);
    }
}
