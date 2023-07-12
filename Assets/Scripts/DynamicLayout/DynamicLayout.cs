using UnityEngine;

public class DynamicLayout : MonoBehaviour
{
    [SerializeField] private Transform _landscapeLayoutTransform;
    [SerializeField] private Transform _portraitLayoutTransform;
    private ScreenOrientation _orientation;

    private void Awake()
    {
        _orientation = Screen.orientation;
    }

    private void Update()
    {
        if(_orientation != Screen.orientation)
        {
            ChangeOrientation();
        }
    }

    private void ChangeOrientation()
    {
        if (_orientation == ScreenOrientation.Portrait || _orientation == ScreenOrientation.PortraitUpsideDown)
            MoveToLandscape();
        else
            MoveToPortrait();

        _orientation = Screen.orientation;
    }

    private void MoveToPortrait()
    {
        foreach (Transform transfrom in _landscapeLayoutTransform)
            transform.parent = _portraitLayoutTransform;
    }

    private void MoveToLandscape()
    {
        foreach (Transform transfrom in _portraitLayoutTransform)
            transform.parent = _landscapeLayoutTransform;
    }
}
