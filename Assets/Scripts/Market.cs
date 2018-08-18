using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

    public Toggle[] Option;
    //public Personel[] AvailableCandidat;
    private Personnel SelectedCandidat;
    private Personnel[] Candidat;
    //public int AvailableCandidatsN;

    private void OnEnable()
    {
        if (Candidat == null)
        {
            Candidat = new Personnel[Option.Length];
            for (int i = 0; i < Option.Length; i++)
            {
                Candidat[i] = new Personnel();
            }
        }
    }


    public void SelectCandidat(int i)
    {
        if (Option[i].isOn)
        {
            SelectedCandidat = Candidat[i];
        }
    }

    public void HireCandidat()
    {
        MonoCharacter MC = CharacterMNGR.Instance.GetMonoCharacter();
        MC.character = SelectedCandidat;
        MC.gameObject.SetActive(true);
    }

}
