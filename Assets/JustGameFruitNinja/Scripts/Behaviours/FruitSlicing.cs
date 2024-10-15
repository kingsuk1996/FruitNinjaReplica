

using UnityEngine;


public class FruitSlicing : MonoBehaviour, IPooledObject
{
    [SerializeField] private Rigidbody topSliced;
    [SerializeField] private Rigidbody bottomSliced;
    [SerializeField] private GameObject unSliced;
    [SerializeField] private float minRotate;
    [SerializeField] private float maxRotate;
    [SerializeField] private float cutForce;
    [SerializeField] private Color startColorForParticle;
    [SerializeField] private Material particleMaterial;
    private bool triggerOnlyOnce;

    /// <summary>
    /// In this method we are just doing gameobject enable and disable 
    /// set their position according to unsliced gameobject's position
    /// </summary>
    public void OnObjectSpawn()
    {
        unSliced.SetActive(true);

        topSliced.gameObject.SetActive(false);
        topSliced.transform.position = this.transform.position;
        topSliced.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        bottomSliced.gameObject.SetActive(false);
        bottomSliced.transform.position = this.transform.position;
        bottomSliced.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        triggerOnlyOnce = true;
    }

    /// <summary>
    /// When fruit collision with blade, SliceFruit method willbe called everytime.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blade") && Refrences.Instance.gameManager.GameIsPlaying)
        {
            if (triggerOnlyOnce)
            {
                triggerOnlyOnce = false;
                AudioManager.Instance.Play(AudioData.EAudio.FruitCutting, 1, false, AudioManager.AudioType.Sound);
                SLiceFruit(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Unsliced Gameobject disable and sliced gameobject enabled.
    /// Give velocity to separate from each other.
    /// </summary>
    private void SLiceFruit(GameObject blade)
    {
        Blade bladeSc = blade.GetComponent<Blade>();
        PlayParticleOnSlice();

        unSliced.SetActive(false);
        topSliced.gameObject.SetActive(true);
        bottomSliced.gameObject.SetActive(true);

        topSliced.velocity = bladeSc.force + cutForce * Vector3.left;
        topSliced.AddTorque(Vector3.up * minRotate, ForceMode.Impulse);
        topSliced.AddTorque(Vector3.left * maxRotate, ForceMode.Impulse);

        bottomSliced.velocity = -bladeSc.force + cutForce * Vector3.right;
        bottomSliced.AddTorque(Vector3.up * maxRotate, ForceMode.Impulse);
        bottomSliced.AddTorque(Vector3.left * minRotate, ForceMode.Impulse);
    }

    /// <summary>
    /// This is used to play Fruit Particle after slice.
    /// </summary>
    private void PlayParticleOnSlice()
    {
        Vector3 currentPos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        ParticleSystem fruitparticle = Refrences.Instance.objectPooler.SpawnFromPool(GameConstants.fruitParticleTag, currentPos, Quaternion.identity).GetComponent<ParticleSystem>();
        fruitparticle.gameObject.transform.position = currentPos;
        ParticleSystem fruitParticleChild = fruitparticle.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        var mainChild = fruitParticleChild.GetComponent<ParticleSystemRenderer>();
        mainChild.material = particleMaterial;
        fruitparticle.Play(true);
    }
}
