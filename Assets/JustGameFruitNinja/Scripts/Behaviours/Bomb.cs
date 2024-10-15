
using System.Collections;
using UnityEngine;


public class Bomb : MonoBehaviour, IPooledObject
{
    [SerializeField] private float minForce;
    public float maxForce;
    [SerializeField] private ParticleSystem Explosion;
    [SerializeField] private float rotateForce;

    private Rigidbody rb;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!Refrences.Instance.gameManager.GameIsPlaying)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    /// <summary>
    /// A trigger to check if the bomb should explode or deactivate
    /// </summary>
    /// <param name="other">The collider of the triggered gameobject</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blade") && Refrences.Instance.gameManager.GameIsPlaying)
        {
            AudioManager.Instance.Play(AudioData.EAudio.BombBlast, 1, false, AudioManager.AudioType.Sound);
            Explosion.Play(true);
            StartCoroutine(OnBombExplotion());
        }

        if (other.gameObject.CompareTag("Deactivator"))
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// A method that is called to put the game in the GameOver state.
    /// </summary>
    /// <returns></returns>
    IEnumerator OnBombExplotion()
    {
        yield return new WaitForSeconds(0.8f);
        Refrences.Instance.uiManager.GameOver();
    }

    /// <summary>
    /// An interface method inherited for objectpooling to be called every time the gameobject is being activated
    /// </summary>
    public void OnObjectSpawn()
    {
        float force = Random.Range(minForce, maxForce);
        Vector3 upForce = new Vector3(0, force, 0);
        rb.velocity = transform.up.y * upForce;
        rb.AddTorque(Vector3.forward * (Random.Range(0, 2) < 1 ? -rotateForce : rotateForce), ForceMode.Acceleration);
    }
}
