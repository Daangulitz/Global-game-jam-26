using UnityEngine;

public class AddRandomMask : MonoBehaviour
{
    [SerializeField] private Mask mask;
    GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.AddMask(mask);
        }
    }
}
