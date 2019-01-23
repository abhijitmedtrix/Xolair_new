using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class FAQViewCSU : MonoBehaviour 
{
    [SerializeField] private GameObject contentDrug,contentDisease;
    //[SerializeField] private MaterialUI.ScreenManager screenManager;
    [SerializeField] private Text[] Title;
   // [SerializeField] private bool toggle;
    private static List<FAQDDrugCSU> faqdrugDat;
    private static List<FAQDiseaseCSU> faqdiseaseDat=new List<FAQDiseaseCSU>();
    private List<int> questionNum=new List<int>();

    public  void  Drug(string mode)
    {
   
        switch (mode)
        {
          
            case "xolairandCSU":
                {
                    setextDrug(FAQDDrugCSU.xolairandCSU.Count,FAQDDrugCSU.xolairandCSU);
                    break;
                }
            case "dosageAdministration":
                {
                    setextDrug(FAQDDrugCSU.dosageAdministration.Count, FAQDDrugCSU.dosageAdministration);
                    break;
                }
            case "packageInformation":
                {
                    setextDrug(FAQDDrugCSU.packageInformation.Count, FAQDDrugCSU.packageInformation);
                    break;
                }
            case "warning":
                {
                    setextDrug(FAQDDrugCSU.warning.Count, FAQDDrugCSU.warning);
                    break;
                }
            case "treatment":
                {
                    setextDrug(FAQDDrugCSU.treatment.Count,FAQDDrugCSU.treatment);
                    break;
                }
            case "useofXolair":
                {
                    setextDrug(FAQDDrugCSU.useofXolair.Count, FAQDDrugCSU.useofXolair);
                    break;
                }
            case "contraIndications":
                {
                    setextDrug(FAQDDrugCSU.contraIndications.Count, FAQDDrugCSU.contraIndications);
                    break;
                }
            case "drugInteraction":
                {
                    setextDrug(FAQDDrugCSU.drugInteraction.Count, FAQDDrugCSU.drugInteraction);
                    break;
                }

           
        }
    }


    public  void Disease(string mode)
    {
        switch(mode)
        {
            case "aboutCsu":
                {
                    setextDisease(FAQDiseaseCSU.aboutCsu.Count, FAQDiseaseCSU.aboutCsu);
                    break;
                }
            case "diagoniseCsu":
                {
                    setextDisease(FAQDDrugCSU.drugInteraction.Count, FAQDiseaseCSU.diagoniseCsu);
                    break;
                }
            case "symptomCsu":
                {
                    setextDisease(FAQDDrugCSU.drugInteraction.Count, FAQDiseaseCSU.symptomCsu);
                    break;
                }
            case "triggerCsu":
                {
                    setextDisease(FAQDDrugCSU.drugInteraction.Count, FAQDiseaseCSU.triggerCsu);
                    break;
                }
            case "measureCsu":
                {
                    setextDisease(FAQDDrugCSU.drugInteraction.Count, FAQDiseaseCSU.measureCsu);
                    break;
                }
            case "modifyCsu":
                {
                    setextDisease(FAQDDrugCSU.drugInteraction.Count, FAQDiseaseCSU.modifyCsu);
                    break;
                }
        }
        
    }
    private void setextDrug(int length, List<FAQDDrugCSU> data)
    {
        Debug.Log(length);
        faqdrugDat = data;
        for (int i = 0; i < length;i++)
        {
            Debug.Log(i);
            //content.transform.setactive(true);
            contentDrug.transform.GetChild(i).gameObject.SetActive(true);
            contentDrug.transform.GetChild(i).GetComponent<Text>().text = "\n\n"+data[i].Question+"\n\n";
        }
        //screenManager.Set(34);
    }



    private void setextDisease(int length, List<FAQDiseaseCSU> data)
    {
        faqdiseaseDat = data;
        for (int i = 0; i < length; i++)
        {
            //content.transform.setactive(true);
            contentDisease.transform.GetChild(i).gameObject.SetActive(true);
            contentDisease.transform.GetChild(i).GetComponent<Text>().text = "\n\n" + data[i].Question + "\n\n";
        }
       // screenManager.Set(35);
    }



    public void reset()
    {
        questionNum.Clear();
       // screenManager.Set(32);
        for (int i = 0; i < contentDrug.transform.childCount;i++)
        {
            contentDrug.transform.GetChild(i).gameObject.SetActive(false);
            contentDisease.transform.GetChild(i).gameObject.SetActive(false);

        }
    }
    public  void setanswerDrug(int indx)
    {
        //Debug.Log(toggle);
        if (questionNum.Contains(indx))
        {
            contentDrug.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdrugDat[indx].Question + "\n\n";
            questionNum.Remove(indx);


        }
            else
        {
            contentDrug.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" +faqdrugDat[indx].Question + "\n\n" + "<color=black>" + faqdrugDat[indx].Answer+"</color>" + "\n\n";
            questionNum.Add(indx);
        }
       


    }
    public void setanswerDisease(int indx)
    {
        //Debug.Log(toggle);
        if (questionNum.Contains(indx))
        {
            contentDisease.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdiseaseDat[indx].Question + "\n\n";
            questionNum.Remove(indx);


        }
        else
        {
            contentDisease.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdiseaseDat[indx].Question + "\n\n" + "<color=black>" + faqdiseaseDat[indx].Answer + "</color>" + "\n\n";
            questionNum.Add(indx);
        }



    }
    public void settitleDrug(string title)
    {

        Title[0].text = title;
    }
    public void settitleDisease(string title)
    {
        Title[1].text = title;
    }
}
