using Unity.Cinemachine;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private Transform mainCamera;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = player.GetComponentInChildren<CinemachineCamera>().transform;
    }

    private void Update()
    {
        transform.position = mainCamera.position * 0.8f;
    }
}
