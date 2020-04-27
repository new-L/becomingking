using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public int respawnItemSize;

    [SerializeField] private List<GameObject> m_statsPrefab;


    [SerializeField] private Dictionary<GameObject, bool> respawnPoints = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject[] respawnPoint;


    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject energyPrefab;
    private void Start()
    {
        respawnPoint = GameObject.FindGameObjectsWithTag("StatsItemRespawn");
        foreach (var item in respawnPoint)
            respawnPoints.Add(item, false);
        for (int i = 0; i < respawnItemSize; i++)
        {
            CreateItem(healthPrefab, m_statsPrefab);
            CreateItem(energyPrefab, m_statsPrefab);
        }
        Spawn();
    }




    private void Spawn()
    {
        foreach (var item in respawnPoints.ToList())
        {
            if (!item.Value) SpawnSetItems(item.Key, m_statsPrefab);
        }
        
    }

    private void SpawnSetItems(GameObject _Parent, List<GameObject> prefabList)
    {
        foreach (var item in prefabList)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.SetParent(_Parent.transform); item.SetActive(true);
                item.transform.position = _Parent.transform.position;
                respawnPoints[_Parent] = true;
                break;
            }

        }
    }
    //Создаем пул объектов
    private void CreateItem(GameObject prefab, List<GameObject> prefabList)
    {
        GameObject item = Instantiate(prefab);
        item.SetActive(false);
        prefabList.Add(item);
    }


    public void InActiveItem(GameObject pickupItem)
    {
        foreach (var item in respawnPoints.ToList()) if (pickupItem == item.Key) item.Key.SetActive(false);
    }
    
}
