using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Skycall.Scripts.Scenes
{
    public class SlideShow : MonoBehaviour
    {
        public List<GameObject> objects;
        public float coolDownTime = 6.0f;
        private int currentObject = 0;

        void Start()
        {
            Init(currentObject);
            StartCoroutine(ChangeObject());
        }

        void Init(int index)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(i == index);
            }
        }

        IEnumerator ChangeObject()
        {
            while (currentObject < objects.Count)
            {
                yield return new WaitForSeconds(coolDownTime);
                if (currentObject == objects.Count -1)
                {
                    yield break;
                }
                objects[currentObject].SetActive(false);

                currentObject = (currentObject + 1);

                if (currentObject < objects.Count)
                {
                    Init(currentObject);
                }
            }
        }
    }
}