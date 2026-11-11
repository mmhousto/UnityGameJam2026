using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private static PopUp instance;
    public static PopUp Instance {  get { return instance; } }

    public GameObject popUpPanel;
    public TextMeshProUGUI popUpText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        popUpPanel.SetActive(false);
    }

    public void SetText(string text)
    {
        popUpText.text = text;
    }

    public void ShowPopUp()
    {
        popUpPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            popUpPanel.SetActive(false);
        }
    }
}
