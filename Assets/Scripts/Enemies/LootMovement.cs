using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(new Vector2(0.3f, 1f) * GameManager.ShipBoosterVelocity, ForceMode2D.Impulse);
    }
}
