using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personel : Character {

    public Vector3 TargetVector
    {
        get; private set;
    }
    public Reception reception
    {
        get; private set;
    }

    public bool Wait;
    public float AnimationTime;

    private IPersBehaviour Behaviour;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        TargetVector = transform.position;
    }


    // Use this for initialization
    void Start () {        
        StartCoroutine("Behave");
    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(TargetVector);
    }

    private IEnumerator Behave()
    {
        while (true)
        {
            if (Wait)
            {
                Wait = false;
                yield return new WaitForSeconds(AnimationTime);
            }
            else
            {
                AnimatorComponent.SetInteger("AnimationID", 0);
                if (Behaviour != null)
                {
                    TargetVector = Behaviour.RoomBehaviour();
                }
                yield return new WaitUntil(() => hasReachedTarget(TargetVector));
            }
        }
    }
    /*
    public void SelectJob(string JobName)
    {
        switch (JobName)
        {
            case "Waiter":
                //Job = new Waiter();                
                break;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        GameObject another = other.transform.parent.gameObject;
        if (other.tag == "Room")
        {
            if (another.name.Contains("Bar"))
            {
                //Behaviour.SwitchRoom();
                Bar RoomType = another.GetComponent<Bar>();// Debug.Log("BarTime");
                Behaviour = new PersBar(this, RoomType);
                TargetVector = transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
        }
    }
}
