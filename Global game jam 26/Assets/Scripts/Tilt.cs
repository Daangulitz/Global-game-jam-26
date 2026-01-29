using System.Collections;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    private RectTransform m_RectTransform;
    [SerializeField] private float flickStrength;
    [SerializeField] private int flickFrames;
    float time;
    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        time++;
        if (time >= flickFrames)
        {
            StartCoroutine(Flick());
            time = 0;
        }
    }


    private IEnumerator Flick()
    {
        for (int i = 0; i > 5; i++)
        {
            m_RectTransform.Rotate(Vector3.forward * -flickStrength * i);

            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 5; i--)
        {
            m_RectTransform.Rotate(-Vector3.forward * -flickStrength * i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
