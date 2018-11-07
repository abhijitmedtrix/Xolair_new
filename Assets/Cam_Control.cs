using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Control : MonoBehaviour
{

    // Use this for initialization
    private WebCamTexture webCamTexture;
    [SerializeField]
    private RawImage Camera_Screen;
    //[SerializeField]
    //private RawImage camerascreen;
    [SerializeField]
    private List<Texture2D> pics;
    [SerializeField]
    private RawImage[] Captured_Images;
    private const byte Pic_count = 5;
    [SerializeField]
    private Text txt;
    [SerializeField]
    private GameObject Cam_buttn;
    [SerializeField]
    private Audio_Manager audio_Manager;
    public AudioSource audioSource;
    public AudioClip camsound;
    void Start()
    {
        //webCamTexture = new WebCamTexture();
        //Camera_Screen.texture = webCamTexture;

        //webCamTexture.Play();
        //TakePhoto();
        webCamTexture = new WebCamTexture();

    }

    private void Update()
    {
        //int x = webCamTexture.videoRotationAngle;
        //txt.text = x.ToString();
      Camera_Screen.rectTransform.localEulerAngles = Vector3.forward *90;
       // Debug.Log("webcam"+webCamTexture.videoRotationAngle);
        if (Input.GetKeyUp(KeyCode.A))
        {

            TakePhoto();

        }
        if (Input.GetKeyUp(KeyCode.D))
        {

            StopPhoto();

        }
    }
    public void TakePhoto()
    {
        if (!audioSource.isPlaying)
        {
            
            if (pics.Count <= Pic_count)
            {
                audio_Manager.Audio_play(camsound);
                Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
                photo.SetPixels(webCamTexture.GetPixels());
                photo.Apply();
                pics.Add(photo);
                // camerascreen.texture = photo;
            }
            else
            {
                Debug.Log("pic count");
            }
            // byte[] bytes = photo.EncodeToPNG();

            //File.WriteAllBytes(Application.dataPath + "/backgrounds" + 1 + ".png", bytes);
            //Debug.Log(Application.dataPath);
        }
    }
    public void Save_IMG() 
    {
        webCamTexture.Stop();
        if (pics.Count!=0)
        {
            foreach(RawImage r in Captured_Images)
            {
                r.gameObject.SetActive(false);
            }
            for (int i = 0; i < pics.Count;i++)
            {
                Captured_Images[i].gameObject.SetActive(true);
                Captured_Images[i].texture = pics[i];

            }
        }
        else
        {
            Debug.Log("no picture captured");
        }

    }
    public void StopPhoto()
    {
        webCamTexture.Stop();
    }
    public  void Start_cam()
    {
        Camera_Screen.gameObject.SetActive(true);

        webCamTexture.Play();
        Camera_Screen.texture = webCamTexture;
        Cam_buttn.SetActive(false);

    }
}
