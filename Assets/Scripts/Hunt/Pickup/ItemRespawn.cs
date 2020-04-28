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
    [SerializeField] private List<GameObject> m_keyPrefab;


    [SerializeField] public Dictionary<GameObject, bool> respawnPoints = new Dictionary<GameObject, bool>();
    [SerializeField] public Dictionary<GameObject, bool> respawnCurrencyPoints = new Dictionary<GameObject, bool>();
    [SerializeField] public Dictionary<GameObject, bool> respawnKeyPoints = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject[] respawnPoint;
    [SerializeField] private GameObject[] respawnCurrencyPoint;
    [SerializeField] private GameObject[] respawnKeyPoint;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject diamondPrefab;

    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject energyPrefab;

    [SerializeField] private GameObject goldKeyPrefab;
    [SerializeField] private GameObject silverKeyPrefab;


    private void Start()
    {
        respawnPoint = GameObject.FindGameObjectsWithTag("StatsItemRespawn");
        respawnCurrencyPoint = GameObject.FindGameObjectsWithTag("CurrencyItemPickup");
        respawnKeyPoint = GameObject.FindGameObjectsWithTag("KeyItemPickup");
        foreach(var item in respawnKeyPoint)
            respawnKeyPoints.Add(item, false);
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
        CreateItem(goldKeyPrefab, m_keyPrefab);
        CreateItem(silverKeyPrefab, m_keyPrefab);
        SpawnKeys();
        Spawn(respawnPoints, m_statsPrefab);
        Spawn(respawnCurrencyPoints, m_currencyPrefab);
    }


    private void SpawnKeys()
    {
        int random = Random.Range(0, respawnKeyPoint.Length);

        m_keyPrefab.ElementAt(0).transform.SetParent(respawnKeyPoints.ElementAt(random).Key.transform); 
        m_keyPrefab.ElementAt(0).SetActive(true);
        m_keyPrefab.ElementAt(0).transform.position = respawnKeyPoints.ElementAt(random).Key.transform.position;
        respawnKeyPoints[respawnKeyPoints.ElementAt(random).Key] = true;
        random = Random.Range(0, respawnKeyPoint.Length);
        foreach(var item in respawnKeyPoints.ToList()){
            if (!respawnKeyPoints.ElementAt(random).Value)
            {
                m_keyPrefab.ElementAt(1).transform.SetParent(respawnKeyPoints.ElementAt(random).Key.transform);
                m_keyPrefab.ElementAt(1).SetActive(true);
                m_keyPrefab.ElementAt(1).transform.position = respawnKeyPoints.ElementAt(random).Key.transform.position;
                respawnKeyPoints[respawnKeyPoints.ElementAt(random).Key] = true;
            }
            else
            {
                random = Random.Range(0, respawnKeyPoint.Length);
            }
        }
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
