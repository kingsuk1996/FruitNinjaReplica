
using UnityEngine;


public class Fruit : MonoBehaviour, IPooledObject
{
    [SerializeField] private float minForce;
    [SerializeField] public float maxForce;
    private bool checkForCombo;

    [SerializeField] private float rotateForce;

    [SerializeField] private GameObject unSlicedObject;

    [SerializeField] private ParticleSystem juiceParticle;

    private Rigidbody rb;
    private void Awake()
    {
        checkForCombo = true;
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
    /// A trigger is being taken to check for combo points and disabling the fruit object
    /// </summary>
    /// <param name="other">The collider of the triggered gameobject</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blade") && Refrences.Instance.gameManager.GameIsPlaying)
        {
            if (checkForCombo)
            {
                checkForCombo = false;
                if (juiceParticle != null)
                {
                    juiceParticle.Play();
                }
                UIManager.ScoreCount?.Invoke(10);
                Refrences.Instance.comboManager.ComboCheck();
            }
        }

        if (other.gameObject.CompareTag("Deactivator"))
        {
            checkForCombo = true;

            if (unSlicedObject.activeInHierarchy)
                Refrences.Instance.uiManager.LifeLost();

            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// An interface method inherited for objectpooling to be called every time the gameobject is being activated
    /// </summary>
    public void OnObjectSpawn()
    {
        checkForCombo = true;
        float force = Random.Range(minForce, maxForce);
        Vector3 upForce = new Vector3(0, force, 0);
        rb.velocity = transform.up.y * upForce;
        rb.AddTorque(Vector3.forward * (Random.Range(0, 3) < 1 ? -rotateForce : rotateForce), ForceMode.Acceleration);
    }
}
