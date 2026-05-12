using UnityEngine;

public class PlayAnimationOnScreenClick : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clique esquerdo
        {
            animator.Play("hitpic");
        }
    }
}