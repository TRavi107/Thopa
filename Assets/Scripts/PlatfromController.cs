using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromController : MonoBehaviour
{
    public ColorWithName mycolor;
    public GameObject circlePopPrefabs;

    private float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        SetMyColor(GameManager.instance.GenerateRandomColor());
        originalScale = 1;
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
            soundManager.instance.PlaySound(SoundType.colorChangeSound);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ColorCircle"))
        {
            CircleController tempController = collision.gameObject.GetComponent<CircleController>();
            if (tempController != null && tempController.myColor.type == mycolor.type)
            {
                Destroy(collision.gameObject);
                GameManager.instance.AddScore(1);
                //Some Particle effects
                
                GameObject prefabs = Instantiate(circlePopPrefabs, collision.gameObject.transform.position, Quaternion.identity);
                //prefabs.transform.position = new(prefabs.transform.position.x, prefabs.transform.position.y, 1);
                //StopCoroutine(nameof(ZoomPlatform));
                //StartCoroutine(nameof(ZoomPlatform));
                GameManager.instance.TouchShakeCamera();
                prefabs.GetComponentInChildren<ParticleSystem>().startColor = mycolor.color;
                soundManager.instance.PlaySound(SoundType.circleHitsound);
            }
            else
            {
                //GameOver screen
                soundManager.instance.PlaySound(SoundType.gameOverSound);
                StopCoroutine(nameof(DestroyPlatform));
                StartCoroutine(nameof(DestroyPlatform));
            }
        }
    }

    [System.Obsolete]
    IEnumerator DestroyPlatform()
    {
        GameManager.instance.ShakeCamera();
        GameObject prefabs = Instantiate(circlePopPrefabs, transform.position, Quaternion.identity);
        prefabs.GetComponentInChildren<ParticleSystem>().startColor = mycolor.color;
        GameManager.instance.currentSpeed=0;
        //Some particle effects
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(0.25f);
        GameManager.instance.GameOver();
    }

    IEnumerator ZoomPlatform()
    {
        transform.localScale = 1.1f*Vector2.one;
        originalScale = 1.1f;
        yield return new WaitForSeconds(0.75f);
        transform.localScale = 1 * Vector2.one;
        originalScale = 1f;
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
        StopCoroutine(nameof(SwitchColor));
        StartCoroutine(nameof(SwitchColor), colorId);
        //GetComponent<SpriteRenderer>().color = colorId.color;

        //mycolor = new ColorWithName
        //{
        //    color = colorId.color,
        //    type = colorId.type
        //};
    }

    IEnumerator SwitchColor(ColorWithName colorId)
    {
        while(transform.localScale.x > 0.01f)
        {
            transform.localScale = new Vector2(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f);
            yield return new WaitForSecondsRealtime(0.005f);
        }
        GetComponent<SpriteRenderer>().color = colorId.color;

        mycolor = new ColorWithName
        {
            color = colorId.color,
            type = colorId.type
        };
        while (transform.localScale.x < 1)
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f);
            yield return new WaitForSecondsRealtime(0.005f);
        }

    }
}
