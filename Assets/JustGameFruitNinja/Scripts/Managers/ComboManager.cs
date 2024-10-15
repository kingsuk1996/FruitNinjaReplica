
using System.Collections;
using TMPro;
using UnityEngine;


public class ComboManager : MonoBehaviour
{
    bool timerStart = false;

    private int comboCounter = 0;
    [SerializeField] private float timer = 1f;

    /// <summary>
    /// Initially fruit count 0.
    /// </summary>
    private void Start()
    {
        comboCounter = 0;
    }

    /// <summary>
    /// When we cut the fruit timer will be decreased everytime.
    /// if the counter greater equal to 3 that will be combo.
    /// </summary>
    private void Update()
    {
        if (timerStart)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timerStart = false;
            timer = 1f;

            if (comboCounter >= 3)
            {
                AudioManager.Instance.Play(AudioData.EAudio.ComboSound, 1, false, AudioManager.AudioType.Sound);
                StartCoroutine(TextPopUp());
            }
            comboCounter = 0;
        }
    }

    /// <summary>
    /// When timer is greater than 0 timer will be start and counter will be increased.
    /// </summary>
    public void ComboCheck()
    {
        if (timer > 0f)
        {
            timerStart = true;
            comboCounter++;
        }
    }

    /// <summary>
    /// Show Combo PopUp
    /// </summary>
    /// <returns></returns>
    IEnumerator TextPopUp()
    {
        int comboPoints;
        GameObject textPopUp = Refrences.Instance.objectPooler.SpawnFromPool(GameConstants.PopUPTag, transform.position, Quaternion.identity);
        if (comboCounter == 3)
        {
            comboPoints = 50;
            textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = comboCounter.ToString() + " Fruit Combo \n +" + comboPoints;
            Animator textAnimator = textPopUp.GetComponentInChildren<Animator>();
            textAnimator.SetTrigger("TextUp");
            Refrences.Instance.uiManager.Score(comboPoints);
        }

        if (comboCounter == 4)
        {
            comboPoints = 70;
            textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = comboCounter.ToString() + " Fruit Combo \n +" + comboPoints;
            Animator textAnimator = textPopUp.GetComponentInChildren<Animator>();
            textAnimator.SetTrigger("TextUp");
            Refrences.Instance.uiManager.Score(comboPoints);
        }

        if (comboCounter == 5)
        {
            comboPoints = 100;
            textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = comboCounter.ToString() + " Fruit Combo \n +" + comboPoints;
            Animator textAnimator = textPopUp.GetComponentInChildren<Animator>();
            textAnimator.SetTrigger("TextUp");
            Refrences.Instance.uiManager.Score(comboPoints);
        }

        if (comboCounter == 6)
        {
            comboPoints = 150;
            textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = comboCounter.ToString() + " Fruit Combo \n +" + comboPoints;
            Animator textAnimator = textPopUp.GetComponentInChildren<Animator>();
            textAnimator.SetTrigger("TextUp");
            Refrences.Instance.uiManager.Score(comboPoints);
        }

        yield return new WaitForSeconds(1);
        textPopUp.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        textPopUp.SetActive(false);
    }
}
