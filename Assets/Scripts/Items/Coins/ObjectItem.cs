using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    public Items classOfItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    void OnCollisionEnter(Collision collider)
    {
        switch (classOfItem.itemClass)
        {
            case (ItemClass.Coin):
                if (collider.gameObject.CompareTag("Player"))
                {
                    collider.gameObject.GetComponent<Player>().playerStats.rCoins += classOfItem.Raise;
                    Destroy(this.gameObject);
                }
                break;

            case (ItemClass.PowerUp):
                break;

            case (ItemClass.Bomb):
                if (collider.gameObject.CompareTag("Player"))
                {
                    collider.gameObject.GetComponent<Player>().playerStats.rBombs += classOfItem.Raise;
                    Destroy(this.gameObject);
                }
                break;

            case (ItemClass.Key):
                if (collider.gameObject.CompareTag("Player"))
                {
                    collider.gameObject.GetComponent<Player>().playerStats.rKey += classOfItem.Raise;
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}