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

    public int NumberOfPersons = 4;

    public GameObject PersonPrefab;
    public GameObject[] homePositions;

    private List<GameObject> persons;

    private GameObject pictureFrame;

    private States currentState = States.SETUP;

    // Start is called before the first frame update
    void Start()
    {
        InitRound();
    }

    void InitRound()
    {
        persons = new List<GameObject>();
        for(int p = 0; p < NumberOfPersons; p++) {
            GameObject person = GameObject.Instantiate(PersonPrefab);
            person.transform.position = homePositions[p].transform.position;
            // TODO set person values: character image, settings, ...

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
