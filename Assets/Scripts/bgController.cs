using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance !=null)
            transform.position = new Vector3(0, transform.position.y - GameManager.instance.currentSpeed / 2 * Time.unscaledDeltaTime,0);
        if(transform.position.y < GameManager.instance.bgdeletePos.position.y)
        {
            GameManager.instance.GenerateBG();
            Destroy(this.gameObject);
        }

    }
}
