using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private ObjectController objectController;
    private RaycastHit hit;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private List<ObjectData> interactCheck;
    private GameObject currentHit;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        InteractableObjects(_ray);
    }

    private void InteractableObjects(Ray _ray)
    {
        if(Physics.Raycast(_ray, out hit, Mathf.Infinity, interactableLayer))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider != null) //Could change this out for a pre-binded input using input manager or action maps
            {
                Debug.Log($"{hit.collider.name} is hit after all conditions");
                //Storing generic variable for cleaner re-usage of the component reference
                objectController = hit.collider.GetComponent<ObjectController>();
                
                ObjectData _data = objectController.data;

                if (objectController.data.objectType == ObjectType.CAPSULE)
                {
                    ScaleObjectCheck(_data);
                }
                else
                {
                    _data.interact = !_data.interact;
                }

                objectController.ObjectCheck(_data.objectType);
            }
        }        
    }

    private void ScaleObjectCheck(ObjectData _data)
    {
        Debug.Log("checking calls");

        for (int i = 0; i < interactCheck.Count; i++)
        {
            if (interactCheck[i].state == 1 && _data.state == 0)
            {
                return;
            }
        }

        //Checking if either condition is met (To prevent spam clicking to stop scaling)
        if (objectController.transform.localScale.x >= objectController.data.maxScale ||
            objectController.transform.localScale.x <= objectController.data.minScale)
        {
            _data.interact = !_data.interact;
        }
    }
}
