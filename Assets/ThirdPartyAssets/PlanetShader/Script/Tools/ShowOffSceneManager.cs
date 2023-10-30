using UnityEngine;

namespace ThirdPartyAssets.PlanetShader.Script.Tools
{
    public class ShowOffSceneManager : MonoBehaviour
    {
        public float planetRotSpeed = 1.0f;
        public float sunRotSpeed = 1.0f;
        public Transform sun;

        [SerializeField] private GameObject planet;


        void Update()
        {
            planet.transform.Rotate(new Vector3(0, planetRotSpeed * Time.deltaTime, 0));
            sun.Rotate(new Vector3(0, sunRotSpeed * Time.deltaTime, 0));
        }

    
    }
}