using UnityEngine;

public class Build : MonoBehaviour {

    private void OnEnable()
    {
        GoldMNGR.Instance.inputField.onValueChanged.AddListener(BuildMNGR.Instance.CheckBuildPrices);
        Reception.Instance.boxCollider.gameObject.layer = 0;
        foreach (Room item in BuildMNGR.Instance.roomsList)
        {
            item.boxCollider.gameObject.layer = 0;
        }
    }

    private void OnDisable()
    {
        GoldMNGR.Instance.inputField.onValueChanged.RemoveAllListeners();
        BuildMNGR.Instance.SizeGroup.gameObject.SetActive(false);
        Reception.Instance.boxCollider.gameObject.layer = 2;
        foreach (Room item in BuildMNGR.Instance.roomsList)
        {
            item.boxCollider.gameObject.layer = 2;
        }
        if (BuildMNGR.Instance.Preview)
        {
            BuildMNGR.Instance.Preview.gameObject.SetActive(false);
        }
    }


}