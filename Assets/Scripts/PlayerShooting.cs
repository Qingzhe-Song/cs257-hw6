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

    public float fireRate = 0.2f;
    private Animator animator;

    public void OnFire(InputValue value)
    {
        animator.SetBool("Shooting", value.isPressed);

        if (value.isPressed)
        {
            InvokeRepeating(nameof(Shoot), fireRate, fireRate);
        }
        else
        {
            CancelInvoke(nameof(Shoot));
        }
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Shoot()
{
    if (remainingBullets > 0 && Time.timeScale > 0)
    {
        remainingBullets--;

        GameObject clone = Instantiate(projectilePrefab);

        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;

        muzzleEffect.Play();
        shootSound.Play();
    }
}
}
