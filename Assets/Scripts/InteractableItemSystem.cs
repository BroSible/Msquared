using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemSystem : MonoBehaviour, IInteractable
{
    public static InteractableItemSystem Instance;
    public enum Types
    {
        food,
        drink,
        treatment,
        cash,
    }

    [Header("Item type")]
    public Types type;
    public float value;
    public string itemName;

    void Start()
    {
        Instance = this;
    }


    public void Interact()
    {
        var player = PlayerStats.Instance;
        switch (type)
        {
            case Types.food:
                player.Eat(value);
                Destroy(gameObject);
                break;

            case Types.drink:
                player.Drink(value);
                Destroy(gameObject);
                break;

            case Types.treatment:
                player.health += value;
                player.health = Mathf.Clamp(player.health, 0, 100);
                Destroy(gameObject);
                break;

            case Types.cash:
                player.AddMoney(value);
                Destroy(gameObject);
                break;
        }
        InteractionUI.Instance.HideHint();
    }
    
    public void ShowHint()
    {
        string action = type switch
        {
            Types.food => "eat",
            Types.drink => "drink",
            Types.treatment => "use",
            Types.cash => "take",
            _ => "interact"
        };

        string message = $"Press <color=yellow>E</color>, to {action} {itemName}";
        InteractionUI.Instance.ShowHint(message);
    }

    public void HideHint()
    {
        InteractionUI.Instance.HideHint();
    }

}
