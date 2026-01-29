using UnityEngine;
using UnityEngine.Tilemaps;

public class CheeseDeathHit : MonoBehaviour
{
    private PlayerHealth ph;
    private Collider2D myCollider;

    [Header("Settings")]
    public string floorMapName = "GroundLayer";

    private void Start()
    {
        ph = FindObjectOfType<PlayerHealth>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ph != null) ph.TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Tilemap tilemap = other.GetComponent<Tilemap>();

        if (tilemap != null && other.gameObject.name != floorMapName)
        {
            // Get the bounds of the Cheese's collider
            Bounds bounds = myCollider.bounds;

            // Convert the min and max bounds to Tilemap cell coordinates
            Vector3Int minCell = tilemap.WorldToCell(bounds.min);
            Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

            // Loop through all cells covered by the Cheese's size
            for (int x = minCell.x; x <= maxCell.x; x++)
            {
                for (int y = minCell.y; y <= maxCell.y; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);

                    if (tilemap.HasTile(cellPos))
                    {
                        tilemap.SetTile(cellPos, null);
                        Debug.Log("Area Destroyed: " + cellPos);
                    }
                }
            }
        }
    }
}