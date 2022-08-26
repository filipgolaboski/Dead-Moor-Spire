using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRevive : MonoBehaviour
{
    public float reviveTime;
    public Character character;
    GameObject revivePoint;
    // Start is called before the first frame update
    void Start()
    {
        revivePoint = GameObject.FindGameObjectWithTag("RevivePoint");
    }

    public void Revive()
    {
        StartCoroutine(ReviveCoroutine());
    }

    IEnumerator ReviveCoroutine()
    {
        yield return new WaitForSeconds(reviveTime);
        character.transform.SetPositionAndRotation(revivePoint.transform.position, revivePoint.transform.rotation);
        Debug.Log("REVIVE");
        character.ReviveCharacter();
    }
}
