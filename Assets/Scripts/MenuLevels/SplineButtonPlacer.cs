using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class SplineButtonPlacer : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private RectTransform _transformParent;

    [Space]
    [SerializeField] private int _buttonsCount = 20;
    [SerializeField] private bool _placeOnAllKnots = true;

    private bool _initialized = false;

    [Button]
    public void Init()
    {
        if (_initialized)
            return;

        if (_placeOnAllKnots)
            PlaceButtonsOnKnots();
        else
            PlaceButtonsUniformly(_buttonsCount);

        _initialized = true;
    }

    private void PlaceButtonsUniformly(int count)
    {
        if (_splineContainer == null) return;

        for (int i = 0; i < count; i++)
        {
            float t = i / (float)(count - 1);

            Vector3 position = _splineContainer.EvaluatePosition(t);
            Vector3 worldPosition = _splineContainer.transform.TransformPoint(position);

            CreateButtonAtPosition(worldPosition, i);
        }
    }

    private void PlaceButtonsOnKnots()
    {
        if (_splineContainer == null) return;

        Spline spline = _splineContainer.Spline;

        for (int i = 0; i < spline.Count; i++)
        {
            BezierKnot bezierKnot = spline[i];
            Vector3 point = _splineContainer.transform.TransformPoint(bezierKnot.Position);

            CreateButtonAtPosition(point, i);
        }
    }

    private void CreateButtonAtPosition(Vector3 worldPosition, int index)
    {
        GameObject button = Instantiate(_buttonPrefab, _transformParent);
        button.transform.position = worldPosition;
        button.name = $"Level {index + 1}";
        button.GetComponent<LevelButton>().Init(button.name);

        // if (UnityEngine.Camera.main != null)
        // {
        //     Vector2 screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(worldPosition);
        //     RectTransform rectTransform = button.GetComponent<RectTransform>();
        //
        //     if (rectTransform != null)
        //         rectTransform.position = screenPoint;
        // }
    }
}