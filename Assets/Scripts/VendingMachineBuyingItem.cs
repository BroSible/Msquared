using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class VendingMachineBuyingItem : MonoBehaviour, IInteractable
{
    public float price;
    public GameObject _item;
    public Transform _spawnItemPoint;
    public PlayerStats _player;

    public void Start()
    {
        _player = PlayerStats.Instance;
    }
    
    public void Interact()
    {
        if (_player.SpendMoney(price))
        {
            Instantiate(_item, _spawnItemPoint.position, _spawnItemPoint.rotation);
            Debug.Log("Покупка прошла успешно");
        }
    }
}
