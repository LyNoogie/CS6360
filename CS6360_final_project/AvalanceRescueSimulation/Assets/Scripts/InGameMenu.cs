using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this object is enabled and disabled in player_script by key 'h'

public class InGameMenu : MonoBehaviour
{
    private GameObject titleText;
    private GameObject contentText;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("TitleText");
        contentText = GameObject.Find("ContentText");

        RectTransform objectRectTransform = gameObject.GetComponent<RectTransform>();

        Vector2 canvasSize = new Vector2(objectRectTransform.rect.width, objectRectTransform.rect.height);
        Vector2 titleTextSize = new Vector2(titleText.GetComponent<RectTransform>().rect.width, titleText.GetComponent<RectTransform>().rect.height);
        Vector2 contentTextSize = new Vector2(contentText.GetComponent<RectTransform>().rect.width, contentText.GetComponent<RectTransform>().rect.height);

        titleText.transform.localScale = new Vector2(canvasSize.y * 0.001f, canvasSize.y * 0.001f);
        titleText.transform.position = new Vector3(titleText.transform.position.x, canvasSize.y *0.85f, titleText.transform.position.z);

        contentText.transform.localScale = new Vector2(canvasSize.y * 0.00075f, canvasSize.y * 0.00075f);
        contentText.transform.position = new Vector3(contentText.transform.position.x, canvasSize.y * 0.5f, contentText.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
