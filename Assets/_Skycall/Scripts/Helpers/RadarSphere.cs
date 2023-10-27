using UnityEngine;

namespace _Skycall.Scripts.Helpers
{
    public class RadarSphere : MonoBehaviour
    {
        [SerializeField] private Transform sphere;

        public Transform FoundObject(string tag)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphere.lossyScale.x);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag(tag))
                {
                    return hitCollider.transform;
                }
            }

            return null;
        }
    }
}