using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineRobbery : MonoBehaviour, IDamageable
{
    [Header("Transmitted values")]
    public Transform _spawnItemPoint;
    public List<GameObject> _items = new List<GameObject>();

    [Header("Var")]
    public float currentVendingHealth;
    public float maxVendingHealth;
    private bool isBroken = false;

    #region Links
    private InteractableItemSystem _itemInstance;
    public bool IsBroken => isBroken; //public property for the state of the machine
    #endregion

    void Start()
    {
        currentVendingHealth = maxVendingHealth;
        _itemInstance = InteractableItemSystem.Instance;
    }

    public void TakeDamage(float damage)
    {
        if (isBroken)
        {
            return;
        }

        currentVendingHealth -= damage;

        if (currentVendingHealth <= 0f)
        {
            currentVendingHealth = 0f;
            RobVendingMachine();
        }
    }

    void RobVendingMachine()
    {
        isBroken = true;

        if (_items.Count == 0)
        {
            return;
        }

        int minCountDrop = 1;
        int maxCountDrop = 5;
        int randomItemCount = Random.Range(minCountDrop, maxCountDrop + 1); // from 1 to 5 items

        for (int i = 0; i < randomItemCount; i++)
        {
            int randomItemIndex = Random.Range(0, _items.Count);
            Instantiate(_items[randomItemIndex], _spawnItemPoint.position, Quaternion.identity);

            // // Если предмет — деньги, задаём случайный номинал
            // if (_itemInstance.TryGetComponent<InteractableItemSystem>(out var itemSystem) && itemSystem.type == InteractableItemSystem.Types.cash)
            // {
            //     float minCash = 5f;
            //     float maxCash = 50f;
            //     itemSystem.value = Random.Range(minCash, maxCash); // случайный номинал
            //     itemSystem.itemName = $"{(int)itemSystem.value} $";
            // }
        }

        Debug.Log($"Автомат ограблен! Выпало предметов: {randomItemCount}");
    }

}

