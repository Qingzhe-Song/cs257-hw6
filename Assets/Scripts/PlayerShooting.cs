// PlayerShooting.cs
// Maintainer: Qingzhe-Song
// Summary: Spawns a projectile at the weapon's shoot point and plays the muzzle effect.

using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectile")]
    [InspectorName("Projectile Prefab")]
    [Tooltip("Projectile prefab instantiated when the Fire input calls OnFire.")]
    [FormerlySerializedAs("prefab")]
    public GameObject projectilePrefab;
    [Tooltip("Object whose position and rotation define the projectile's spawn pose.")]
    public GameObject shootPoint;

    [Header("Weapon Effects")]
    [Tooltip("Particle system played each time the player fires.")]
    public ParticleSystem muzzleEffect;

    [Tooltip("Audio source played each time the player fires.")]
    public AudioSource shootSound;

    public void OnFire()
    {
        // Keep this callback name so the PlayerInput Send Messages binding can find it.
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootPoint.transform.position;
        projectile.transform.rotation = shootPoint.transform.rotation;
        muzzleEffect.Play();
        shootSound.Play();
    }
}
