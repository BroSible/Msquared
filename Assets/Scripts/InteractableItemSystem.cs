using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemSystem : MonoBehaviour, IInteractable
{
    public enum Types
    {
        food,
        drink,
        treatment,
    }

    [Header("Item type")]
    public Types type;
    public float value;
    public string itemName;


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
