using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject forgePanel, shopPanel, itemStorage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceMain;
    [SerializeField] private Sprite muteOn, muteOff;
    [SerializeField] private Button muteButton;

    private bool mute;

    private void Start()
    {
        mute = false;
    }

    // Кнопки верхней игровой панели

    public void Restart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Mute()
    {
        if (!mute)
        {
            audioSourceMain.volume = 0;
            muteButton.GetComponent<Image>().sprite = muteOn;
            mute = true;
        }
        else
        {
            audioSourceMain.volume = 0.4f;
            muteButton.GetComponent<Image>().sprite = muteOff;
            mute = false;
        }

    }

    public void Go() // Начало движения персонажа
    {
        audioSource.Play();
        animator.SetBool("isRun", !animator.GetBool("isRun"));        
    }

    // Кнопки перехода в кузницу и в магазин
    public void ForgeButton()
    {
        audioSource.Play();
        forgePanel.SetActive(true);
        itemStorage.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void ShopButton()
    {
        audioSource.Play();
        forgePanel.SetActive(false);
        itemStorage.SetActive(false);
        shopPanel.SetActive(true);
    }
}