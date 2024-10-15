
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    Dictionary<int, GameObject> touchContainer = new Dictionary<int, GameObject>();

    private void Awake()
    {
        Input.multiTouchEnabled = true;
    }

    private void Update()
    {
        if (Refrences.Instance.gameManager.GameIsPlaying)
            TouchInput();
    }

    /// <summary>
    /// A method that check for multiple touch input and calls function according to the phases as required
    /// </summary>
    private void TouchInput()
    {
        if (Input.touchCount < 6)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    touchContainer.Add(t.fingerId, createBlade());
                    if (touchContainer.ContainsKey(t.fingerId))
                    {
                        touchContainer[t.fingerId].GetComponent<Blade>().StartSliceMobile(t.position);
                    }
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    if (touchContainer.ContainsKey(t.fingerId))
                    {
                        touchContainer[t.fingerId].GetComponent<Blade>().StopSliceMobile();
                        touchContainer[t.fingerId].SetActive(false);
                        touchContainer.Remove(t.fingerId);
                    }
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    if (touchContainer.ContainsKey(t.fingerId))
                    {
                        touchContainer[t.fingerId].GetComponent<Blade>().ContinueSliceMobile(t.position);
                    }
                }
            }
        }
    }

    /// <summary>
    /// A method that activated the Blade prefab whenever a touch is started on the screen
    /// </summary>
    /// <returns></returns>
    private GameObject createBlade()
    {
        GameObject currentBlade = Refrences.Instance.objectPooler.SpawnFromPool("Blade", Vector3.zero, Quaternion.identity);
        return currentBlade;
    }
}
