using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_trigger : MonoBehaviour
{
    //public GameObject lo=null;
    public Light light_t;
    public light_game game;
    // Start is called before the first frame update
    void Start()
    {

        game = (light_game)FindObjectOfType(typeof(light_game));
        if (game)
        {
            Debug.Log("found light game");

        }
        else
        {
            Debug.Log("light game not found");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider trig)
    {
        Debug.Log("entered Trigger");
    }


    void OnTriggerStay(Collider trig)
    {
        //var light = this.gameObject.GetComponent<Light>();
        if (light_t != null)
        {
            if (light_t.enabled == true)
            {
                Debug.Log("Triggered on light!");
                game.updateScore();
                
            }
        }
       
    }
}
