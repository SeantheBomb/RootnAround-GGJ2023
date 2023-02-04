using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class DialogueButton : MonoBehaviour
{
    //public DialogueView view;
    Text text;
    Button button;

    public void UpdateButton(string text, System.Action action)
    {
        if (button == null)
            button = GetComponent<Button>();
        if (this.text == null)
            this.text = GetComponentInChildren<Text>();

        this.text.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { action(); Debug.Log("ResponseNode: Button Pressed"); });
    }

    void PressButton()
    {
        if (button == null)
            button = GetComponent<Button>();
        button?.onClick.Invoke();
    }

    public void SetActive(bool on)
    {
        gameObject.SetActive(on);
    }
}

