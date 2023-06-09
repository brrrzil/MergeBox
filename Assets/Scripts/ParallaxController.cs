using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private Animator animator;
    [SerializeField] private float[] coeff;
    [SerializeField] private float speed;

    private int layersCount;

    private void Start()
    {
        layersCount = layers.Length;
    }

    private void Update() // �������� ����� ��������� � ������� ���������� � ���������
    {
        if (animator.GetBool("isRun"))
        {
            MoveEnvironment();            
        }
    }

    private void MoveEnvironment()
    {
        for (int i = 0; i < layersCount; i++)
        {
            layers[i].position = new Vector2(layers[i].position.x - coeff[i] * speed/500, layers[i].position.y);
        }
    }
}