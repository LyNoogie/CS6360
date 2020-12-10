using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost_items : MonoBehaviour
{
	public GameObject ski1;
	public GameObject ski2;
	public GameObject scarf;

	public bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        ski1 = GameObject.Find("Lost_Ski_1");
		ski2 = GameObject.Find("Lost_Ski_2");
		scarf = GameObject.Find("Scarf");

    }

    // Update is called once per frame
    void Update()
    {
        loaded = true;
    }

    public void SetItems(Vector3 i1, Vector3 i2, Vector3 i3)
    {
    	ski1.transform.position = i1 + new Vector3(0f, 10f, 0f);
    	ski2.transform.position = i2 + new Vector3(0f, 10f, 0f);;
    	scarf.transform.position = i3 + new Vector3(0f, 10f, 0f);;

    	ski1.transform.rotation = Random.rotation;
    	ski2.transform.rotation = Random.rotation;
    	scarf.transform.rotation = Random.rotation;


    }
}
