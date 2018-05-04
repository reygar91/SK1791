using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

    public Toggle[] Option;
    public Personel[] AvailableCandidat;
    private Personel SelectedCandidat;


    public void SelectCandidat()
    {
        for (int i =0; i < Option.Length; i++)
        {
            if (Option[i].isOn)
            {
                SelectedCandidat = AvailableCandidat[i];
            }
        }
    }

    public void HireCandidat()
    {
        if (SelectedCandidat != null)
        {
            SelectedCandidat.gameObject.SetActive(true);
        }
        else Debug.Log("NoOne Selected");        
    }

}
