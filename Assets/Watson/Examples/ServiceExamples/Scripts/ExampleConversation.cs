/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Logging;
using System.Collections;
using FullSerializer;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;
using RogoDigital.Lipsync;
public class ExampleConversation : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/conversation/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("The workspaceId to run the example.")]
   
    [SerializeField]
    private string _workspaceId;
    [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
    [SerializeField]
    private string _versionDate;
    [Header("CF Authentication")]
    [Tooltip("The authentication username.")]
    [SerializeField]
    private string _username;
    [Tooltip("The authentication password.")]
    [SerializeField]
    private string _password;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey;
    [Tooltip("The IAM url used to authenticate the apikey (optional). This defaults to \"https://iam.bluemix.net/identity/token\".")]
    [SerializeField]
    private string _iamUrl;
    #endregion

    // [SerializeField]
    //private List <LipSyncData> SAA_dat,CSU_dat;
    public LipSyncData currentdat;
    [SerializeField]
    public GameObject SAA_Avatar;
    [SerializeField]
    private GameObject[] answertext;
    private Conversation _service;
    public SSA_UI_OUT sSA_UI_OUT;
    public CSU_UI_OUT cSU_UI_OUT;
    private fsSerializer _serializer = new fsSerializer();
    private Dictionary<string, object> _context = null;
    private int _questionCount = -1;
    private bool _waitingForResponse = true;
    string[] temp;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;
    public string[] WorkSpace_id;
   
     void Start()
    {
        var check = Resources.Load<LipSyncData>("CSU_datafiles/1");
        currentdat = check;


    }
    public  void Set_Avatar(string name)
    {
        AppManager.Current_mode = name;
       // Debug.Log(AppManager.Current_mode);
        if(AppManager.Current_mode=="CSU")
        {
            _workspaceId = WorkSpace_id[0];
           // Debug.Log(_workspaceId);
            LogSystem.InstallDefaultReactors();
            Runnable.Run(CreateService());
        }
        else if(AppManager.Current_mode=="SAA")
        {
            _workspaceId = WorkSpace_id[1];
           // Debug.Log(_workspaceId);
            LogSystem.InstallDefaultReactors();
            Runnable.Run(CreateService());
        }

    }
    private IEnumerator CreateService()
    {
        //  Create credential and instantiate service
        Credentials credentials = null;
        if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
        {
            //  Authenticate using username and password
            credentials = new Credentials(_username, _password, _serviceUrl);
        }
        else if (!string.IsNullOrEmpty(_iamApikey))
        {
            //  Authenticate using iamApikey
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = _iamApikey,
                IamUrl = _iamUrl
            };

            credentials = new Credentials(tokenOptions, _serviceUrl);

            //  Wait for tokendata
            while (!credentials.HasIamTokenData())
                yield return null;
        }
        else
        {
            throw new WatsonException("Please provide either username and password or IAM apikey to authenticate the service.");
        }

        _service = new Conversation(credentials);
        _service.VersionDate = _versionDate;

       // Runnable.Run(Examples());
    }

    //private IEnumerator Examples()
    //{
    //    //if (!_service.Message(OnMessage, OnFail, _workspaceId, "hello"))
    //        //Log.Debug("ExampleConversation.Message()", "Failed to message!");

    //    while (_waitingForResponse)
    //        yield return null;

    //    _waitingForResponse = true;
    //    _questionCount++;

    //    AskQuestion();
    //    while (_waitingForResponse)
    //        yield return null;

    //    _questionCount++;

    //    _waitingForResponse = true;

    //    AskQuestion();
    //    while (_waitingForResponse)
    //        yield return null;
    //    _questionCount++;

    //    _waitingForResponse = true;

    //    AskQuestion();
    //    while (_waitingForResponse)
    //        yield return null;
    //    _questionCount++;

    //    _waitingForResponse = true;

    //    AskQuestion();
    //    while (_waitingForResponse)
    //        yield return null;

    //    Log.Debug("ExampleConversation.Examples()", "Conversation examples complete.");
    //}

    public void AskQuestion(string question )
    {

        audioSource.Stop();
       if(AppManager.Current_mode=="SAA")
        {
            answertext[0].SetActive(false);
        }
        else if (AppManager.Current_mode == "CSU")
        {
            answertext[1].SetActive(false);
        }
        MessageRequest messageRequest = new MessageRequest()
        {
            input = new Dictionary<string, object>()
            {
                { "text", question}
            },
            context = _context
        };

        if (!_service.Message(OnMessage, OnFail, _workspaceId, messageRequest))
            Log.Debug("ExampleConversation.AskQuestion()", "Failed to message!");
    }

    private void OnMessage(object resp, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleConversation.OnMessage()", "Conversation: Message Response: {0}", customData["json"].ToString());

        //  Convert resp to fsdata
        fsData fsdata = null;
        fsResult r = _serializer.TrySerialize(resp.GetType(), resp, out fsdata);
        if (!r.Succeeded)
            throw new WatsonException(r.FormattedMessages);

        //  Convert fsdata to MessageResponse
        MessageResponse messageResponse = new MessageResponse();
        object obj = messageResponse;
        r = _serializer.TryDeserialize(fsdata, obj.GetType(), ref obj);
        if (!r.Succeeded)
            throw new WatsonException(r.FormattedMessages);
        // Debug.Log(messageResponse.output.nodes_visited[messageResponse.output.nodes_visited.Length]);
        if (messageResponse.output.text.Length == 0)
        {
            if (AppManager.Current_mode == "SAA")
            {
                answertext[0].SetActive(true);
                sSA_UI_OUT.Answer_txt.text = "please type a valid question";
            }
            else if (AppManager.Current_mode == "CSU")
            {
                answertext[1].SetActive(true);
                cSU_UI_OUT.Answer_txt.text = "please type a valid question";
            }


        }
        else
        {
            if(AppManager.Current_mode=="SAA")
            {
                temp = new string[2];
                //temp = messageResponse.output.text[0].Split('/');
                //Debug.Log(temp[0]+"answer"+temp[1]);
                Debug.Log(messageResponse.intents[0].confidence);
                float confdc = messageResponse.intents[0].confidence;
                if(confdc>0.7f)
                {
                    temp = messageResponse.output.text[0].Split('/');
                    sSA_UI_OUT.Answer_txt.text = temp[0];
                    //AppManager.AnswerIndx = int.Parse(temp[1]);
                    //SAA_Avatar.GetComponent<LipSync>().defaultClip = SAA_dat[AppManager.AnswerIndx - 1];
                    // Debug.Log(AppManager.AnswerIndx);
                    // SAA_Avatar.GetComponent<LipSync>().Play(SAA_dat[AppManager.AnswerIndx - 1]);
                    currentdat = Resources.Load<LipSyncData>("SAA_datafiles/" + temp[1]);
                    answertext[0].SetActive(true);
                    SAA_Avatar.GetComponent<LipSync>().Play(currentdat);
                    animator.SetBool("StartTalk", true);
                    animator.SetBool("StopTalk", false);

                    StartCoroutine(AudioSound());
                }
                else
                {
                    answertext[0].SetActive(true);
                    sSA_UI_OUT.Answer_txt.text = "please type a valid question";
                    cSU_UI_OUT.Answer_txt.text = "please type a valid question";
                }
              
            }
            else if(AppManager.Current_mode=="CSU")

            {
                temp = new string[2];
                //temp = messageResponse.output.text[0].Split('/');
                //Debug.Log(temp[0]+"answer"+temp[1]);
                Debug.Log(messageResponse.intents[0].confidence);
                float confdc = messageResponse.intents[0].confidence;
                if (confdc > 0.7f)
                {
                    temp = messageResponse.output.text[0].Split('/');
                    answertext[1].SetActive(true);
                    cSU_UI_OUT.Answer_txt.text = temp[0];
                    // AppManager.AnswerIndx = int.Parse(temp[1]);
                    //SAA_Avatar.GetComponent<LipSync>().defaultClip = SAA_dat[AppManager.AnswerIndx - 1];
                    // Debug.Log("coming");
                    // Debug.Log(AppManager.AnswerIndx - 1);
                    // SAA_Avatar.GetComponent<LipSync>().Play(CSU_dat[AppManager.AnswerIndx - 1]);
                    currentdat = Resources.Load<LipSyncData>("CSU_datafiles/" + temp[1]);
                    answertext[1].SetActive(true);
                    SAA_Avatar.GetComponent<LipSync>().Play(currentdat);
                    animator.SetBool("StartTalk", true);
                    animator.SetBool("StopTalk", false);
                    StartCoroutine(AudioSound());
                }
                else
                {
                    answertext[1].SetActive(true);
                    sSA_UI_OUT.Answer_txt.text = "please type a valid question";
                    cSU_UI_OUT.Answer_txt.text = "please type a valid question";

                }
              
            }
        }
        Debug.Log(messageResponse.output.nodes_visited[0]);
        //  Set context for next round of messaging
        object _tempContext = null;
        (resp as Dictionary<string, object>).TryGetValue("context", out _tempContext);

        if (_tempContext != null)
            _context = _tempContext as Dictionary<string, object>;
        else
            Log.Debug("ExampleConversation.OnMessage()", "Failed to get context");
        _waitingForResponse = false;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleConversation.OnFail()", "Error received: {0}", error.ToString());
    }
    IEnumerator AudioSound()
    {
        while(true)
        {
            if(!audioSource.isPlaying)
            {
              
                animator.SetBool("StartTalk", false);

                animator.Play("handdown");
                animator.SetBool("StopTalk", true);
               

              //  Debug.Log("one");
                yield break;
            }
            yield return null;
        }
    }
   
}
