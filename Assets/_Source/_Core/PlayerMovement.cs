using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 300f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Camera mainCamera;

    private float _leftBorder, _rightBorder;
    

    private bool _isMoving;

    private float _len;

    private void Start()
    {
        _len = (spriteRenderer.size * transform.localScale.x).x;
        _leftBorder = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        _rightBorder = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)).x;
    }

    private void Update()
    {
        if (_isMoving) return;

        if (Input.GetKey(KeyCode.LeftArrow) && !(transform.position.x - _len * 1.5 <= _leftBorder))
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(transform.position.x + _len * 1.5 >= _rightBorder))
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