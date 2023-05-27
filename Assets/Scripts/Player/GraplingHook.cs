using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Camera _camera;
    private LineRenderer _lineRenderer;
    private DistanceJoint2D _distanceJoint;
    [SerializeField] private LayerMask _hookHandle;

    private void Start()
    {
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _distanceJoint = GetComponent<DistanceJoint2D>();
        _distanceJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2 mousePos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("HookHandle"))
                {
                    _distanceJoint.connectedAnchor = mousePos;
                    _lineRenderer.SetPosition(0, mousePos);
                    _lineRenderer.SetPosition(1, transform.position);
                    _lineRenderer.enabled = true;
                    _distanceJoint.enabled = true;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            _lineRenderer.enabled = false;
            _distanceJoint.enabled = false;
        }

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

}
