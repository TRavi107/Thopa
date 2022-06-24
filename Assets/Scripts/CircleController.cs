using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public ColorWithName myColor;

    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        SetMyColor(GameManager.instance.GenerateRandomColor());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, transform.position.y - GameManager.instance.currentSpeed / 2 * Time.deltaTime,0);
        if (!spawned && transform.position.y < GameManager.instance.deletePos.position.y)
        {
            spawned = true;
            GameManager.instance.GenerateColorCicles();
        }
    }

    void SetMyColor(ColorWithName colorId)
    {
        GetComponent<SpriteRenderer>().color = colorId.color;

        myColor = new ColorWithName
        {
            color = colorId.color,
            type = colorId.type
        };
    }
}
