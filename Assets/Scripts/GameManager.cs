using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool playerIsDead = false;

    [SerializeField] EndGameUI gameUI;
    [SerializeField] GameObject Cinemachine;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TMP_Text EnemyCounter;

    private int _numberOfEnemies = 5;
    public int NumberOfEnemies
    {
        get { return _numberOfEnemies; }
        set 
        { 
            _numberOfEnemies = value;
            EnemyCounter.text = _numberOfEnemies.ToString();
            if (_numberOfEnemies == 0)
                gameUI.GameWon();
        }
    }

    void Start()
    {
        OpenUI();
        Time.timeScale = 0;
        EnemyCounter.text = NumberOfEnemies.ToString();
        Instance = this;
    }
    
    public void Play() 
    {
        CloseUI();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InputSystem.DisableAllEnabledActions();
        Cinemachine.SetActive(false);
    }

    public void CloseUI() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput.actions.Enable();
        Cinemachine.SetActive(true);
    }
}
