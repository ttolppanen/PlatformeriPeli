using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu instance;
    public bool isPaused;

    public GameObject buttonsParent;
    public GameObject[] buttons = new GameObject[0];
    public Color selectedButtonColor;
    public Color stantardButtonColor;
    int buttonIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isPaused = false;
        buttonIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonsParent.SetActive(!buttonsParent.activeSelf);

            if (buttonsParent.activeSelf)
            {
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
            }
        }

        if (buttonsParent.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonIndex -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttonIndex += 1;
            }
            buttonIndex = Mathf.Clamp(buttonIndex, 0, buttons.Length - 1);

            foreach (GameObject button in buttons)
            {
                ColorBlock buttonColors = button.GetComponent<Button>().colors;
                buttonColors.normalColor = stantardButtonColor;
                button.GetComponent<Button>().colors = buttonColors;
            }

            Button buttonComponent = buttons[buttonIndex].GetComponent<Button>();
            ColorBlock buttonColor = buttonComponent.colors;
            buttonColor.normalColor = selectedButtonColor;
            buttonComponent.colors = buttonColor;

            if (Input.GetKeyDown(KeyCode.X))
            {
                buttons[buttonIndex].SendMessage("PressButton");
            }
        }
    }
}
