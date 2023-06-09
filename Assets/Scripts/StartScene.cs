using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour // Скрипт для начального экрана
{
    [SerializeField] Animator heroAnimator;
    
    [SerializeField] private GameObject skill;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject merge;

    private void Start()
    {
        heroAnimator.SetBool("isRun", true);
        StartCoroutine(TitleAnimation());
    }

    public void IdleAnimation()
    {
        heroAnimator.SetBool("isRun", false);
    }

    public void StartButton()
    {

        SceneManager.LoadScene(1);
    }

    private IEnumerator TitleAnimation()
    {
        yield return new WaitForSeconds(1);
        skill.SetActive(true);
        yield return new WaitForSeconds(2);
        skill.SetActive(false);
        game.SetActive(true);
        yield return new WaitForSeconds(2);
        game.SetActive(false);
        merge.SetActive(true);
    }
}