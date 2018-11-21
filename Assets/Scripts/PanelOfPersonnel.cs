using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOfPersonnel : MonoBehaviour {

    public Text Name, RemainedEnergy;
    private BehaviourController SelectedPersonnel;
    private Dropdown DropdownComponent;

    private void Awake()
    {
        DropdownComponent = GetComponentInChildren<Dropdown>();
    }

    private void OnEnable()
    {
        StartCoroutine("DetailedInfo");
    }

    public void UpdateSelectedPersonnel()
    {
        SelectedPersonnel = SelectorForPersonnel.SelectedPersonnel;
        Name.text = SelectedPersonnel.name;
    }

    private IEnumerator DetailedInfo()
    {
        while (gameObject.activeSelf)
        {
            int Energy = Mathf.RoundToInt(SelectedPersonnel.character.CountDown);
            RemainedEnergy.text = "Remained Energy: " + Energy.ToString();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
