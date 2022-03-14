using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image health;
    public void LoseHealth(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth - dmg <= 0)
        {
            //player dead
            currentHealth = 0;
            EnemySpawner.Instance.OnPlayerDead();
            EnemySpawner.isGameOver = true;
        } else if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

        health.fillAmount = currentHealth / maxHealth;
        EnemySpawner.Instance.UpdateUI();
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        health.fillAmount = currentHealth / maxHealth;

    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
