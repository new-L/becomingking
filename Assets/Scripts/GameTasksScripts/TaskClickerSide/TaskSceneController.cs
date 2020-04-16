using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSceneController : MonoBehaviour
{

    [SerializeField] private GameObject loadPanel, gamePanel,resultPanel, damagePanel,resourceGO, clickHPPanel, loadAnimator, bonusPanel;
    [SerializeField] private SpriteRenderer resourceI;
    [SerializeField] private Transform resourceITransform;
    [SerializeField] private Image backgroundI, clickHPI, damageI;
    [SerializeField] private Text clickHPT, damageT;
    [SerializeField] private GameObject[] monstersPrefab;
    [SerializeField] private Animator monsterAnimator;
    [SerializeField] private GameObject monster;
    [SerializeField] private ServerController sController;
    /**Элементы UI в результатах таска**/
    public Image main_resourceI, diamondI, expI, ratingI, bonusI;
    public Text main_resourceT, diamondT, expT, ratingT, bonusTitleT, bonusCountT;
    /****/
    [SerializeField] private NumberConversion numberConversion;

    public GameObject[] MonstersPrefab { get => monstersPrefab; set => monstersPrefab = value; }
    public GameObject Monster { get => monster; set => monster = value; }
    public Animator MonsterAnimator { get => monsterAnimator; set => monsterAnimator = value; }
    public Text ClickHPT { get => clickHPT; set => clickHPT = value; }
    public GameObject LoadAnimator { get => loadAnimator; set => loadAnimator = value; }

    private void Start()
    {
        ShowPanels(false, true, false);
        SetUI();
    }

    private void ShowPanels(bool isLoadPanel, bool isGamePanel, bool isResultPanel)
    {
        loadPanel.SetActive(isLoadPanel);
        gamePanel.SetActive(isGamePanel);
        resultPanel.SetActive(isResultPanel);
    }

    private void SetUI()
    {
        if (TaskData.TypeTask == "worker")
        {
            resourceGO.SetActive(true);
            //Устанока background'а
            backgroundI.sprite = Resources.Load<Sprite>("task_scene/background/task_scene_" + TaskData.TypeResource + "_background_winter");
            //Установка основных картинок UI
            clickHPI.sprite = Resources.Load<Sprite>("task_scene/ui/task_scene_energy_icon");
            damageI.color = new Color32(255, 255, 255, 0);
            //Убираем текст с количеством урона
            damagePanel.SetActive(false);
            //Устанавливаем количество кликов
            ClickHPT.text = TaskData.ClickCount.ToString();
            //Устанавливаем картинку в зависимости от типа ресурса
            Sprite resourceSprite;
            resourceSprite = Resources.Load<Sprite>("task_scene/objects/task_scene_" + TaskData.TypeResource + "_style_01");
            resourceI.sprite = resourceSprite;
            //Устанавливаем картинку по предполагаемому ресурсу
            SetResourceSprite();
        }
        else
        {
            //Устанока background'а
            backgroundI.sprite = Resources.Load<Sprite>("task_scene/background/task_scene_" + TaskData.TypeTask + "_background_winter");
            //Установка основных картинок UI
            clickHPI.sprite = Resources.Load<Sprite>("task_scene/ui/task_scene_health_icon");
            damageI.color = new Color32(255, 255, 255, 255);
            //Делаем активным текст с количеством урона
            damagePanel.SetActive(true);
            //Устанавливаем ХП монстра
            ClickHPT.text = TaskData.MonstersHP.ToString();
            //Рандомный монстр
            SetMonsterSprite();
        }

    }

    private void SetResourceSprite()
    {
        switch (TaskData.TypeResource)
        {
            case "wood": resourceITransform.position = new Vector3(0, 0, 0); break;
            case "rock": resourceITransform.position = new Vector3(0, -2.3f, 0); break;
            case "limestone": resourceITransform.position = new Vector3(0, -2.3f, 0); break;
            case "wheat": resourceITransform.position = new Vector3(0, -1.65f, 0); break;
        }

    }
    private void SetMonsterSprite()
    {
        int random = UnityEngine.Random.Range(0, monstersPrefab.Length);
        monster = Instantiate(monstersPrefab[random], new Vector3(-.07f, -2.77f, 0), Quaternion.identity);
        monster.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
        monster.AddComponent<MonsterControll>();
        resourceGO.SetActive(false);
        monsterAnimator = monster.GetComponent<Animator>();
        monsterAnimator.SetInteger("state", 1);
        monsterAnimator.speed = 0.75f;
    }

    public void SetResultUIData()
    {
        
        resourceGO.SetActive(false);
        Destroy(monster);
        ShowPanels(false, true, true);
        //Делаем основные компоненты UI невидимыми
        clickHPI.color = new Color32(255, 255, 255, 0);
        damageI.color = new Color32(255, 255, 255, 0);
        clickHPPanel.SetActive(false);
        damagePanel.SetActive(false);
        //Устанавливаем данные по заданию в UI-элементы
        if (TaskData.TypeTask.Equals("worker")) { SetImage(main_resourceI, "resources_icon/" + TaskData.TypeResource);  }
        else { SetImage(main_resourceI, "goldIcon"); SetImage(bonusI, "goldIcon"); }
        SetImage(diamondI, "diamondIcon");
        SetImage(expI, "task_scene/objects/task_scene_exp_icon");
        SetImage(ratingI, "task_scene/objects/task_scene_rating_icon");
        SetText(main_resourceT, TaskData.ResourceReward);
        SetText(diamondT, TaskData.DiamondCount);
        SetText(expT, TaskData.Exptotask);
        SetText(ratingT, TaskData.Rating);
        if (TaskData.Bonus)
        {
            bonusPanel.SetActive(true);
            if (TaskData.TypeTask.Equals("worker")) SetImage(bonusI, "resources_icon/" + TaskData.TypeResource);
            else SetImage(bonusI, "goldIcon");
            double flag = Math.Round((TaskData.ResourceReward * TaskData.BonusPrecent)/100);
            bonusTitleT.text = TaskData.BonusBuilding;
            print(flag);
            SetText(bonusCountT, Convert.ToInt32(flag));
            TaskData.ResourceReward += Convert.ToInt32(flag);
        }
        else
        {
            bonusPanel.SetActive(false);
        }
        StartCoroutine(StartLoad());
    }
    private void SetImage(Image image, string path)
    {
        image.sprite = Resources.Load<Sprite>(path);
    }
    private void SetText(Text text, int field)
    {
        text.text = "+" + numberConversion.NumberConverter(Convert.ToInt64(field));
    }


    private IEnumerator StartLoad()
    {
        //Начинаем анимацию загрузки
        loadAnimator.SetActive(true);
        yield return new WaitForSeconds(.8f);
        StartCoroutine(sController.SaveDateWorker());
    }
    
    
}
