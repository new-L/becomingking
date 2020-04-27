using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Нужен рефакторинг всего проекта. Мне очень плохо. Поэтому даже сил нет что-то выдумать
public class ItemRespawn : MonoBehaviour
{
    public int respawnItemSize;

    [SerializeField] private List<GameObject> m_statsPrefab;
    [SerializeField] private List<GameObject> m_currencyPrefab;


    [SerializeField] public Dictionary<GameObject, bool> respawnPoints = new Dictionary<GameObject, bool>();
    [SerializeField] public Dictionary<GameObject, bool> respawnCurrencyPoints = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject[] respawnPoint;
    [SerializeField] private GameObject[] respawnCurrencyPoint;


    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject diamondPrefab;

    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject energyPrefab;


    private void Start()
    {
        respawnPoint = GameObject.FindGameObjectsWithTag("StatsItemRespawn");
        respawnCurrencyPoint = GameObject.FindGameObjectsWithTag("CurrencyItemPickup");
        foreach (var item in respawnPoint)
            respawnPoints.Add(item, false);
        foreach (var item in respawnCurrencyPoint)
            respawnCurrencyPoints.Add(item, false);
        for (int i = 0; i < respawnItemSize; i++)
        {
            CreateItem(healthPrefab, m_statsPrefab);
            CreateItem(energyPrefab, m_statsPrefab);
            CreateItem(coinPrefab, m_currencyPrefab);
            CreateItem(coinPrefab, m_currencyPrefab);
        }
        Spawn(respawnPoints, m_statsPrefab);
        Spawn(respawnCurrencyPoints, m_currencyPrefab);
    }




    private void Spawn(Dictionary<GameObject, bool> spawnList, List<GameObject> prefabList)
    {
        foreach (var item in spawnList.ToList())
        {
            if (!item.Value) SpawnSetItems(item.Key, prefabList, spawnList);
        }
        
    }

    private void SpawnSetItems(GameObject _Parent, List<GameObject> prefabList, Dictionary<GameObject, bool> spawnList)
    {
        foreach (var item in prefabList)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.SetParent(_Parent.transform); item.SetActive(true);
                item.transform.position = _Parent.transform.position;
                spawnList[_Parent] = true;
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


    public void InActiveItem(GameObject pickupItem, Dictionary<GameObject, bool> respawns)
    {
        foreach (var item in respawns.ToList()) if (pickupItem == item.Key) item.Key.SetActive(false);
    }
    
}
