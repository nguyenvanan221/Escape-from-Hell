using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // energy value
    float EnergyValue;
    readonly float energyTotal = 7;

    [Header("Manager Text")]
    [SerializeField] private TextMeshProUGUI energyTxt;
    [SerializeField] private TextMeshProUGUI numbulletTxt;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause Game")]
    [SerializeField] private GameObject pauseGameScreen;
    [HideInInspector] public bool isPause;
    [SerializeField] private Image image;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    PlayerControls controls;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseGameScreen.SetActive(false);
        EnergyValue = 0;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        controls = new();
        controls.Enable();
        controls.Manager.Pause.performed += ctx =>
        {
            if (pauseGameScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else PauseGame(true);
        };

        image.sprite = playSprite;
    }

    private void Update()
    {
        energyTxt.text = EnergyValue.ToString();
        numbulletTxt.text = Gun.NumBullet.ToString();

    }

    public void AddEnergy(float _value)
    {
        EnergyValue = Mathf.Clamp(EnergyValue + _value, 0f, energyTotal);
    }

    #region GameOver
    //Active Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
        Time.timeScale = 0f;
    }

    //Game over func
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
    }
    #endregion

    #region Pause Game
    public void PauseGame(bool _status)
    {
        pauseGameScreen.SetActive(_status);

        if (_status)
        {
            Time.timeScale = 0f;
            isPause = true;
            
            image.sprite = pauseSprite;
        }
        else
        {
            Time.timeScale = 1f;
            isPause = false;
            image.sprite = playSprite;

        }
    }
    #endregion
}
