using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int health = 100;
    private int maxHealth = 100;
    public int food = 100;
    private int maxFood = 100;
    public int water = 100;
    private int maxWater = 100;
    public int stamina = 100;
    private int maxStamina = 100;

    private void Start()
    {
        Invoke(nameof(UpdateStats), 1);
    }

    public void UpdateStats()
    {
        GameManager.Instance.canvasManager.UpdateStats(0, health);
        GameManager.Instance.canvasManager.UpdateStats(1, food);
        GameManager.Instance.canvasManager.UpdateStats(2, water);
        GameManager.Instance.canvasManager.UpdateStats(3, stamina);
    }

    public void ConsumableProcess(consumableType consumableType, int value)
    {
        if(consumableType == consumableType.Health)
        {
            ChangeHealth(value);
        }
        if (consumableType == consumableType.Food)
        {
            ChangeFood(value);
        }
        if (consumableType == consumableType.Water)
        {
            ChangeWater(value);
        }
        if (consumableType == consumableType.Stamina)
        {
            ChangeStamina(value);
        }
        if (consumableType == consumableType.Poison)
        {
            ChangeHealth(value);
        }
    }

    void ChangeHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health < 0)
        {
            health = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(0, health);
    }
    void ChangeFood(int value)
    {
        food += value;
        if (food > maxFood)
        {
            food = maxFood;
        }
        if (food < 0)
        {
            food = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(1, food);
    }
    void ChangeWater(int value)
    {
         water += value;
        if (water > maxWater)
        {
            water = maxWater;
        }
        if (water < 0)
        {
            water = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(2, water);
    }
    void ChangeStamina(int value)
    {
         stamina += value;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina < 0)
        {
            stamina = 0;
        }
        GameManager.Instance.canvasManager.UpdateStats(3, stamina);
    }

    public List<int> SaveStats()
    {
        List<int> stats = new List<int>();
        stats.Add(health);
        stats.Add(food);
        stats.Add(water);
        stats.Add(stamina);
        return stats;
    }

    public void LoadStats(List<int> stats)
    {
        health = stats[0];
        food = stats[1];
        water = stats[2];
        stamina = stats[3];
        UpdateStats();
    }
}
