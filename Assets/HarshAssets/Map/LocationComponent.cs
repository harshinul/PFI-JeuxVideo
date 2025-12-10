using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationComponent : MonoBehaviour
{
    public int index;

}

[Serializable]
public class LocationNode
{
    public int index;
    public GameObject gameObject;
    public List<IntTuple> neighboors;
}
