using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private GameObject hero;
    [SerializeField] private Button goButton;
    [SerializeField] private int income; // Золото, выпадающее из врага при убийстве

    private Animator enemyAnimator;

    public int Income { get { return income; } set { income = value; } }

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // При столкновении с героем, обект назначается врагом и начинается бой
    {
        heroAnimator.SetBool("isRun", false);
        hero.GetComponent<Fighting>().SetEnemy(gameObject);
        goButton.interactable = false;
        StartCoroutine(hero.GetComponent<Fighting>().StartFight());
    }    
}