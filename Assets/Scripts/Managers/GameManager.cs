using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public int health;
    public int money;

    [Header("Components")]
    public TextMeshProUGUI healthAndMoneyText;
    public EnemyPath enemyPath;

    [Header("Events")]
    public UnityEvent onEnemyDestroyed;
    public UnityEvent onMoneyChange;

    //Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void UpdateHealthAndMoneyText()
    {
        healthAndMoneyText.text = $"Health: {health}\nMoney: {money}";
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateHealthAndMoneyText();

        onMoneyChange.Invoke();
    }

    public void TakeMoney(int amount)
    {
        money -= amount;
        UpdateHealthAndMoneyText();

        onMoneyChange.Invoke();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealthAndMoneyText();

        if (health <= 0)
            GameOver();
    }

    void GameOver()
    {
        
    }

    void winGame()
    {

    }

    public void OnEnemyDestroyed()
    {

    }
}
