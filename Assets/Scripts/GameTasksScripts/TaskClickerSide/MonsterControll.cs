using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    [SerializeField] private TaskSceneController taskScene;
    [SerializeField] private bool isDying;
    private void Start()
    {
        taskScene = GameObject.FindGameObjectWithTag("taskcontroller").GetComponent<TaskSceneController>();
    }
    void OnMouseDown()
    {
        //Для расчета рандома;
        int random = Random.Range(1, 100);
        taskScene.MonsterAnimator.SetInteger("state", 2);
        if (TaskData.MonstersHP <= 100)
        {
            TaskData.MonstersHP = 0;
            taskScene.MonsterAnimator.SetInteger("state", 3);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            isDying = false;
            StartCoroutine(Result());
        }
        else
        {
            TaskData.MonstersHP -= 100;
            isDying = true;
        }
        taskScene.ClickHPT.text = TaskData.MonstersHP.ToString();
        if (random <= TaskData.DiamondChance)
        {
            TaskData.DiamondCount += 1;
        }
        
    }
    void OnMouseUp()
    {
        if(isDying) taskScene.MonsterAnimator.SetInteger("state", 1);
        else if(!isDying) taskScene.MonsterAnimator.SetInteger("state", 3);
    }
    //Отправляем извещение об окончании задания с задержкой
    private IEnumerator Result()
    {
        yield return new WaitForSeconds(1.5f);
        taskScene.SetResultUIData();
    }
}
