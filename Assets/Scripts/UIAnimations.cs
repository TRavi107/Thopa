using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    public Transform minPos;
    public Transform maxPos;
    public float speed;
    public bool OneTime;

    private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = maxPos;
    }
    private void OnEnable()
    {
        destination = maxPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (destination == null)
            return;
        if (Vector2.Distance(transform.position, destination.position) < 0.01)
        {
            if (!OneTime)
            {
                destination = destination == maxPos ? minPos : maxPos;
            }
                
        }
        transform.position = Vector2.MoveTowards(transform.position, destination.position, speed * Time.unscaledDeltaTime);
    }

}
