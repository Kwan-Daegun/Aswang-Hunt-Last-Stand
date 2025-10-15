using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public string type; 
    public GameObject BarGO;
    public Image BarMask;
    public float currentBarValue;
    float maxBarValue = 100;

    void Start()
    {
        BarGO.SetActive(true);
    }

    public void addHealth(float number)
    {
        if (number <= 0) return;
        currentBarValue = Mathf.Clamp(currentBarValue + number, 0, maxBarValue);
        /*if (currentBarValue >= maxBarValue)
        {
            //off the player's collider
        }*/
        float fill = currentBarValue / maxBarValue;
        BarMask.fillAmount = fill;
    }

    public void subHealth(float number)
    {
        if (number <= 0) return;
        currentBarValue = Mathf.Clamp(currentBarValue - number, 0, maxBarValue);
        /*if (currentBarValue >= maxBarValue)
        {
            //off the player's collider
        }*/
        float fill = currentBarValue / maxBarValue;
        BarMask.fillAmount = fill;
    }

    private void Update()
    {     
        if (currentBarValue <= 0)
        {
            Destroy(gameObject);
            BarGO.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == "House")
        {
            if(collision.CompareTag("Enemy"))
            {
                subHealth(10 /*enemyDamage*/);
            }
        }

        if(type == "Enemy")
        {
            if(collision.CompareTag("Bullet"))
            {
                subHealth(10 /*bulletDamage*/);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == "Player")
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                subHealth(5 /*enemyDamage*/);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (type == "Player")
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                
                subHealth(5 /*enemyDamage*/);
            }
        }
    }

}
