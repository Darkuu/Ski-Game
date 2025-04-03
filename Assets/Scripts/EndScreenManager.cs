using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Image crossFade;


    private void Start()
    {
        endScreen.SetActive(false);
        crossFade.CrossFadeAlpha(0, 1f, true);
    }

    private void OnEnable()
    {
        GameEvents.raceEnd += EnableGameOver;
    }


    private void OnDisable()
    {
        GameEvents.raceEnd -= EnableGameOver;
    }

    private void EnableGameOver()
    {
        endScreen.SetActive(true);
    }


    public void restartScene()
    {
        StartCoroutine(RestartSceneCouroutine());
    }

    public void NextScene()
    {
        StartCoroutine(NextSceneCouroutine());
    }

    public void quitGame()
    {
        StartCoroutine(QuitGameCouroutine());
    }

    private IEnumerator QuitGameCouroutine()
    {
        crossFade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Game has been quit!");
        Application.Quit();
    }
    private IEnumerator NextSceneCouroutine()
    {
        crossFade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Game has been quit!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private IEnumerator RestartSceneCouroutine()
    {
        crossFade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
