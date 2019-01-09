using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class Fade : MonoBehaviour {

    // Use this for initialization
    WaitForSeconds delay=new WaitForSeconds(0.1f);
    float time;
	void Start () 
    {

	}
    private void OnEnable()
    {
        StartCoroutine(Fadecontrol());
    }
  
    IEnumerator Fadecontrol()
    {
        while(true)
        {
            yield return delay;
            time += 0.1f;
            if(time>=2f)
            {
                gameObject.SetActive(false);
                time = 0;
                yield break;
            }

        }
    }
}
