using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    //CubeDetail CDet;
    public const string path = "pickupdatabase";

    //Inheritited Classes
    FirstQuest oneQuest = new FirstQuest();

    //Public Variables
    public Collider questItem, itemLocation;
    public GameObject PickUpPanel, laidPlank;
    public Text Title, textBody;

    //Private Variables
    protected float Distance;

    void Start()
    {
        //ItemContainer i = ItemContainer.Load(path);
        Distance = Vector3.Distance(questItem.transform.position, itemLocation.transform.position);
        oneQuest.QuestStart();
    }

    void Update()
    {
        Distance = Vector3.Distance(questItem.transform.position, itemLocation.transform.position);
        //Debug.Log(Distance);
    }

    //Distance tracking works, triggers the end of the FirstQuest
    private void FixedUpdate()
    {
        if (Distance <= 20)
        {
            laidPlank.SetActive(true);
            oneQuest.QuestEnd();
        }
    }

    public void CavePickup()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        ItemContainer i = ItemContainer.Load(path);
        //Works perfectly, just need to add the Ui elements and so
        foreach (Item PickUp in i.PickUps) {

            if (other.gameObject.name == "Saint Edwin of Northumbria's Bracer")
            {
                print("Worked");
                if (PickUp.name == other.gameObject.name)
            {
                print(PickUp.name);
                print(PickUp.Effect);
                Title.text = PickUp.name;
                textBody.text = PickUp.Effect;
                PickUpPanel.SetActive(true);
                Destroy(other.gameObject);
            }
            }
            if (other.gameObject.name == "Saint Thomas Becket's Hairshirt")
            {
                print("Worked 2");
                if (PickUp.name == other.gameObject.name)
                {
                    print(PickUp.name);
                    print(PickUp.Effect);
                    Title.text = PickUp.name;
                    textBody.text = PickUp.Effect;
                    PickUpPanel.SetActive(true);
                    Destroy(other.gameObject);
                }
            }
        }
            
    }
}


//Collision tester for database input, working

//void OnCollisionEnter(Collision collision)
//{
//    ItemContainer i = ItemContainer.Load(path);
//    foreach (Item PickUp in i.PickUps)
//    {
//        if (collision.gameObject.name == "The Crown")
//        {
//            print("Dog");
//            if (PickUp.name == collision.gameObject.name)
//            {
//                print(PickUp.name);
//                print(PickUp.Effect);
//                oneQuest.QuestEnd();
//            }
//        }
//    }
//}

