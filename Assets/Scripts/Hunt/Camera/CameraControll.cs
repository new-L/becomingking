using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float followAhead;
    [SerializeField] private float smoothing;

    private Vector3 targetPosition;
    private Camera camera;
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().gameObject;
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        CameraZoom();
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, transform.position.z);
        if(!target.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
    private void CameraZoom()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (camera.orthographicSize < 6.5f) camera.orthographicSize += 4f * Time.deltaTime;
        }
        else
        {
            if (camera.orthographicSize > 5f) camera.orthographicSize -= 4f * Time.deltaTime;
            else if (camera.orthographicSize < 5f) camera.orthographicSize = 5f;
        }
    }
}
