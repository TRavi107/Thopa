using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;
    [HideInInspector] public Vector2 leftBound;
    [HideInInspector] public Vector2 rightBound;
    [HideInInspector] public Vector2 bottomBound;
    [HideInInspector] public Vector2 topBound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        leftBound =Camera.main.ScreenToWorldPoint(new( Camera.main.pixelRect.xMin,0,0));
        rightBound = Camera.main.ScreenToWorldPoint(new(Camera.main.pixelRect.xMax, 0, 0));
        bottomBound = Camera.main.ScreenToWorldPoint(new(0,Camera.main.pixelRect.yMin, 0));
        topBound = Camera.main.ScreenToWorldPoint(new(0,Camera.main.pixelRect.yMax, 0));

    }
    private void Update()
    {
        if(GameManager.instance!=null)
            transform.position = new Vector3(transform.position.x, GameManager.instance.platfromController.transform.position.y+1.5f, transform.position.z);
    }

    public bool CheckBound(Vector2 pos)
    {
        if (pos.x < leftBound.x || pos.x > rightBound.x || pos.y < bottomBound.y || pos.y >topBound.y)
        {
            return true;
        }
        return false;
    }

    public bool CheckTwiceBound(Vector2 pos)
    {
        if (pos.x < leftBound.x*3 || pos.x > rightBound.x*3 || pos.y < bottomBound.y*3 || pos.y > topBound.y*3)
        {
            return true;
        }
        return false;
    }

}
