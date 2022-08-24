using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCharacterDetector : MonoBehaviour
{
    public float chancesOfAppereance;
    public float distanceFromPlayer;
    public Vector2 targetPosition;
    public float speed;
    public float returnSpeed;


    Vector2 originPosition;
    bool shownState;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shownState && player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > distanceFromPlayer)
            {
                Debug.Log(Vector2.Distance(transform.position, player.transform.position));
                if (Random.Range(0f, 1f) < chancesOfAppereance)
                {
                    shownState = true;
                    StartCoroutine(ShowSkeleton());
                }
            }
        }
    }

    IEnumerator ShowSkeleton()
    { 
        while(Vector2.Distance(transform.position,targetPosition) > 0.001f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(HideSkeleton());
    }

    IEnumerator HideSkeleton()
    {
        while (Vector2.Distance(transform.position, originPosition) > 0.001f)
        {
            transform.position = Vector2.Lerp(transform.position, originPosition, speed * Time.deltaTime);
            yield return null;
        }
        shownState = false;
    }
}
