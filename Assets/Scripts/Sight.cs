// Sight.cs
// Maintainer: Qingzhe-Song
// Summary: Finds the first collider within range and viewing angle that has an unobstructed line of sight.

using UnityEngine;
using UnityEngine.Serialization;

public class Sight : MonoBehaviour
{
    [Header("Detection Range")]
    [InspectorName("Detection Distance")]
    [Tooltip("Radius in world units used to search for candidate colliders.")]
    [FormerlySerializedAs("distance")]
    public float detectionDistance;
    [InspectorName("View Half Angle (Degrees)")]
    [Tooltip("Maximum angle from forward on either side; the full viewing cone is twice this value.")]
    [FormerlySerializedAs("angle")]
    public float viewHalfAngle;

    [Header("Detection Layers")]
    [Tooltip("Layers containing colliders that this sensor can detect.")]
    [FormerlySerializedAs("objectsLayers")]
    public LayerMask detectableLayers;
    [Tooltip("Layers that block the line from the sensor to a candidate collider's bounds center.")]
    [FormerlySerializedAs("obstaclesLayers")]
    public LayerMask obstacleLayers;

    [Header("Detection Result")]
    [Tooltip("First visible candidate this frame, or null when no candidate passes the checks.")]
    public Collider detectedObject;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, detectionDistance, detectableLayers
        );

        // Clear the previous result so enemies can react when the player leaves sight.
        detectedObject = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);

            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < viewHalfAngle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider;
                    // Use the first visible overlap result; candidates are not sorted by distance.
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        Vector3 rightDirection = Quaternion.Euler(0, viewHalfAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * detectionDistance);

        Vector3 leftDirection = Quaternion.Euler(0, -viewHalfAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * detectionDistance);
    }
}
