using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 300f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _isMoving;

    private float _len;

    void Start()
    {
        _len = (spriteRenderer.size * transform.localScale.x).x;
    }

    void Update()
    {
        if (_isMoving) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            StartCoroutine(Roll(Vector3.right));
        }
    }
    

    private IEnumerator Roll(Vector3 direction)
    {
        _isMoving = true;
        var remainingAngle = 90f;
        var rotationCenter = transform.position +
                          direction * (_len / 2f) +
                          Vector3.down * (_len / 2f);

        var rotationAxis = Vector3.forward * -direction.x;

        while (remainingAngle > 0)
        {
            var rotationAngle = Mathf.Min(remainingAngle, Time.deltaTime * movementSpeed);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            
            yield return null;
        }

        _isMoving = false;
    }
}