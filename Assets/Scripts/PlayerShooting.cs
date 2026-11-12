// PlayerShooting.cs
// Maintainer: Qingzhe-Song
// Summary: Fires projectiles while unpaused, consumes ammunition, and plays weapon effects.

using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("Ammunition")]
    [Tooltip("Remaining bullets; each accepted Fire input consumes one bullet.")]
    [FormerlySerializedAs("bulletsAmount")]
    public int remainingBullets = 100;

    public void OnFire(InputValue value)
    {
        // PlayerInput sends this callback while paused too, so firing checks the time scale.
        if (value.isPressed && remainingBullets > 0 && Time.timeScale > 0f)
        {
            remainingBullets--;
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPoint.transform.position;
            projectile.transform.rotation = shootPoint.transform.rotation;
            muzzleEffect.Play();
            shootSound.Play();
        }
    }
}
