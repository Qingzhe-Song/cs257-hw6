// ContactDestroyer.cs
// Maintainer: Qingzhe-Song
// Summary: Destroys both its own GameObject and the other collider's GameObject on trigger contact.

using UnityEngine;

public class ContactDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
