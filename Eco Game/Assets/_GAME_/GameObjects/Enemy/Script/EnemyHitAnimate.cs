using UnityEngine;

public class EnemyHitAnimate : MonoBehaviour
{

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HitAnimation()
    {
        animator.SetTrigger("EnemyHit");
    }
}
