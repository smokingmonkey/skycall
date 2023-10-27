using System.Collections;
using UnityEngine;

namespace _Skycall.Scripts.Scenes
{
    public class LoadNextScene : MonoBehaviour
    {
        public float delayBeforeLoad = 5.0f; // Set the delay time in seconds

        private bool isLoading = false;

        [SerializeField] private GameObject objescForhide;

        void Update()
        {
            if (!isLoading && Input.anyKeyDown)
            {
                isLoading = true;
                StartCoroutine(LoadNextSceneWithDelay());
            }
        }

        IEnumerator LoadNextSceneWithDelay()
        {
            objescForhide.SetActive(false);
            yield return new WaitForSeconds(delayBeforeLoad);

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}