using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class FAQViewSAA : MonoBehaviour
{
    [SerializeField] private GameObject contentDrug, contentDisease;
   // [SerializeField] private MaterialUI.ScreenManager screenManager;
    [SerializeField]private Text[] Title;
    //[SerializeField] private bool toggle;
    private static List<FAQDrugSAA> faqdrugDat;
    private static List<FAQDiseaseSAA> faqdiseaseDat;
    private List<int> questionNum = new List<int>();
    //public AppManager appManager;
    public void Drug(string mode)
    {

        switch (mode)
        {

            case "xolairandAsthma":
                {
                    setextDrug(FAQDrugSAA.xolairandAsthma.Count, FAQDrugSAA.xolairandAsthma);
                    break;
                }
            case "dosageAdministration":
                {
                    setextDrug(FAQDrugSAA.dosageAdministration.Count, FAQDrugSAA.dosageAdministration);
                    break;
                }
            case "packInformation":
                {
                    setextDrug(FAQDrugSAA.packInformation.Count, FAQDrugSAA.packInformation);
                    break;
                }
            case "warning":
                {
                    setextDrug(FAQDrugSAA.warning.Count, FAQDrugSAA.warning);
                    break;
                }
            case "treatmentofXolair":
                {
                    setextDrug(FAQDrugSAA.treatmentofXolair.Count, FAQDrugSAA.treatmentofXolair);
                    break;
                }
            case "useofXolair":
                {
                    setextDrug(FAQDrugSAA.useofXolair.Count, FAQDrugSAA.useofXolair);
                    break;
                }
            case "contraIndications":
                {
                    setextDrug(FAQDrugSAA.contraIndications.Count, FAQDrugSAA.contraIndications);
                    break;
                }
            case "drugInteraction":
                {
                    setextDrug(FAQDrugSAA.drugInteraction.Count, FAQDrugSAA.drugInteraction);
                    break;
                }


        }
    }


    public void Disease(string mode)
    {
        switch (mode)
        {
            case "aboutAsthma":
                {
                    setextDisease(FAQDiseaseSAA.aboutAsthma.Count, FAQDiseaseSAA.aboutAsthma);
                    break;
                }
            case "measureAsthma":
                {
                    setextDisease(FAQDiseaseSAA.measureAsthma.Count, FAQDiseaseSAA.measureAsthma);
                    break;
                }
            case "diagoniseAsthma":
                {
                    setextDisease(FAQDiseaseSAA.diagoniseAsthma.Count, FAQDiseaseSAA.diagoniseAsthma);
                    break;
                }
            case "asthmSymptoms":
                {
                    setextDisease(FAQDiseaseSAA.asthmSymptoms.Count, FAQDiseaseSAA.asthmSymptoms);
                    break;
                }
            case "asthmaTrigger":
                {
                    setextDisease(FAQDiseaseSAA.asthmaTrigger.Count, FAQDiseaseSAA.asthmaTrigger);
                    break;
                }
            case "modificationAsthma":
                {
                    setextDisease(FAQDiseaseSAA.modificationAsthma.Count, FAQDiseaseSAA.modificationAsthma);
                    break;
                }
        }

    }
    private void setextDrug(int length, List<FAQDrugSAA> data)
    {
        faqdrugDat = data;
        for (int i = 0; i < length; i++)
        {
            //content.transform.setactive(true);
            contentDrug.transform.GetChild(i).gameObject.SetActive(true);
            contentDrug.transform.GetChild(i).GetComponent<Text>().text = "\n\n" + data[i].Question + "\n\n";
        }
        //screenManager.Set(34);
    }



    private void setextDisease(int length, List<FAQDiseaseSAA> data)
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
        for (int i = 0; i < contentDrug.transform.childCount; i++)
        {
            contentDrug.transform.GetChild(i).gameObject.SetActive(false);
            contentDisease.transform.GetChild(i).gameObject.SetActive(false);

        }
    }
    public void setanswerDrug(int indx)
    {
        //Debug.Log(toggle);
        if (questionNum.Contains(indx))
        {
            contentDrug.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdrugDat[indx].Question + "\n\n";
            questionNum.Remove(indx);


        }
        else
        {
            contentDrug.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdrugDat[indx].Question + "\n\n" + faqdrugDat[indx].Answer + "\n\n";
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
            contentDisease.transform.GetChild(indx).GetComponent<Text>().text = "\n\n" + faqdiseaseDat[indx].Question + "\n\n" + faqdiseaseDat[indx].Answer + "\n\n";
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

