using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private Button RePlayButton;
    public void GameLost()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OpenUI();
        m_Text.text = "You Lost";
        RePlayButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void GameWon()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OpenUI();
        m_Text.text = "You Won";
        RePlayButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
