using UnityEngine;

public class VendingMachineBuyingItem : MonoBehaviour, IInteractable
{
    public float price;
    public GameObject _item;
    public Transform _spawnItemPoint;
    private PlayerStats _player;
    private bool isHintShown = false;
    private VendingMachineRobbery _vendingMachineRobbery;

    private void Start()
    {
        _player = PlayerStats.Instance;
        _vendingMachineRobbery = GetComponent<VendingMachineRobbery>();
    }

    public void ShowHint()
    {
        if (!isHintShown && !_vendingMachineRobbery.IsBroken)
        {
            string message = $"Купить предмет за <color={(_player.money > price ? "green" : "red")}>{price}</color> (E)";
            InteractionUI.Instance.ShowHint(message);
            isHintShown = true;
        }
    }

    public void HideHint()
    {
        if (isHintShown)
        {
            InteractionUI.Instance.HideHint();
            isHintShown = false;
        }
    }

    public void Interact()
    {
        if (_player.SpendMoney(price) && !_vendingMachineRobbery.IsBroken)
        {
            Instantiate(_item, _spawnItemPoint.position, _spawnItemPoint.rotation);
            InteractionUI.Instance.ShowHint("<color=green>Покупка успешна!</color>");
            Debug.Log("Покупка прошла успешно");
        }
        else
        {
            InteractionUI.Instance.ShowHint("<color=red>Недостаточно денег</color>");
            Debug.Log("Недостаточно денег");
        }
    }
}
