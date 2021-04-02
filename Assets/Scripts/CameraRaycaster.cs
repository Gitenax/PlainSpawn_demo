using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    #pragma warning disable CS0649
                     private Camera    _camera;
    [SerializeField] private LayerMask _castMask;
    [SerializeField] private Spawner   _spawner;
    #pragma warning restore CS0649
    
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Raycast();
    }

    private void Raycast()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _castMask))
        {
            VerifyObject(rayHit);
        }
    }

    private void VerifyObject(RaycastHit obj)
    {
        // Если это объект
        if (obj.collider.TryGetComponent(out PlainObject plainObject))
        {
            plainObject.IncreaseClicks();
            return;
        }

        SpawnAndAjustObject(obj.point);
    }

    private void SpawnAndAjustObject(Vector3 position)
    {
        var obj = _spawner.SpawnObject(position);
        obj.transform.position += new Vector3(0, 0, transform.localScale.y);
    }
    
}
