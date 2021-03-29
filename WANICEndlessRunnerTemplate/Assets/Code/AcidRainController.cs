using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AcidRainController : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI acidRainAnnouncer;
    public static bool announceRain = false;
    public static bool RainInProgress = false;
    private float timer = 0;
    [SerializeField] GameObject acid;
    private int acidSpawnAmount = 6;
    // Start is called before the first frame update
    void Start()
    {
        acidRainAnnouncer.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (announceRain)
        {
            announceRain = false;
            RainInProgress = true;
            StartCoroutine(prepare());

        }
    }
    IEnumerator prepare()
    {
        timer = 5;
        while (timer > 0)
        {
            acidRainAnnouncer.text = "Acid Rain begins in " + (int)timer;
            yield return new WaitForSeconds(1);
            timer--;
        }
        StartCoroutine(LaunchRain());
    }
    IEnumerator LaunchRain()
    {
        acidRainAnnouncer.text = "Acid inbound!";
        yield return new WaitForSeconds(2);
        acidRainAnnouncer.text = "";
        for (int i = 0; i < 2 * acidSpawnAmount; i++)
        {
            Instantiate(acid, new Vector3(Random.Range(transform.position.x - 18, transform.position.x + 18), transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
        RainInProgress = false;
    }
}
