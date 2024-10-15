
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// A serialized Class to contain the required variables for the Object Pool
/// </summary>
[System.Serializable]
public class ObjectPoolItem
{
    public string objectTag;
    public GameObject Object;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolItem> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private List<GameObject> fruitsList;


    /// <summary>
    /// Instantiation of pools gameObjects and storing it in the pool Dictionary
    /// </summary>
    private void Start()
    {
        fruitsList = new List<GameObject>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPoolItem pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.Object);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(this.transform);
            }
            poolDictionary.Add(pool.objectTag, objectPool);
        }
    }

    /// <summary>
    /// A method to activate the gameobjects in the pool dictionary as when required and calling of the interface so that it behaves the same whenever it is activated
    /// </summary>
    /// <param name="tag">A tag to identify the object that is required</param>
    /// <param name="position">A postion to modify the gameobjects transform</param>
    /// <param name="rotation">A rotation to modify the gameobjects transform</param>
    /// <returns>It returns to activated GameObject of the pool</returns>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();
        ObjectToSpawn.SetActive(true);
        ObjectToSpawn.transform.position = position;
        ObjectToSpawn.transform.rotation = rotation;


        IPooledObject[] pooledObjs = ObjectToSpawn.GetComponents<IPooledObject>();


        foreach (IPooledObject pooledObj in pooledObjs)
        {
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
        }
        poolDictionary[tag].Enqueue(ObjectToSpawn);
        return ObjectToSpawn;
    }

    /// <summary>
    /// A method that returns a list of fruits
    /// </summary>
    /// <returns></returns>
    public List<GameObject> CheckFruits()
    {
        foreach (KeyValuePair<string, Queue<GameObject>> entry in poolDictionary)
        {
            if (entry.Key == "Blade" || entry.Key == GameConstants.PopUPTag)
            {
                continue;
            }
            else
            {
                Queue<GameObject> fruits = entry.Value;
                foreach (GameObject fruit in fruits)
                {
                    fruitsList.Add(fruit);
                }
            }
        }
        return fruitsList;
    }
}
