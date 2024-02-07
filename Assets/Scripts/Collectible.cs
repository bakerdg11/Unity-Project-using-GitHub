using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    //sorts what kind of collectible they each are. Enumerative value. Creating an integer and giving specific values
    public enum PickupType
    {
        Powerup,
        Score,
        Life,
    }



    [SerializeField] PickupType currentCollectible;
    [SerializeField] float timeToDestroy = 0;

    
    public PickupType currentCollectibe;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            switch (currentCollectible)
            {
                case PickupType.Powerup:
                    pc.StartJumpForceChange();
                    break;
                case PickupType.Score:
                    break;
                case PickupType.Life:
                    break;
            }
            Destroy(gameObject);
        }
            
    }



}
