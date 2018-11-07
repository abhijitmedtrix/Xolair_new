
using UnityEngine;
using UnityEngine.UI;
public class Asthma_Onclick : MonoBehaviour
{

    [SerializeField]
    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
    }
    public void Onclick()
    {

        for (int i = 0; i < parent.childCount; i++)
        {

            parent.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
        gameObject.GetComponent<Toggle>().isOn = true;



    }
}