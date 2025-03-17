using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDown;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback (GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;

        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
        
    }

    private IEnumerator Reset ()
    {
        yield return new WaitForSeconds(coolDown);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }

}
