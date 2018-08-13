using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

    public Toggle[] Option;
    //public Personel[] AvailableCandidat;
    //private Personel SelectedCandidat;
    private Character[] Candidat;
    public int AvailableCandidatsN;

    private void OnEnable()
    {
        if (Candidat == null)
        {
            Candidat = new Character[AvailableCandidatsN];
            for (int i = 0; i < AvailableCandidatsN; i++)
            {
                //Candidat[i] = CharacterMNGR.
            }
        }
    }


    public void SelectCandidat(int i)
    {
        if (Option[i].isOn)
        {
            //SelectedCandidat = AvailableCandidat[i];
        }
    }

    public void HireCandidat()
    {
        //if (SelectedCandidat != null)
        //{
        //    SelectedCandidat.gameObject.SetActive(true);
        //}
        //else Debug.Log("NoOne Selected");        
    }

}
