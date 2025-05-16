using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Main parametrs")]
    public float health = 100f;
    public float hunger = 100f;
    public float thirst = 100f;
    public float money = 50f;

    [Header("Var")]
    public float hungerDecayRate = 1f; // Еда убывает со временем
    public float thirstDecayRate = 1.5f;
    
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleNeeds();
        CheckDeath();
    }

    void HandleNeeds()
    {
        hunger -= hungerDecayRate * Time.deltaTime;
        thirst -= thirstDecayRate * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0, 100);
        thirst = Mathf.Clamp(thirst, 0, 100);
    }

    void CheckDeath()
    {
        if (hunger <= 0 || thirst <= 0)
        {
            health -= 10f * Time.deltaTime;
        }

        if (health <= 0)
        {
            Debug.Log("Игрок умер");
            // Здесь можно вызвать GameOver или другую логику
        }
    }

    #region "Money Logic"
    public void AddMoney(float amount)
    {
        money += amount;
    }

    public bool SpendMoney(float amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }

        else
        {
            Debug.Log("Недостаточно денег");
            return false;
        }
    }
    #endregion


    #region "Needs"
    public void Eat(float foodValue)
    {
        hunger += foodValue;
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    public void Drink(float waterValue)
    {
        thirst += waterValue;
        thirst = Mathf.Clamp(thirst, 0, 100);
    }
    #endregion

}
