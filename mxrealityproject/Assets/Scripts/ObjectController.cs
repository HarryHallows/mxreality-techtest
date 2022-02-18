using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public ObjectData data;
    public bool localSwitch;
    public int localState;

    public bool hovered;

    private void Awake()
    {
        data.interact = false;
    }

    private void Update()
    {
        #region legacy
        if (data.interact)
        {
             ObjectCheck(data.objectType); //Using Scriptable Object to obtain object data
        }
        
        #endregion

        localSwitch = data.interact;
        localState = data.state;
    }

    //Redirect functionality based on current object
    public void ObjectCheck(ObjectType _type)
    {
        switch (_type)
        {
            case ObjectType.SPHERE:
                ChangeColour();
                break;

            case ObjectType.CUBE:
                StartCoroutine(Rotate());
                break;

            case ObjectType.CAPSULE:
                StartCoroutine(ScaleObject());
                break;
        }
    } 
 

    public IEnumerator Rotate()
    {
        if (data.interact)
        {
            //Rotate 
            transform.Rotate(new Vector3(0, 100f, 0) * Time.deltaTime);
            data.state = 1;
        }
        else
        {
            data.state = 0;
            yield return null;
        }

        // Spherical Lerping 
        #region legacy
        /*
        float _rotationValue = 0;
        _rotationValue += Time.deltaTime;
        Quaternion _desiredRotation = Quaternion.Euler(transform.eulerAngles.x, _rotationValue, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, 0.03f);
        */
        #endregion

        #region legacy 
        /*else
        {
            //Stop rotating
            Quaternion _resetRotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, _resetRotation, .1f);
        }
        */
        //localSwitch = !localSwitch;
        #endregion
    }

    public void ChangeColour()
    {
        if (data.interact)
        {
            if (data.state == 1)
            {
                //switch the object colour
                data.material.color = new Color32(0, 0, 255, 255);
                data.state = 0; ;
                data.interact = false;
                return;
            }
            else
            {
                //switch back to original colour
                data.material.color = new Color32(0, 255, 0, 255);
                data.state = 1;
                data.interact = false;
                return;
            }       
        }
    }

    public IEnumerator ScaleObject()
    {
        if (data.interact)
        {
            if (data.state == 0)
            {
                //scale upto maxScale (original scale * 2) 
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;

                if (transform.localScale.x >= data.maxScale)
                {
                    data.state = 1;
                    yield return data.interact = false;
                }
            }
            else
            {
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;

                if (transform.localScale.x <= data.minScale)
                {
                    data.state = 0;
                    yield return data.interact = false;
                }
            }
        }
        else
        {
            yield return null;
        }
    }
}
