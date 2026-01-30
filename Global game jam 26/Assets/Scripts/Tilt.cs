using System.Collections;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    private RectTransform m_RectTransform;

    [SerializeField] private float flickStrength = 15f; // total degrees of flick
    [SerializeField] private float flickInterval = 3f;  // seconds between flicks
    [SerializeField] private int steps = 5;             // number of steps to animate
    [SerializeField] private float stepDelay = 0.05f;   // delay per step

    private float timer;
    private bool isFlicking;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flickInterval && !isFlicking)
        {
            StartCoroutine(Flick());
            timer = 0f;
        }
    }

    private IEnumerator Flick()
    {
        isFlicking = true;
        float anglePerStep = flickStrength / steps;

        // tilt one direction in small steps
        for (int i = 0; i < steps; i++)
        {
            m_RectTransform.Rotate(Vector3.forward * -anglePerStep);
            yield return new WaitForSeconds(stepDelay);
        }

        // return back in same-size steps
        for (int i = 0; i < steps; i++)
        {
            m_RectTransform.Rotate(Vector3.forward * anglePerStep);
            yield return new WaitForSeconds(stepDelay);
        }

        isFlicking = false;
    }
}
