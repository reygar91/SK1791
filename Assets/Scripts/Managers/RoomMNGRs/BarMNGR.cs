using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMNGR : MonoBehaviour {

    public static BarMNGR Instance;

    public List<Transform> AvailableSeats;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
