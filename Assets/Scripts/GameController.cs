using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private enum States {
        INTRO,
        TITLE,
        SETUP,
        GAME,
        SNAP,
        END
    }


    public GameObject[] PersonPrefabs;

    public GameObject[] homePositions;
    

    private List<GameObject> persons;

    private Transform familyTransform;
    private GameObject pictureFrame;

    private States currentState = States.SETUP;

    // Start is called before the first frame update
    void Start()
    {
        familyTransform = GameObject.Find("Family").transform;

        InitRound();
    }

    void InitRound()
    {
        persons = new List<GameObject>();
        for(int p = 0; p < PersonPrefabs.Length; p++) {
            GameObject person = GameObject.Instantiate(PersonPrefabs[p]);
            person.transform.parent = familyTransform;

            person.GetComponent<PersonController>().SetHomePosition(homePositions[p]);
            person.transform.position = homePositions[p].transform.position;

            // TODO? (if not defined in prefab) set person values: character image, cooldown/target settings, ...

            persons.Add(person);
        }

        // hide home positions, will only be visible while dragging person
        foreach(GameObject homePosition in homePositions) {
            homePosition.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }

        pictureFrame = GameObject.FindGameObjectWithTag("PictureFrame");

        currentState = States.GAME;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        switch(currentState) {
            case States.INTRO:
                break;
            case States.TITLE:
                break;
            case States.SETUP:
                break;
            case States.GAME:
                break;
            case States.SNAP:
                break;
            case States.END:
                break;
        }
    }
}
