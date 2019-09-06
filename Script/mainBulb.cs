using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainBulb : MonoBehaviour
{
    public float speedbulb = 50f;
    public float BulbSpawnTime = 3f;
    public GameObject Bulb;
    public GameObject BulbPlayer;
    public Text coutText;
    public Transform PoinBulb;
    public int count;
    public GameObject WinPanel;
    void Start()
    {
        InvokeRepeating("Timer", 1, BulbSpawnTime);
        Bulb = Resources.Load("bulb") as GameObject;
        count = 0;
        SetCounttext();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }

    void Timer()
    {
       
        BulbPlayer = Instantiate(Bulb, PoinBulb.position, transform.rotation);
      
        BulbPlayer.GetComponent<Rigidbody>().AddForce(BulbPlayer.transform.position*speedbulb);
        Destroy(BulbPlayer, 10);
        SetCounttext();
    }

    void SetCounttext()
    {
        coutText.text = "Money : " + count.ToString();
        if(count>=100)
        WinPanel.SetActive(true);
    }
}
