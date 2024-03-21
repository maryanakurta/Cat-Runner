using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float newYPosition = Mathf.Clamp(transform.position.y, 0.65f, 0.65f);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        transform.Translate(0, 0, speed * Time.deltaTime);

        if (transform.position.z < -10f)
        {
            GameManager.instance.ScoreUp();
            Destroy(gameObject);
        }
    }
}

