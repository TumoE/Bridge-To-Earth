#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;

public class ItemDrawer : MonoBehaviour
{
    [SerializeField] private GameObject[] _item;

    [SerializeField] private Camera _camera;

    [SerializeField, Range(1, 500)] private int _density = 1000;
    [SerializeField, Range(0.1f, 15f)] private float _brushSize = 0.1f;

    [SerializeField] private float _y;
    [SerializeField] private bool _canDraw;

    [SerializeField] private LayerMask _mask;

    private Vector3 _pointer;
    private Vector3 _mousePosition;

    private void Start()
    {
        
    }
    private void OnValidate()
    {
        if (_canDraw) 
        {
            SceneView.duringSceneGui += this.OnScene;
        }
        else SceneView.duringSceneGui -= this.OnScene;
    }

    void OnScene(SceneView scene)
    {
        GetMousePoint(scene);
    }

    private void GetMousePoint(SceneView scene)
    {
        if (!_canDraw) return;

        Event e = Event.current;
        _mousePosition = e.mousePosition;

        float ppp = EditorGUIUtility.pixelsPerPoint;
        _mousePosition.y = scene.camera.pixelHeight - _mousePosition.y * ppp;
        _mousePosition.x *= ppp;

        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit , _mask))
        {
            _pointer = hit.point;
        }

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Draw(); 
            _canDraw = false;
            e.Use();
        }
    }

    void Draw()
    {
        if (!_canDraw) return;

        for (int k = 0; k < _density; k++)
        {
            Vector3 origin = _pointer;

            origin.x += Random.Range(-_brushSize, _brushSize);
            origin.z += Random.Range(-_brushSize, _brushSize);

            origin.y = _y;

            int randomObjectIndex = Random.Range(0, _item.Length);
            Instantiate(_item[randomObjectIndex], origin, Quaternion.Euler(0, Random.Range(0,360f), 0), transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (!_canDraw) return;
        Gizmos.DrawWireSphere(_pointer, _brushSize);
    }
}
#endif