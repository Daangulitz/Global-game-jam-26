using System.Collections;
using UnityEngine;

public class DestructForShortDuration : MonoBehaviour
{
    [SerializeField] private float destroyedTime = 4;
    [SerializeField] private float delay = 1;
    private SpriteRenderer sr;
    private BoxCollider2D bc;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Destroying());
        }
    }

    private IEnumerator Destroying()
    {
        sr.color = Color.gray;
        yield return new WaitForSeconds(delay);
        sr.enabled = false;
        bc.enabled = false;
        yield return new WaitForSeconds(destroyedTime);
        sr.enabled = true;
        bc.enabled = true;
    }
}
