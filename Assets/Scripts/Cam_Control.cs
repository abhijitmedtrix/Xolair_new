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
    private List<RawImage> Captured_Images;
    private const byte Pic_count = 5;
    [SerializeField]
    private Text txt;
    [SerializeField]
    private GameObject Cam_buttn;
    [SerializeField]
    private Audio_Manager audio_Manager;
    public AudioSource audioSource;
    public AudioClip camsound;
    [SerializeField]
    private CSU_Display cSU_Display;
    [SerializeField]
    private List<Texture2D> Finalimg;
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
      //Camera_Screen.rectTransform.localEulerAngles = Vector3.forward *90;
       // Debug.Log("webcam"+webCamTexture.videoRotationAngle);
        //if (Input.GetKeyUp(KeyCode.A))
        //{

        //    TakePhoto();

        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{

        //    StopPhoto();

        //}
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
    public void Set_IMG() 
    {
        webCamTexture.Stop();
        foreach (RawImage r in Captured_Images)
        {
            r.gameObject.SetActive(false);
        }
        if (pics.Count!=0)
        {
          
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
    public void Save_IMG()
    {
        if (Finalimg.Count != 0)
        {
            cSU_Display.Save_Img(Finalimg.ToArray());
        }
    }
    public void Setbutn(GameObject obj)
    {
        Texture2D temp;
        switch (obj.tag)
        {
            case "delete":
                {
                     temp=   (obj.transform.parent.GetComponent<RawImage>().texture)as Texture2D;
                  
                    pics.Remove(temp);
                    for (int i = 0; i < Captured_Images.Count;i++)
                    {
                        Captured_Images[i].gameObject.SetActive(false);
                        
                    }
                    Captured_Images.RemoveAt(Captured_Images.Count-1);
                    Finalimg.Remove(temp);
                    for (int i = 0; i < pics.Count; i++)
                    {

                        Captured_Images[i].texture = pics[i];
                        Captured_Images[i].gameObject.SetActive(true);

                        //for (int x = 0; x < Finalimg.Count; x++)
                        //{
                          
                        //}
                    }
                   

                    
                    
                    break;
                }
            case "add":
                {
                    temp = (obj.transform.parent.GetComponent<RawImage>().texture) as Texture2D;
                    if(!Finalimg.Contains(temp))
                    {
                        Finalimg.Add(temp);
                    }
                    break;
                }
            case "remove":
                {
                    temp = (obj.transform.parent.GetComponent<RawImage>().texture) as Texture2D;
                    Finalimg.Remove(temp);
                    break;
                }
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
        pics.Clear();
        Finalimg.Clear();

    }
}
