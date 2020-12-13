using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance = null;
    public static GameController instance { get { return _instance; } private set {} }

    class PhotoScore {
        public Texture2D photo;
        public float score;

        public PhotoScore(Texture2D photo, float score)
        {
            this.photo = photo;
            this.score = score;
        }
    }

    private enum States {
        // INTRO,
        // TITLE,
        SETUP,
        GAME,
        END
    }

    // manually-assigned objects
    public GameObject[] PersonPrefabs;

    public GameObject[] homePositions;
    
    public float PointsForHappy = 2;
    public float PointsForBored = 1;
    public float PointsForWalkingInFrame = 0.5f;
    public float PointsForShopper = -1;

    // detected/generated objects/groups
    private List<GameObject> persons;

    private Transform familyTransform;

    private GameObject pictureFrame;
    private PhotoCounter photoCounter;
    private Timer timer;

    private States currentState = States.SETUP;

    // Scoring
    private List<PhotoScore> scores;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        familyTransform = GameObject.Find("Family").transform;
        photoCounter = GameObject.Find("PhotoIconBar").GetComponent<PhotoCounter>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        EnterState(currentState);
    }

    void Reset()
    {
        if(persons != null) {
            foreach(GameObject person in persons) {
                GameObject.DestroyImmediate(person);
            }
            persons.Clear();
            persons = null;
        }

        timer.Reset();
        photoCounter.ShowIcons(3);
        scores = new List<PhotoScore>();

        // TODO more?
    }

    void InitRound()
    {
        Reset();

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

        EnterState(States.GAME);
    }

    public void SaveScreenshot(Texture2D screenshot)
    {
        float score = CalculateScore();

        scores.Add(new PhotoScore(screenshot, score));
        Debug.Log("Saved score "+scores.Count+": "+score);

        photoCounter.ShowIcons(3 - scores.Count);

        if(scores.Count >= 3) {
            EnterState(States.END);
        }
    }

    private PhotoScore GetBestScore()
    {
        PhotoScore best = new PhotoScore(null, 0);
        foreach(PhotoScore score in scores) {
            if(score.score > best.score) {
                best = score;
            }
        }

        return best;
    }

    private float CalculateScore()
    {
        float score = 0;

        // Scoring:
        // 2 points for every HAPPY person in the picture
        // 1 point for every BORED person in the picture
        // -1 point for every shopper in the picture
        //
        // TODO optional: fractional points for just-still-in-frame WALKING persons, depending on distance from home
        foreach(GameObject person in persons) {
            switch(person.GetComponent<PersonController>().playerState) {
                case PersonController.States.HAPPY:
                    score += PointsForHappy;
                    break;
                case PersonController.States.BORED:
                    score += PointsForBored;
                    break;
                case PersonController.States.MOVING:
                    // if(inFrame) { // TODO check if in frame
                    //     score += PointsForWalkingInFrame;
                    // }
                    break;
            }
        }

        // TODO shoppers - match overlap with pictureFrame

        return score;
    }

    private void EnterState(States newState)
    {
        Debug.Log("ENTER STATE: "+newState);

        switch(newState) {
            // case States.INTRO:
            //     break;
            // case States.TITLE:
            //     break;
            case States.SETUP:
                InitRound();
                break;
            case States.GAME:
                // TODO player sound "Cheeeeese"?
                break;
            case States.END:
                Debug.Log("END! Best score: "+GetBestScore().score);
                // TODO show screenshot
                GameObject.DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene("EndScreen");
                break;
        }

        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        switch(currentState) {
            // case States.INTRO:
            //     break;
            // case States.TITLE:
            //     break;
            case States.SETUP:
                break;
            case States.GAME:
                break;
            case States.END:
                // TODO on click start new round?
                break;
        }
    }
}
