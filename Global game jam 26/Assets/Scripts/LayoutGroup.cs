using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LayoutGroup : MonoBehaviour
{
    [SerializeField] private RawImage hotseat;
    [SerializeField] private GameObject maskPrefab;

    private Stack<Mask> masks = new();

    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        masks = gm.masks;
    }


    private void Update()
    {
        if (masks != gm.masks)
        {
            masks = gm.masks;
            hotseat.texture = masks.Peek().sprite.texture;
            hotseat.material = masks.Peek().hotseatMaterial;
        }
    }
}
