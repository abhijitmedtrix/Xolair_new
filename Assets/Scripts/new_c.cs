using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_c : MonoBehaviour {
    public GameObject notification;
    public void Onclick()
    {
        gameObject.SetActive(false);
        notification.SetActive(false);
    }
}
