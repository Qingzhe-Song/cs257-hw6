// Readme.cs
// Source: Unity project template.
// Maintainer: Qingzhe-Song
// Summary: Stores the project introduction and its Inspector sections.

using System;
using UnityEngine;

public class Readme : ScriptableObject
{
    [Header("Introduction")]
    [Tooltip("Image displayed above the project introduction.")]
    public Texture2D icon;
    public string title;
    [Header("Content")]
    public Section[] sections;
    [Header("Editor State")]
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        [Header("Section Content")]
        public string heading;
        public string text;

        [Header("Link")]
        public string linkText;
        public string url;
    }
}
