using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromController : MonoBehaviour
{
    public ColorWithName mycolor;
    public GameObject circlePopPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        SetMyColor(GameManager.instance.GenerateRandomColor());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, transform.position.y + GameManager.instance.currentSpeed / 2 * Time.deltaTime,0);
        //GetComponent<Rigidbody2D>().velocity = Vector2.up * GameManager.instance.currentSpeed / 2;
        if (Input.GetMouseButtonDown(0))
        {
            if (mycolor.type == ColorType.white)
            {
                SetMyColor(GameManager.instance.availableColors[1]);
            }
            else
            {
                SetMyColor(GameManager.instance.availableColors[0]);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        print(collision.gameObject.GetComponent<CircleController>());
        if (collision.gameObject.CompareTag("ColorCircle"))
        {
            CircleController tempController = collision.gameObject.GetComponent<CircleController>();
            if (tempController != null && tempController.myColor.type == mycolor.type)
            {
                Destroy(collision.gameObject);
                GameManager.instance.AddScore(1);
                //Some Particle effects
                //GameObject prefabs = Instantiate(circlePopPrefabs, collision.transform.position, Quaternion.identity, this.transform);
                //prefabs.GetComponent<ParticleSystem>().startColor = mycolor.color;
            }
            else
            {
                //GameOver screen
                GameManager.instance.GameOver();
                //Some particle effects
                print("Game Over");
            }
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColorCircle"))
        {
            CircleController tempController = collision.GetComponent<CircleController>();
            if(tempController!=null && tempController.myColor.type == mycolor.type)
            {
                Destroy(collision.gameObject);
                GameManager.instance.AddScore(1);
                //Some Particle effects
                GameObject prefabs=Instantiate(circlePopPrefabs, collision.transform.position, Quaternion.identity,this.transform);
                prefabs.GetComponent<ParticleSystem>().startColor = mycolor.color;
            }
            else
            {
                //GameOver screen
                GameManager.instance.GameOver();
                //Some particle effects
                print("Game Over");
            }
        }
    }

    void SetMyColor(ColorWithName colorId)
    {
        GetComponent<SpriteRenderer>().color = colorId.color;

        mycolor = new ColorWithName {
            color=colorId.color,
            type=colorId.type
        };
    }
}
