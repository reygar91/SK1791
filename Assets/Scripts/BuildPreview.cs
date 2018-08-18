using UnityEngine;

public class BuildPreview : MonoBehaviour {

    void LateUpdate()
    {
        BuildMNGR.Instance.SelectBuildPosition();
        if (Input.GetMouseButtonDown(1))
        {
            BuildMNGR.Instance.ResetPreview();
        }
    }
    
}


