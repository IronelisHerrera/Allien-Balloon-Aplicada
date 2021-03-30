using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private Button btnPause;
    [SerializeField]
    private CanvasGroup mainCanvasGroup;
    public bool isPaused = false;
    public bool isPlaying = true;
    public int points = 10;
    public float time = 0.0f;
    [SerializeField]
    private TMP_Text txtPoints;
    [SerializeField]
    private TMP_Text txtTimer;
    [SerializeField]
    private GameObject txtPaused;
    [SerializeField]
    private GameObject txtGameOver;
    [SerializeField]
    private GameObject txtGameWin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;

        btnPause.onClick.AddListener(() =>
        {
            PauseGame();
        });

        StartCoroutine(CO_LosePoints());
    }

    private void Update()
    {
        if (isPaused && Input.anyKeyDown)
        {
            UnPauseGame();
        }

        if (!isPaused && isPlaying)
        {
            txtPoints.SetText(points.ToString("000"));
        }

        if (isPlaying && points <= 0)
        {
            GameOver();
        }
        else if (isPlaying && points >= 500)
        {
            GameWin();
        }

        time += Time.deltaTime;

        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        txtTimer.SetText(string.Format("{0}:{1}", minutes, seconds));
    }

    private IEnumerator CO_LosePoints()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(10.0f);
            points -= 5;

            if (points < 0)
            {
                points = 0;
            }
        }
    }

    public void PauseGame()
    {
        txtPaused.gameObject.SetActive(true);
        mainCanvasGroup.interactable = false;
        isPaused = true;
        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
        txtPaused.gameObject.SetActive(false);
        mainCanvasGroup.interactable = true;
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        mainCanvasGroup.interactable = false;
        Time.timeScale = 0.0f;
        txtGameOver.SetActive(true);
        isPlaying = false;
        StartCoroutine(CO_ResetGame());
    }

    public void GameWin()
    {
        mainCanvasGroup.interactable = false;
        Time.timeScale = 0.0f;
        txtGameWin.SetActive(true);
        isPlaying = false;
        StartCoroutine(CO_ResetGame());
    }

    private IEnumerator CO_ResetGame()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        while (true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Main");
            }

            yield return null;
        }
    }
}
