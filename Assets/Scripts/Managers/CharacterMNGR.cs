using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMNGR : MonoBehaviour {

    protected CharacterMNGR() { }
    //[SerializeField] private BehaviourController CustomerPrefab, PersonelPrefab;
    private CustomerFactory customerFactory;
    //[SerializeField] private CustomerFactory customerFactory;


    //public List<BehaviourController> CustomersPool = new List<BehaviourController>();
    //public List<BehaviourController> PersonnelPool = new List<BehaviourController>();
    public List<BehaviourController> ActiveCharacters = new List<BehaviourController>();

    public static CharacterMNGR Instance;

    private void Awake()
    {
        Instance = this;
        customerFactory = GetComponent<CustomerFactory>();
    }

    private void Start()
    {
        StartCoroutine("SpawnCustomer"); //later this should start after some dialogue
    }

    private void Update()
    {
        //.ToArray returns Array copy of List
        foreach (BehaviourController BC in ActiveCharacters.ToArray())
        {
            BC.Tick();
        }
    }

    private IEnumerator SpawnCustomer()
    {
        int CountDown = 0;
        while (true)
        {
            if (TimeMNGR.Instance.Pause.isOn)
            {
                yield return new WaitWhile(() => TimeMNGR.Instance.Pause.isOn);
            }
            else
            {
                CountDown -= InputMNGR.Instance.Reputation;
                if (CountDown <= 0)
                {
                    customerFactory.SpawnCustomer(new Customer());
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
            }
        }
    }

    

    //public BehaviourController SpawnCharacter(string CharacterType)
    //{
    //    BehaviourController BC = GetController();
    //    BC.character = CreateCharacter(CharacterType);
    //    BC.gameObject.SetActive(true);
    //    return BC;
    //}

    //public BehaviourController GetController()
    //{
    //    BehaviourController BC = null;
    //    if (CharactersPool.Count == 0)
    //        BC = Instantiate(CustomerPrefab, CharacterContainer.transform);
    //    else
    //        BC = CharactersPool[0];
    //    return BC;
    //}


    //public Character CreateCharacter(string CharacterType)
    //{
    //    Character character;
    //    switch (CharacterType)
    //    {
    //        case "MainCharacter":
    //            character = new MainCharacter();
    //            break;
    //        case "Customer":
    //            character = new Customer();
    //            break;
    //        case "Personnel":
    //            character = new Personnel();
    //            break;
    //        default:
    //            character = null;
    //            break;
    //    }
    //    return character;
    //}

}
