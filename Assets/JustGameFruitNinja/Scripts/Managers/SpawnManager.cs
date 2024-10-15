
using System.Collections;
using TMPro;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private TMP_Text TimerText;

    [SerializeField] private string[] listOFTags;

    private Collider spawnArea;

    private BoxCollider myCollider;

    [Header("Values")]
    [Range(0f, 1f)][SerializeField] private float bombChance;

    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    [SerializeField] private int minPerSpawn;
    [SerializeField] private int maxPerSpawn;

    private void Awake()
    {
        TimerText.gameObject.SetActive(true);
        spawnArea = GetComponent<Collider>();
        myCollider = this.GetComponent<BoxCollider>();

        myCollider.size = new Vector3(Mathf.Abs((Screen.width * 3.5f) / 1080), myCollider.size.y, myCollider.size.z);
    }

    /// <summary>
    /// A method to Start the game when the player gives presses the play button
    /// </summary>
    public void StartGameplay()
    {
        StartCoroutine(StartGame());
    }

    /// <summary>
    /// A method that resumes the gameplay when paused
    /// </summary>
    public void ResumeSpawn()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// A countdown of 3 seconds before the gameplay starts
    /// </summary>
    /// <returns> a gap of 1 seconds to calculte countdown</returns>
    private IEnumerator StartGame()
    {
        int timer = 4;
        while (timer > 0)
        {
            if (timer == 1)
            {
                TimerText.text = "Let's Start!";
            }
            else
            {
                TimerText.text = (timer - 1).ToString();
            }

            Animator animatorContdown = TimerText.GetComponent<Animator>();
            if (animatorContdown != null)
            {
                animatorContdown.SetTrigger("Countdown");
            }

            yield return new WaitForSeconds(1f);

            timer -= 1;
        }
        TimerText.gameObject.SetActive(false);
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// A method to spawn the fruits and bombs in the game scene
    /// </summary>
    /// <returns> secodns delay for spawning in waves </returns>
    private IEnumerator Spawn()
    {
        StartCoroutine(IncreaseOverTime());
        // timerManager.StartTime();
        while (true)
        {
            if (Refrences.Instance.gameManager.GameIsPlaying)
            {
                int numOFItemsInWave = Random.Range(minPerSpawn, maxPerSpawn);

                for (int i = 0; i < numOFItemsInWave; i++)
                {
                    Vector3 position = new Vector3();
                    position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
                    position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
                    position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

                    float rotationZ = Random.Range(minAngle, maxAngle);
                    rotationZ = (position.x < 0) ? -rotationZ : rotationZ;

                    Quaternion rotation = Quaternion.Euler(0f, 0f, rotationZ);

                    string tag = "";

                    if (Random.value < bombChance)
                    {
                        tag = GameConstants.BombTag;
                    }
                    else
                    {
                        tag = listOFTags[Random.Range(0, listOFTags.Length)];
                    }

                    if (!string.IsNullOrEmpty(tag))
                    {
                        GameObject fruit = Refrences.Instance.objectPooler.SpawnFromPool(tag, position, rotation);
                    }
                    else
                    {
                        yield return null;
                    }

                    yield return new WaitForSeconds(.3f);
                }

                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// A method that handles increasing of spawnrate of Fruits and Bombs
    /// </summary>
    /// <returns></returns>
    private IEnumerator IncreaseOverTime()
    {
        yield return new WaitForSeconds(8);
        maxPerSpawn++;
        bombChance += 0.01f;
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (maxPerSpawn <= 10 && bombChance <= 0.3f)
            {
                maxPerSpawn++;
                bombChance += 0.05f;
            }
            else
            {
                break;
            }
        }
    }
}
