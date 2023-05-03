using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapControls : MonoBehaviour,
    IPointerClickHandler,
    IDragHandler,
    IPointerExitHandler,
    IPointerEnterHandler,
    IScrollHandler,
    IPointerMoveHandler,
    IEndDragHandler
{
    public Camera WorldMapCamera;
    public Camera MainCamera;
    private RectTransform rectTransform;
    public float mapSize = 160f;
    public float worldMapScrollSensitivity = 0.01f;
    public float worldMapScrollFollowSensitivity = 0.01f;
    public float worldMapScrollFollowFactor = 1.5f;
    public float maxScrollMoveDelta = 10f;
    public float worldMapDragSensitivity = 0.003f;
    public float minZoomSize = 0f;
    public float maxZoomSize = 160f;
    public float dragEndForceFactor = 10f;


    public WorldMapObject _currentMapObject = null;

    LayerMask layer;

    public WorldMapTooltip tooltip;
    private RectTransform tooltipRect;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        tooltipRect = tooltip.GetComponent<RectTransform>();
        layer = LayerMask.GetMask("Map Objects");
    }

    void OnEnable()
    {
        WorldMapCamera.gameObject.SetActive(true);

    }

    void OnDisable()
    {
        WorldMapCamera.gameObject.SetActive(false);
    }



    public void OnPointerClick(PointerEventData eventData) // 3
    {
        
        if (Physics.Raycast(GetRayToMap(eventData), out RaycastHit hit, Mathf.Infinity, layer))
        {
            WorldMapObject mapObject = hit.collider.gameObject.GetComponent<WorldMapObject>();
            if (mapObject)
            {
                mapObject.Select();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Todo: Use this with rigidbody on World Camera to make camera follow drag direction

    }

    public void OnPointerMove(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, MainCamera, out Vector2 localClick);
        localClick.x = (rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (rectTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / rectTransform.rect.size.x, localClick.y / rectTransform.rect.size.y);

        Ray ray = WorldMapCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            
            WorldMapObject mapObject = hit.collider.gameObject.GetComponent<WorldMapObject>();
            if (mapObject)
            {
                // We are hitting a mapObject
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, MainCamera, out Vector2 localClick);
                print(mapObject.description);
                tooltip.Show(mapObject.description);

                tooltipRect.anchoredPosition = new Vector3(localClick.x,localClick.y,1);

                if (!mapObject == _currentMapObject)
                {
                    _currentMapObject = mapObject;
                    mapObject.Enter();
                }
            }
        }
        else
        {
            if (!tooltip.IsHidden())
            {
                tooltip.Hide();
            }

            if (_currentMapObject)
            {
                _currentMapObject.Exit();
                _currentMapObject = null;
            }
        }

    }

    void SetCurrentMapObject()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

        float newX, newY, newZ;

        newX = WorldMapCamera.transform.position.x - (eventData.delta.x * worldMapDragSensitivity * WorldMapCamera.orthographicSize);
        newY = WorldMapCamera.transform.position.y;
        newZ = WorldMapCamera.transform.position.z - (eventData.delta.y * worldMapDragSensitivity * WorldMapCamera.orthographicSize);

        Vector3 pos = new Vector3(newX, newY, newZ);

        WorldMapCamera.transform.position = ClampViewPosition(pos);

        print("OnDrag position");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnScroll(PointerEventData eventData)
    {
        float newSize = WorldMapCamera.orthographicSize - (eventData.scrollDelta.y * WorldMapCamera.orthographicSize  * worldMapScrollSensitivity);
        newSize = Mathf.Clamp(newSize, minZoomSize, mapSize);
        WorldMapCamera.orthographicSize = newSize;

        Vector3 cameraNewPosition;

        if (eventData.scrollDelta.y >= 0 )
        {
            // Zooming In
            print("Delta Positive");
            Ray ray = GetRayToMap(eventData);
            Vector3 towardsPos = WorldMapCamera.transform.position - ray.origin;
            towardsPos.y = 0;
            //Todo: Instead of Sqrt do Power To 1/x, where x is adjustable
            //towardsPos = towardsPos * Mathf.Sqrt(WorldMapCamera.orthographicSize) * worldMapScrollFollowSensitivity;
            towardsPos = towardsPos * Mathf.Pow(WorldMapCamera.orthographicSize,1/worldMapScrollFollowFactor) * worldMapDragSensitivity;
            cameraNewPosition = ClampViewPosition(WorldMapCamera.transform.position - towardsPos);
        }
        else
        {
            // Zooming Out
            print("Delta negative");
            cameraNewPosition = ClampViewPosition(WorldMapCamera.transform.position);
        }


        WorldMapCamera.transform.position = cameraNewPosition;

    }

    #region Helpers

    Ray GetRayToMap(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, MainCamera, out Vector2 localClick);
        //My RawImage is 700x700 and the click coordinates are in range (-350,350) so I transform it to (0,700) to then normalize
        localClick.x = (rectTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (rectTransform.rect.yMin * -1) - (localClick.y * -1);

        //I normalize the click coordinates so I get the viewport point to cast a Ray
        Vector2 viewportClick = new Vector2(localClick.x / rectTransform.rect.size.x, localClick.y / rectTransform.rect.size.y);


        //I cast the ray from the camera which rends the texture
        Ray ray = WorldMapCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        return ray;
    }

    Vector3 ClampViewPosition(Vector3 originalPos)
    {
        float newX = originalPos.x;
        float newZ = originalPos.z;

        //newX = Mathf.Clamp(newX, -mapSize + WorldMapCamera.orthographicSize, mapSize - WorldMapCamera.orthographicSize);
        //newZ = Mathf.Clamp(newZ, -mapSize + WorldMapCamera.orthographicSize, mapSize - WorldMapCamera.orthographicSize);

        Vector3 newPos = new Vector3(newX, originalPos.y, newZ);

        return newPos;
    }

    #endregion
}
