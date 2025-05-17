using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;

    private string currentHint = "";
    private bool isHintVisible = false;

    void Awake()
    {
        Instance = this;
        HideHint();
    }

    public void ShowHint(string message)
    {
        // Если уже видна и текст тот же — вообще ничего не делаем
        if (isHintVisible && currentHint == message)
            return;

        currentHint = message;

        // Меняем текст только если он отличается
        if (hintText.text != message)
            hintText.text = message;

        if (!hintPanel.activeSelf)
            hintPanel.SetActive(true);

        isHintVisible = true;
    }

    public void HideHint()
    {
        if (!isHintVisible) return;

        currentHint = "";
        hintPanel.SetActive(false);
        isHintVisible = false;
    }
}
