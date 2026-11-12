// PlayerShooting.cs
// Maintainer: Qingzhe-Song
// Summary: Repeats shots while Fire is held, consuming ammunition and driving weapon animation and effects.

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    private const string ShootingParameterName = "Shooting";

    [Header("Projectile")]
    [InspectorName("Projectile Prefab")]
    [Tooltip("Projectile prefab instantiated by repeated shots while the Fire input is held.")]
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
    [Tooltip("Remaining bullets; each successful shot consumes one bullet.")]
    [FormerlySerializedAs("bulletsAmount")]
    public int remainingBullets = 100;

    [Header("Shot Timing")]
    [InspectorName("Shot Interval (Seconds)")]
    [Tooltip("Delay before the first shot and between repeated shots while Fire is held.")]
    [FormerlySerializedAs("fireRate")]
    public float shotInterval = 0.2f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnFire(InputValue value)
    {
        // PlayerInput uses this callback name for both the press and release of the Fire action.
        animator.SetBool(ShootingParameterName, value.isPressed);

        if (value.isPressed)
        {
            InvokeRepeating(nameof(Shoot), shotInterval, shotInterval);
        }
        else
        {
            CancelInvoke(nameof(Shoot));
        }
    }

    private void Shoot()
    {
        // Input callbacks still arrive while paused, so each scheduled shot checks the time scale.
        if (remainingBullets > 0 && Time.timeScale > 0f)
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
