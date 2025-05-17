using UnityEngine;

interface IInteractable
{
    void Interact();
    void ShowHint();
    void HideHint();
}

public class InteractableSystem : MonoBehaviour
{
    public Transform _interactorSource;
    public float interactDistance;
    [Header("Included Layers for interact")]
    public LayerMask interactableLayerMask;
    private GameObject currentLookTarget;

    void Update()
    {
        Ray ray = new Ray(_interactorSource.position, _interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance,interactableLayerMask))
        {
            GameObject target = hitInfo.collider.gameObject;

            // Если мы смотрим на новый объект — меняем подсказку
            if (target != currentLookTarget)
            {
                ClearHint();

                if (target.TryGetComponent<IInteractable>(out var interactable))
                {
                    currentLookTarget = target;
                    interactable.ShowHint();
                }
            }

            // Если смотрим на тот же объект — подсказка не трогаем
            // Проверяем нажатие кнопки
            if (target == currentLookTarget && Input.GetKeyDown(KeyCode.E))
            {
                if (currentLookTarget.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            ClearHint();
        }
    }

    private void ClearHint()
    {
        if (currentLookTarget != null)
        {
            if (currentLookTarget.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.HideHint();
            }
            currentLookTarget = null;
        }
    }
}
