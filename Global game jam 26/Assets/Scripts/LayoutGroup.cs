using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LayoutGroup : MonoBehaviour
{
    [SerializeField] private RawImage hotseat;
    [SerializeField] private GameObject maskPrefab;
    private GridLayoutGroup glg;
    private Stack<Mask> otherMasks = new();
    private Mask hotSeatMask;

    GameManager gm;

    /*
    Pseudocode (detailed plan):

    1. On Awake:
       - Cache GridLayoutGroup component and GameManager reference.
       - Initialize local masks reference from GameManager.

    2. On Update:
       - If the GameManager's stack reference changed, update local reference.
       - Update the hotseat texture/material from the top mask if available.
       - Clear existing instantiated mask visuals (children of this transform) to avoid duplicates.
       - Iterate the masks stack to instantiate visuals and count them.
       - Compute spacing.x using a formula that ensures masks fit within a fixed max width (300).
         - Use the GridLayoutGroup.cellSize.x as the per-mask width (fallback to 100 if not set).
         - Let maxWidth = 300.
         - If maskCount > 1:
             spacingX = (maxWidth - (maskCount * cellWidth)) / (maskCount - 1)
           This yields:
             - zero spacing when maskCount * cellWidth == maxWidth (e.g., 3 * 100)
             - positive spacing to distribute extra room when fewer masks
             - negative spacing to compress when more masks than fit
         - If maskCount <= 1: spacingX = 0
       - Apply computed spacing.x back into GridLayoutGroup (preserve spacing.y).

    3. Notes:
       - Use Destroy for runtime child removal.
       - Use Mathf.Clamp where appropriate if you want to limit extremes (not applied here to keep formula direct).
    */

    private void Awake()
    {
        glg = GetComponent<GridLayoutGroup>();
        gm = FindObjectOfType<GameManager>();
        otherMasks = gm.masks;
    }


    private void Update()
    {



        if (otherMasks.Count != gm.masks.Count - 1 && otherMasks.Count != 0)
        {

            otherMasks = gm.masks;
            var top = gm.masks.Peek();
            if (otherMasks.Count > gm.masks.Count)
            {
                otherMasks.Push(hotSeatMask);
                hotSeatMask = null;

            }
            if (top != null && hotSeatMask == null)
            {
                hotseat.texture = top.sprite != null ? top.sprite.texture : null;
                hotseat.material = top.hotseatMaterial;
                hotSeatMask = top;
                otherMasks.Pop();
            }
        }
        if (otherMasks.Count == 0)
        {
            return;
        }
        // Clear existing instantiated visuals so we don't accumulate duplicates.
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
                Destroy(transform.GetChild(i).gameObject);
        }
        int maskAmount = 0;
        foreach (var mask in otherMasks)
        {
            GameObject maskVisual = Instantiate(maskPrefab, Vector3.zero, Quaternion.identity);
            maskVisual.transform.SetParent(transform, false);
            var ri = maskVisual.GetComponent<RawImage>();
            if (ri != null && mask?.sprite != null)
                ri.texture = mask.sprite.texture;
            maskAmount++;
        }

        // Adjust spacing.x so masks fit within a fixed max width (300 units).
        const float maxWidth = 300f;
        float cellWidth = (glg != null && glg.cellSize.x > 0f) ? glg.cellSize.x : 100f;
        float spacingX = (glg != null) ? glg.spacing.x : 0f;

        if (maskAmount > 1)
        {
            float totalCellsWidth = maskAmount * cellWidth;
            // Formula: distribute remaining space (could be negative to compress)
            spacingX = (maxWidth - totalCellsWidth) / (maskAmount - 1);
        }
        else
        {
            spacingX = 0f;
        }

        if (glg != null)
        {
            var s = glg.spacing;
            s.x = spacingX;
            glg.spacing = s;
        }
    }
}
