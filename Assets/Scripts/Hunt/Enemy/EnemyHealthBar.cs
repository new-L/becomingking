using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Enemy m_Enemy;              //Parent Enemy script

    private Transform healthBar;        //Health Bar Transform component

    private float healthPrecent;        //Precent for Health Bar

    private GameObject healthBarActive;


    private void Start()
    {
        m_Enemy = GetComponent<Enemy>();
        healthBar = gameObject.transform.Find("HealthBar").transform.Find("HealthBarSprite");           //I LIKE THE SAUSAGES(ДЛИННЫЕ ПРИ ЭТОМ) IN UNITY C#
        healthBarActive = gameObject.transform.Find("HealthBar").gameObject;
        healthBarActive.SetActive(false);
    }

    private void HealthBarPrecent()
    {
        healthPrecent = (float)m_Enemy.CurrentHealth / m_Enemy.MaxHeath;
    }

    public void SetHealthBar()
    {
        if (!healthBarActive.activeSelf) healthBarActive.SetActive(true);
        HealthBarPrecent();
        if (healthPrecent > 0) healthBar.localScale = new Vector2(healthPrecent, healthBar.localScale.y);
        else healthBar.localScale = new Vector2(0, healthBar.localScale.y);
    }
}
