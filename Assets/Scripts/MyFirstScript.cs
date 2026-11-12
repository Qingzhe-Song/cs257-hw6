// MyFirstScript.cs
// Maintainer: Qingzhe-Song
// Summary: Demonstrates Unity lifecycle messages by logging Start, an example value, and destruction.

using UnityEngine;
using UnityEngine.Serialization;

public class MyFirstScript : MonoBehaviour
{
    [Header("Logging Example")]
    [Tooltip("Example value printed each frame; this demonstration does not move the object.")]
    [SerializeField]
    [FormerlySerializedAs("speed")]
    private float exampleValue;

    private void Start()
    {
        print("Test Start");
    }

    private void Update()
    {
        print(exampleValue);
    }

    private void OnDestroy()
    {
        print("Test Destroyed");
    }
}
