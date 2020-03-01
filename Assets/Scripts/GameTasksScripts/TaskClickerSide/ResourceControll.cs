using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceControll : MonoBehaviour
{
    [SerializeField] private TaskSceneController taskScene;
    private void Start()
    {
        taskScene = GameObject.FindGameObjectWithTag("taskcontroller").GetComponent<TaskSceneController>();
    }
    void OnMouseDown()
    {
        //Для расчета рандома;
        int random = Random.Range(1, 100);
        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        if (TaskData.ClickCount <= 1)
        {
            TaskData.ClickCount = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            StartCoroutine(Result());
        }
        else
        {
            TaskData.ClickCount -= 1;
        }
        taskScene.ClickHPT.text = TaskData.ClickCount.ToString();
        if (random <= TaskData.DiamondChance)
        {
            TaskData.DiamondCount += 1;
        }
        
    }
    void OnMouseUp()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    //Отправляем извещение об окончании задания с задержкой
    private IEnumerator Result()
    {
        yield return new WaitForSeconds(.5f);
        taskScene.SetResultUIData();
    }
}
