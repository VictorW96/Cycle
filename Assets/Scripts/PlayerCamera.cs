using Mirror;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float scrollSpeed = 4f;

    [SerializeField] private float minSize = 2;
    [SerializeField] private float maxSize = 8;

    private static Vector3 gridCellSize;

    private Vector3 worldMiddle;
    private Vector2 panLimit;

    private Camera cameraComponent;

    void Update()
    {
        moveCamera();
    }

    private void Start()
    {
        World world = World.Instance;
        int width = world.worldParameters.width;
        int height = world.worldParameters.height;
        gridCellSize = world.gridCellSize;
        cameraComponent = GetComponent<Camera>();

        panLimit = new Vector2(width* gridCellSize.x, height * gridCellSize.y);

        float xCor = (width * gridCellSize[0]) / 2;
        float yCor = (height * gridCellSize[1]) / 2;
        worldMiddle = new Vector3(xCor, yCor, -1);
        transform.position = worldMiddle;
    }

    private void moveCamera()
    {
            Vector3 pos = transform.position;

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.y += panSpeed * Time.deltaTime;

            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.y -= panSpeed * Time.deltaTime;

            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;

            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;

            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            cameraComponent.orthographicSize -= scroll * scrollSpeed * 300f * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, -1, panLimit.x);
            pos.y = Mathf.Clamp(pos.y, -1, panLimit.y);
            cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, minSize, maxSize);

            transform.position = pos;

    }
}
