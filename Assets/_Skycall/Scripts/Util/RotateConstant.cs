using UnityEngine;

namespace _Skycall.Scripts.Util
{
    public class RotateConstant : MonoBehaviour
    {
        public float xSpeed;
        public float ySpeed;
        public float zSpeed;

        private float _multiplier = 1f;

        public Vector3 speed = new Vector3(0, 0, 0);

        void Update()
        {
            speed = new Vector3(xSpeed * _multiplier, ySpeed * _multiplier, zSpeed * _multiplier);
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}