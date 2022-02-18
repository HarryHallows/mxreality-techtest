using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    SPHERE,
    CUBE,
    CAPSULE
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectInfo", order = 1)]
public class ObjectData : ScriptableObject
{
    public ObjectType objectType;
    public Vector3 transform;
    public Material material;

    public float xRotation;
    public bool interact;

    public float minScale = 1f;
    public float maxScale = 2f;
}
