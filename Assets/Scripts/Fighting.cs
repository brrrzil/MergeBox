using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Fighting : MonoBehaviour
{
    [SerializeField] private float power, shield;
    [SerializeField] private GameObject hero;
    [SerializeField] private Button goButton;

    private GameObject enemy;
    private static bool nextReady;
    private Animator animator;
    private AudioSource audioSource;

    public float Power { get { return power; } set { power = value; } }
    public float Shield { get { return shield; } set { shield = value; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetEnemy(GameObject outerEnemy) // Вызывается из Enemy Behaviour для обозначения врага
    {
        enemy = outerEnemy;
    }

    private void Attack(GameObject victim)
    {
        animator.SetBool("isAttack", true);
        victim.GetComponent<Health>().GetDamage(power);
        audioSource.Play();
    }

    public IEnumerator StartFight() // Пока оба живы, оба наносят удары по очереди
    {
        while (enemy.GetComponent<Health>().IsAlive() && hero.GetComponent<Health>().IsAlive())
        {
            if (enemy.GetComponent<Health>().IsAlive())
            {
                nextReady = false;
                hero.GetComponent<Fighting>().Attack(enemy);
                if (!enemy.GetComponent<Health>().IsAlive()) goButton.interactable = true;
            }

            while (!nextReady)
            {
                yield return new WaitForSeconds(0.5f);
            }
            hero.GetComponent<Animator>().SetBool("isAttack", false);

            if (hero.GetComponent<Health>().IsAlive())
            {
                nextReady = false;
                enemy.GetComponent<Fighting>().Attack(hero);
                if (!hero.GetComponent<Health>().IsAlive()) goButton.interactable = true;
            }

            while (!nextReady)
            {
                yield return new WaitForSeconds(0.5f);
            }
            enemy.GetComponent<Animator>().SetBool("isAttack", false);
        }        
        yield return null;
    }

    public void NextReady() // Вставляется как Event в конец анимации удара для перехода хода
    {
        nextReady = true;
    }

    //public void BonusPower(float bonus)
    //{
    //    hero.GetComponent<Fighting>().power += bonus;
    //}

    //public void BonusShield(float bonus)
    //{
    //    shield += bonus;
    //}
}