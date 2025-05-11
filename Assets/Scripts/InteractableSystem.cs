using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable 
{
    public void Interact();
}

public class InteractableSystem : MonoBehaviour
{
    public Transform _interactorSource;
    public float interactDistance;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(_interactorSource.position, _interactorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, interactDistance))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
