using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Physics;
using UnityEngine.UI;


public class MapFragment
{
    public GameObject Container;
    public bool FragmentExist;
}
public class Map
{
    public Vector3 Position;
    public string NameObstacle;
    public Map(Vector3 pPosition, string pNameObstacle)
    {
        Position = pPosition;
        NameObstacle = pNameObstacle;
    }
}

public class mainScript : MonoBehaviour
{
    
    public float speed = 0.4f;
    public GameObject Player;
    public GameObject GroundPrefab;
    public GameObject ObstaclePrefab;
   // public GameObject GroundObjOnScene1;
   // public GameObject GroundObjOnScene2;
   // public GameObject GameOverText;
    public GameObject Mony;
    public GameObject GroundS1;
    public GameObject Shark;
    public GameObject DeathPanel;
   
    //------------------------

    void Start()
    {
        Mony = Resources.Load("Vodrosl") as GameObject;
        GroundPrefab = Resources.Load("Ground") as GameObject;
        GroundS1 = Resources.Load("GroundS1") as GameObject;
        ObstaclePrefab = Resources.Load("Obstacle") as GameObject;
        Shark = Resources.Load("shark") as GameObject;
        Player = GameObject.Find("Player");

        //  GameOverText = GameObject.Find("GameOverText");
        // GameOverText.GetComponent<CanvasRenderer>().SetAlpha(0);
        
    }
    public bool isExistMapFragments = false;
     public MapFragment[] AllMapFragments = new MapFragment[6];


    public List<Map> GenerateRandomMapFragment()
    {
        System.Random rnd = new System.Random();

        List<Map> result = new List<Map>();
        for (int i = 0; i < rnd.Next(0, 500); i++)
            result.Add(new Map(new Vector3(rnd.Next(-200, -9), -1.5f, rnd.Next(1, 100)), "Mony"));
        for (int i = 0; i < rnd.Next(0, 500); i++)
            result.Add(new Map(new Vector3(rnd.Next(9, 200), -1.5f, rnd.Next(1, 100)), "Mony"));
        for (int i = 0; i < rnd.Next(0, 2); i++)
            result.Add(new Map(new Vector3(rnd.Next(-9,0), 1.5f, rnd.Next(19, 20)), "LocObstacle"));
        for (int i = 0; i < rnd.Next(0, 2); i++)

            result.Add(new Map(new Vector3(rnd.Next(-1, 7), 0, rnd.Next(85, 90)), "GroundS1"));
        for (int i = 0; i < rnd.Next(0, 2); i++)
            result.Add(new Map(new Vector3(rnd.Next(-60, 60), rnd.Next(0, 3), rnd.Next(10, 90)), "shark"));

        return result;
    }

    public List<Map> ReadMapFragmentFromFile(int index)
    {
        return new List<Map>();
    }

    public MapFragment CreateNewMapFragment(float Zpos)
    {
        MapFragment nFrag = new MapFragment();
        nFrag.FragmentExist = true;
        nFrag.Container = new GameObject();
        nFrag.Container.name = "Container1";
        nFrag.Container.transform.position = new Vector3(0, 0, Zpos);
        GameObject locGround = MonoBehaviour.Instantiate(GroundPrefab, new Vector3(0, 0, 50), Quaternion.identity) as GameObject;
        locGround.name = "Ground1";
        locGround.transform.SetParent(nFrag.Container.transform, false);

        List<Map> newMapObj = GenerateRandomMapFragment();

        for (int i = 0; i < newMapObj.Count; i++)
        {
            if (newMapObj[i].NameObstacle == "Mony")
            {
                GameObject LocChrOldPrefab1 = MonoBehaviour.Instantiate(Mony, newMapObj[i].Position, Quaternion.identity) as GameObject;
                LocChrOldPrefab1.transform.SetParent(nFrag.Container.transform, false);
            }
            if (newMapObj[i].NameObstacle == "GroundS1")
            {
                GameObject LocChrOldPrefab2 = MonoBehaviour.Instantiate(GroundS1, newMapObj[i].Position, Quaternion.identity) as GameObject;
                LocChrOldPrefab2.transform.SetParent(nFrag.Container.transform, false);
            }

            if (newMapObj[i].NameObstacle == "shark")
            {
                GameObject LocChrOldPrefab3 = MonoBehaviour.Instantiate(Shark, newMapObj[i].Position, Quaternion.identity) as GameObject;
                LocChrOldPrefab3.transform.SetParent(nFrag.Container.transform, false);
            }
            if (newMapObj[i].NameObstacle == "LocObstacle")
            {
                GameObject LocChrOldPrefab4 = MonoBehaviour.Instantiate(ObstaclePrefab, newMapObj[i].Position, Quaternion.identity) as GameObject;
                LocChrOldPrefab4.transform.SetParent(nFrag.Container.transform, false);
            }
            

        }

        return nFrag;
    }

    public void RunInfinitePlaneV2()
    {
        // условие начальной установки объектов
        if (!isExistMapFragments)
        {
            isExistMapFragments = true;
            for (int i = 0; i < AllMapFragments.Length; i++)
                AllMapFragments[i] = CreateNewMapFragment(100 * i);
        }

        // условие движения фрагментов
        for (int i = 0; i < AllMapFragments.Length; i++)
        {
            if (AllMapFragments[i].FragmentExist)
                AllMapFragments[i].Container.transform.position += new Vector3(0, 0, speed * -1);
        }

        // условие удаления фрагментов
        for (int i = 0; i < AllMapFragments.Length; i++)
        {
            if (AllMapFragments[i].FragmentExist && AllMapFragments[i].Container.transform.position.z < -200)
            {
                Destroy(AllMapFragments[i].Container);
                AllMapFragments[i].FragmentExist = false;
            }
        }

        // условие установки фрагментов
        for (int i = 0; i < AllMapFragments.Length; i++)
        {
            if (!AllMapFragments[i].FragmentExist)
            {
                float maxz = 0;
                for (int j = 0; j < AllMapFragments.Length; j++)
                {
                    float lpos = AllMapFragments[j].Container.transform.position.z;
                    if (lpos > maxz)
                        maxz = lpos;
                }

                AllMapFragments[i] = CreateNewMapFragment(maxz + 100.0f);
            }
        }
    }



    //Coroutine работает паралельно FixedUpdate 
    public bool IsGameOver = false;
    IEnumerator GameOver()
    {
        PausePanel.SetActive(false);
        DeathPanel.SetActive(true);
        IsGameOver = true;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        speed = 0;

    
        yield break;
    }
    void FixedUpdate()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        bool forwardContact 
            = Raycast(Player.transform.position, Player.transform.TransformDirection(Vector3.forward), out hit1, 1.8f);

      //  Debug.DrawRay(Player.transform.position, Player.transform.TransformDirection(Vector3.forward) * 1.8f, Color.blue);
      
        bool forwardContact2
            = Raycast(Player.transform.position, Player.transform.TransformDirection(Vector3.back), out hit2, 1.5f);
            bool forwardContact3
             = Raycast(Player.transform.position, Player.transform.TransformDirection(Vector3.up), out hit3, 1.5f);
        if (forwardContact && (hit1.transform.gameObject.tag == "Ground" || hit1.transform.gameObject.tag == "Obstacle") && !IsGameOver)
        {
           StartCoroutine(GameOver());
        }

        if (forwardContact2 && hit2.transform.gameObject.tag == "Ground" && !IsGameOver)
        {
           StartCoroutine(GameOver());
        }
        if (forwardContact3 && hit3.transform.gameObject.tag == "Ground" && !IsGameOver)
        {
            StartCoroutine(GameOver());
        }
        Player.GetComponent<Rigidbody>().AddForce(0,0,0.00001f);

        RunInfinitePlaneV2();
       
        if (!IsGameOver)
        {
            if (Input.GetKey("w"))
            {
                Player.GetComponent<Rigidbody>().AddTorque(Vector3.right * 20.1f);
            }
          
            if (Input.GetKey("s"))
            {
                Player.GetComponent<Rigidbody>().AddTorque(Vector3.right * -20.1f);
            }

            if (Input.GetKey("d"))
            {
                Player.transform.position += new Vector3(0.1f, 0, 0);
            }

            if (Input.GetKey("a"))
            {
                Player.transform.position += new Vector3(-0.1f, 0, 0);
            }
        }
    
    }
    public GameObject PausePanel;
    public float timer;
    public bool ispuse;
    public bool guipuse;
    public Slider slider;
    public Text valyeCount;
    public void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false && IsGameOver == false)
        {
            PausePanel.SetActive(true);
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            PausePanel.SetActive(false);
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
        valyeCount.text = slider.value.ToString();
        AudioListener.volume = slider.value;
    }
    
}
