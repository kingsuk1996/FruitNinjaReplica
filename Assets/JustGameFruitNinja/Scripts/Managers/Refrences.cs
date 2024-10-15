
using UnityEngine;



/// <summary>
/// All refrences of scripts that needs to be singleton
/// </summary>
public class Refrences : MonoBehaviour
{
    public GameManager gameManager;
    public ObjectPooler objectPooler;
    public SpawnManager spawnManager;
    public ComboManager comboManager;
    public UIManager uiManager;

    public static Refrences Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
