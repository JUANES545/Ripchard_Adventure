using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    private PlayerController thePlayer;

    public Vector2 facingDirection = Vector2.zero;
    public string placeName;
    void Awake()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        if (!thePlayer.nextPlaceName.Equals(placeName))
        {
            return;
        }
        thePlayer.transform.position = this.transform.position;
        thePlayer.lastMovement = facingDirection;
    }
}
