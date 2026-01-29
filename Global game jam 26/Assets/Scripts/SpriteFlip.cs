using UnityEngine;
using Pathfinding;

public class SpriteFlip : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    public void Update()
    {
        if (aiPath.desiredVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
