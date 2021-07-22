using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] float damageSpeed;
    public float damagePoints;

    public Text damageTex;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        damageTex.text = "-" + damagePoints;
        this.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + damageSpeed * Time.deltaTime,
            this.transform.position.z
        );
    }
}
