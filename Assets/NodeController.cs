using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private GameObject[] nodeList;
    public GameObject[] GetNodeArray() {
        return nodeList;
    }
}
