using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public Character characterPrefab;
    public GameObject revivePoint;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate<Character>(characterPrefab).transform.SetPositionAndRotation(revivePoint.transform.position, revivePoint.transform.rotation);    
    }

}
