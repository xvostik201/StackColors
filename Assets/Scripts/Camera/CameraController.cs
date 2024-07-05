using System;
using Camera;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensivity = 1.0f; 
    private bool _isDrag = false;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && !IsPointerOverCollider())
        {
            _isDrag = true;
            _startPosition = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse start position: " + _startPosition);
        }

        if (Input.GetMouseButton(0) && _isDrag)
        {
            _currentPosition = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _currentPosition - _startPosition;
            CameraHolder.Instance.MainCamera.transform.position -= direction * sensivity * Time.deltaTime;
            _startPosition = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDrag = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsPointerOverCollider()
    {
        Vector2 mousePosition = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        return hit.collider != null;
    }
}
