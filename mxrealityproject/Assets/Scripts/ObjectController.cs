using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public ObjectData data;

    private void Update()
    {
        if (data.interact)
        {
            ObjectCheck(data.objectType); //Using Scriptable Object to obtain object data
        }
    }

    //Redirect functionality based on current object
    public void ObjectCheck(ObjectType _type)
    {
        switch (_type)
        {
            case ObjectType.SPHERE:
                ChangeColour(data.interact);
                break;

            case ObjectType.CUBE:
                Rotate(data.interact);
                break;

            case ObjectType.CAPSULE:
                ScaleObject(data.minScale, data.maxScale);
                break;
        }
    } 

    public void Rotate(bool _rotate)
    {
        if (_rotate)
        {
            //Rotate this object
            transform.Rotate(90f, 0f, 0f);
        }
        else
        {
            //Stop rotating
            
        }
    }

    public void ChangeColour(bool _changeColour)
    {
        if (_changeColour)
        {
            //switch the object colour

        }
        else
        {
            //switch back to original colour
        }
    }

    public void ScaleObject(float _minScale, float _maxScale)
    {
        if (transform.localScale.x <= _maxScale)
        {
            //scale upto maxScale (original scale * 2) 
        }
        else
        {
            //scale down to minScale (original scale == 1) 
        }
    }
}
