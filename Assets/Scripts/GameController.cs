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

    // TEST - TODO: make list or lookup - or save prefabs for each person?
    public Material girlMaterial;

    public int NumberOfPersons = 4;

    public GameObject PersonPrefab;
    public GameObject[] homePositions;

    private List<GameObject> persons;

    private Transform familyTransform;
    private GameObject pictureFrame;

    private States currentState = States.SETUP;

    // Start is called before the first frame update
    void Start()
    {
        familyTransform = transform.Find("Family");

        InitRound();
    }

    void InitRound()
    {
        persons = new List<GameObject>();
        for(int p = 0; p < NumberOfPersons; p++) {
            GameObject person = GameObject.Instantiate(PersonPrefab);
            person.transform.position = homePositions[p].transform.position;
            person.transform.parent = familyTransform;

            // TODO set person values: character image, cooldown/target settings, ...

            // TEST
            person.GetComponent<Renderer>().material = girlMaterial;

            persons.Add(person);
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
