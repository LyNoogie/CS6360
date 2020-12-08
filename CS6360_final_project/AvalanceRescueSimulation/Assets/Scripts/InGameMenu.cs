using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this object is enabled and disabled in player_script by key 'h'

public class InGameMenu : MonoBehaviour
{
    private GameObject titleText;
    private GameObject contentText;
    private float escTime = 0f;
    private float startTime = 0f;

    private int confirmExit;
    private GameObject exitMenu;
    private GameObject exitText;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("TitleText");
        contentText = GameObject.Find("ContentText");

        RectTransform objectRectTransform = gameObject.GetComponent<RectTransform>();

        Vector2 canvasSize = new Vector2(objectRectTransform.rect.width, objectRectTransform.rect.height);
        Vector2 titleTextSize = new Vector2(titleText.GetComponent<RectTransform>().rect.width, titleText.GetComponent<RectTransform>().rect.height);
        Vector2 contentTextSize = new Vector2(contentText.GetComponent<RectTransform>().rect.width, contentText.GetComponent<RectTransform>().rect.height);

        //titleText.transform.localScale = new Vector2(canvasSize.y * 0.0009f, canvasSize.y * 0.001f);
        //titleText.transform.position = new Vector3(titleText.transform.position.x, canvasSize.y, titleText.transform.position.z);

        //contentText.transform.localScale = new Vector2(canvasSize.y * 0.00075f, canvasSize.y * 0.00075f);
        //contentText.transform.position = new Vector3(contentText.transform.position.x, canvasSize.y * 0.45f, contentText.transform.position.z);

        escTime = 0f;
        startTime = 0f;
        confirmExit = 0;

        exitMenu = GameObject.Find("ExitPanel");
        exitText = GameObject.Find("ComfirmExitText");
        exitMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Canvas>().enabled) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            confirmExit = (confirmExit + 1) % 3;
            if (confirmExit == 1)
            {// press esc once
                startTime = Time.time;
                exitMenu.SetActive(true);

            }
            else if (confirmExit == 2) {
                SceneManager.LoadScene("StartMenu");
            }
        }

        if (Input.GetKeyDown("c"))
        {
            confirmExit = 0;
            exitMenu.SetActive(false);
        }

            if (confirmExit == 1) {
            escTime = 10.0f - (Time.time - startTime);
            exitText.GetComponent<Text>().text =
                "Press esc to confirm exit\n" +
                "Press q to quit exit \n" +
                "This window will disappear in " + escTime.ToString("F1")+ " sec";
            if (escTime <= 0) {
                confirmExit = 0;
                exitMenu.SetActive(false);
            }
        }



    }
}
