
using System.Collections.Generic;


public class FAQDrugSAA : FAQ.Faq 
{
    public FAQDrugSAA(string question,string answer)
    {
        this.Question = question;
        this.Answer = answer;

    }


    //ABOUT XOLAIR AND OTHER ASTHMA TREATMENTS


    public static List<FAQDrugSAA> xolairandAsthma = new List<FAQDrugSAA> 
    {
    
    
        //1


        new FAQDrugSAA("What is Xolair?",
                       "Xolair is an injectable prescription medicine that improves asthma control in patients with severe persistent allergic asthma."),

    
        //2


        new FAQDrugSAA("What does Xolair contain?",
                       "The active ingredient in Xolair is Omalizumab. Omalizumab is a humanised monoclonal antibody manufactured by recombinant DNA " +
                       "technology in a Chinese hamster ovary (CHO) mammalian cell line. The other ingredients in Xolair are sucrose, L-histidine, L-histidine " +
                       "hydrochloride monohydrate and polysorbate 20."),

    
        //3


        new FAQDrugSAA("What is the meaning of rescue medications?",
                       "These are provided to all patients for as needed relief of breakthrough symptoms, also during asthma exacerbations. Reducing and " +
                       "ideally eliminating the need for rescue medications is both an important goal in asthma management and a measure of treatment success."),

    
        //4


        new FAQDrugSAA("How often should asthma be reviewed?",
                       "All healthcare providers should periodically review their patients to monitor their symptom control, risk factors and occurrence of " +
                       "exacerbations as well as document any response to treatment changes. Ideally patients should be seen 1-3 months after starting treatment " +
                       "and every 3-12 months thereafter."),

    
        //5


        new FAQDrugSAA("What is the active substance/ingredient in Xolair?",
                       "Xolair contains Omalizumab which is a humanized monoclonal antibody manufactured from a mammalian cell line."),

    
        //6


        new FAQDrugSAA("Will I have to take medications for both allergy and asthma if I suffer from allergic asthma?",
                       "The treatment goal for allergic asthma is symptom control. You may need to take medications for both allergy and asthma, especially " +
                       "when your symptoms become severe. There are new therapies designed to treat both conditions together such as immunotherapy and " +
                       "anti-immunoglobulin (IgE) therapy. Consult your doctor to know which treatment options are best for you."),

    
        //7


        new FAQDrugSAA("Can you tell me about an asthma action plan?",
                       "An asthma action plan is a written treatment plan given by a doctor to an asthma patient. It will include information about: " +
                       "1. Trigger factors that can cause asthma2. Tips to avoid the onset of asthma episodes3. What to do in the event of an asthma attack" +
                       " 4. Instructions about the time and dose of medications The asthma action plan will be individualized based on the patient's condition " +
                       "and requirements."),

    
        //8


        new FAQDrugSAA("Is Xolair available in tablet form?",
                       "No. Xolair is available only as a prescription injectable medicine which is administered under the skin. "),

    
        //9


        new FAQDrugSAA("Is Xolair available as an inhaler?",
                       "No. Xolair is available only as a prescription injectable medicine which is administered under the skin."),

    
        //10


        new FAQDrugSAA("Is Xolair included in my health insurance?",
                       "You will need to check with your health insurance provider to know whether Xolair therapy is covered by your insurance."),

    
        //11


        new FAQDrugSAA("Can I take a Xolair injection to stop an asthma attack?",
                       "No, Xolair is not a rescue medication. It is not indicated for stopping an acute exacerbation of asthma. You may have to use the rescue " +
                       "medication prescribed by your doctor for relieving an asthma attack."),

    
        //12


        new FAQDrugSAA("Does Xolair contain steroids?",
                       "Xolair contains omalizumab which is a genetically manufactured antibody.  It does not contain any steroids."),

    
        //13


        new FAQDrugSAA("Can Xolair be used for patients with allergy?",
                       "Xolair should not be used for treating all types of allergies. Currently, Xolair injections have received approval for use in the " +
                       "treatment of 1. severe persistent asthma induced by allergy 2. chronic (long standing) cases of hives without a known cause Chronic" +
                       " Spontaneous Urticaria"),

    
        //14


        new FAQDrugSAA("How does Xolair affect IgE levels?",
                       "Xolair binds to the free serum IgE, forming small complexes of three or six molecules. By forming complexes with IgE in this way," +
                       " Xolair prevents IgE from binding to receptors on mast cells and basophils, thereby inhibiting the allergen-induced release of " +
                       "inflammatory mediators."),

    
        //15


        new FAQDrugSAA("What happens to the levels of IgE in the long run?",
                       "Xolair binds to the free serum IgE, forming small complexes of three or six molecules. By forming complexes with IgE in this way, " +
                       "Xolair prevents IgE from binding to receptors on mast cells and basophils, thereby inhibiting the allergen-induced release of " +
                       "inflammatory mediators.  With time these receptors decrease in number and a resultant decrease in the overall production of IgE " +
                       "in the long term occurs."),

    
        //16


        new FAQDrugSAA("For how long should patients (adults and children) receive Xolair if it is successful, before deciding to stop the treatment?",
                       "The discontinuation of Xolair treatment has not been fully determined. Discontinuation generally results in a return of asthma associated symptoms."),

    
        //17


        new FAQDrugSAA("If Xolair can be discontinued how should it be done?",
                       "Discontinuation generally results in a return  of asthma associated symptoms. However, the optimal timing and method for discontinuation" +
                       " will need to be established to minimize symptoms. Further research into Xolair treatment discontinuation is required."),

    
        //18


        new FAQDrugSAA("What happens if I stop Xolair treatment?",
                       "Discontinuation of Xolair treatment generally results in a return  to high levels of free IgE and its associated symptoms. "),

    
        //19


        new FAQDrugSAA("What should I let my physician know before starting Xolair therapy?",
                       "Before receiving Xolair, tell your healthcare provider about all of your medical conditions, including if you:• have any other allergies " +
                       "(such as food allergy or seasonal allergies)• have sudden breathing problems (bronchospasm)• have ever had a severe allergic reaction " +
                       "called anaphylaxis• have or have had a parasitic infection• have or have had cancer• are pregnant or plan to become pregnant. It is not" +
                       " known if Xolair may harm your unborn baby.• are breastfeeding or plan to breastfeed. It is not known if Xolair passes into your breast" +
                       " milk. Talk with your healthcare provider about the best way to feed your baby while you receive Xolair.Tell your healthcare provider " +
                       "about all the medicines you take, including prescription and over the-counter medicines, vitamins and herbal supplements."),

    
        //20


        new FAQDrugSAA("Can I take Xolair if I have food allergies?",
                       "Before receiving Xolair, it is important to tell your healthcare provider about any food allergies that you may have. Xolair is not meant " +
                       "to prevent or treat other allergy-type conditions, such as sudden allergic reactions, hyperimmunoglobulin E syndrome " +
                       "(an inherited immune disorder), aspergillosis (a fungus-related lung disease), food allergy, eczema or hay fever because Xolair has " +
                       "not been studied in these conditions."),

    
        //21


        new FAQDrugSAA("Can I take Xolair if I have a history of sudden breathing problems (bronchospasm)?",
                       "Before receiving Xolair, tell your healthcare provider about all of your medical conditions, including if you  have sudden breathing " +
                       "problems (bronchospasm). Xolair is not indicated for the treatment of acute asthma exacerbations, acute bronchospasm or " +
                       "status asthmaticus."),

    
        //22


        new FAQDrugSAA("Can I take Xolair if I have ever experienced anaphylaxis?",
                       "Before receiving Xolair, tell your healthcare provider about all of your medical conditions, including if you have had a severe allergic" +
                       " reaction or anaphylaxis in the past. It is important to know that anaphylaxis may occur when taking omalizumab, also with onset after" +
                       " a long duration of treatment. It was seen that most of these reactions occurred within 2 hours after the first and subsequent injections " +
                       "of Xolair but some started beyond 2 hours and even beyond 24 hours after the injection. Therefore medicinal products for the treatment of" +
                       " anaphylactic reactions should always be available for immediate use following administration of Xolair. Patients should be informed that" +
                       " such reactions are possible and prompt medical attention should be sought if allergic reactions occur. A history of anaphylaxis " +
                       "unrelated to omalizumab may be a risk factor for anaphylaxis following Xolair administration."),

    
        //23


        new FAQDrugSAA("Can I take Xolair if I have or have had a parasitic infection?",
                       "Before receiving Xolair, tell your healthcare provider about all of your medical conditions, including if you have/had parasitic " +
                       "infection. Xolair may indirectly reduce the efficacy of medicinal products for the treatment of helminthic or other parasitic infections."),

    
        //24


        new FAQDrugSAA("Can I take Xolair if I am pregnant or plan to become pregnant. ",
                       "Before receiving Xolair, tell your healthcare provider if you are/or planning to become pregnant. It is not known if Xolair may " +
                       "harm your unborn baby. Xolair should not be used during pregnancy unless clearly necessary."),

    
        //25


        new FAQDrugSAA("Can I take Xolair if I am  breastfeeding or plan to breastfeed?",
                       "It is unknown whether omalizumab is excreted in human milk. A risk to the new-borns/infants cannot be excluded. Omalizumab should" +
                       "not be given during breast-feeding."),

    
        //26


        new FAQDrugSAA("Can Xolair impair fertility?",
                       "There are no human fertility data for omalizumab."),

    
        //27


        new FAQDrugSAA("Can Xolair impair the ability to drive and use machines?",
                       "Xolair has no or negligible influence on the ability to drive and use machines."),

    
        //28


        new FAQDrugSAA("Can relapse occur, if I discontinue Xolair treatment?",
                       "Discontinuation of omalizumab treatment generally results in a return to elevated free IgE levels and associated asthma symptoms.")
    };


    //DOSAGE AND ADMINISTRATION


    public static List<FAQDrugSAA> dosageAdministration = new List<FAQDrugSAA> 
    { 


        //1


        new FAQDrugSAA("Can I take Xolair on my own?",
                       "NO. Xolair treatment should be initiated by physicians experienced in the diagnosis and treatment of severe persistent asthma. " +
                       "Treatment is intended to be administered by a healthcare provider only."),


        //2


        new FAQDrugSAA("On what factors is the dose and dosing frequency of Xolair determined?",
                       "Omalizumab dose and dose frequency are based on baseline serum total IgE level (30−1,500 IU/mL) and body weight (20−150 kg). These " +
                       "variables are used to determine the correct dose using dosing tables.The recommended dose is  75−600 mg every 4 weeks or 225−600 mg " +
                       "every 2 weeks.Patients whose baseline IgE levels or body weight lie outside of the dosing table limits should not be given omalizumab." +
                       "Also, it should be kept in mind that the dosing remains the same regardless of the formulation (lyophilised powder for reconstitution " +
                       "or pre-filled syringes)"),


        //3


        new FAQDrugSAA("What is the recommended Xolair dose?",
                       "The recommended dose is  75−600 mg every 4 weeks or 225−600 mg every 2 weeks. Patients whose baseline IgE levels or body weight lie " +
                       "outside the dosing table limits should not be given omalizumab."),


        //4


        new FAQDrugSAA("How should I receive Xolair?",
                       "Xolair should be given by your healthcare provider, in a healthcare setting. It is given in 1 or more injections under the skin " +
                       "(subcutaneous), 1 time every 2 or 4 weeks."),


        //5


        new FAQDrugSAA("Will there be a difference in the dosing of Xolair in powder/solvent and PFS formulations?",
                       "Dosing of Xolair remains the same regardless of its formulation (lyophilised powder for reconstitution or pre-filled syringes)."),


        //6


        new FAQDrugSAA("What is the method of Xolair administration?",
                       "Xolair is intended for subcutaneous administration only. It is not administered by the intravenous or intramuscular route."),


        //7


        new FAQDrugSAA("What happens if I miss a dose of Xolair?",
                       "Contact your doctor or hospital as soon as possible to re-schedule your appointment."),


        //8


        new FAQDrugSAA("My symptoms improved after taking/receiving Xolair, can the dose of Xolair or its frequency of administration be reduced?",
                       "You will be assessed for treatment effectiveness 16 weeks after initiating Xolair. The decision to continue Xolair should be based " +
                       "on whether a marked improvement in overall asthma control is seen or not. A dose reduction at 6 months results in recurrence of asthma " +
                       "symptoms in patients who are responders at 16 weeks."),


        //9


        new FAQDrugSAA("In case of relapse after the interruption of Xolair treatment,  how can I resume Xolair?",
                       "Dose determination is required for resuming Xolair treatment. For interruption of Xolair treatment for less than 1 year, dose should " +
                       "be based on serum IgE antibody levels obtained at the initial dose determination. For interruption of 1 year or more, a re-test of " +
                       "total serum IgE antibody levels is required to determine the dose."),


        //10


        new FAQDrugSAA("What will decide my Xolair dose once I decide to restart my treatment after a period of treatment interruption?",
                       "Dose determination is required for resuming Xolair treatment. For interruption in Xolair treatment of less than 1 year, dose should " +
                       "be based on serum allergic antibody IgE levels obtained at the initial dose determination. For interruption of 1 year or more, a " +
                       "re-test of total serum allergic antibody IgE levels is required to determine the dose."),


        //11


        new FAQDrugSAA("Is dose determination required after interruption in Xolair treatment?",
                       "Yes. Dose determination is required for resuming Xolair treatment. Because total IgE levels are elevated during omalizumab treatment" +
                       " and remain elevated for up to 1 year after discontinuation, re-testing of IgE levels during omalizumab treatment cannot be used as a" +
                       " guide for determining dose. Hence, for interruption in  Xolair treatment of less than 1 year, dose should be based on serum IgE levels" +
                       " obtained at the initial dose determination. For interruption of 1 year or more, a re-test of total serum IgE antibody levels is " +
                       "required to determine the dose."),


        //12


        new FAQDrugSAA("How can I receive/take Xolair?",
                       "Xolair can be received/taken under the skin (subcutaneous) as injection, administered only by a healthcare professional."),


        //13


        new FAQDrugSAA("Can I self-administer Xolair?",
                       "No. Xolair must be administered  only by a healthcare professional."),


        //14


        new FAQDrugSAA("How frequently do I need to take Xolair?",
                       "Xolair is administered by subcutaneous injection every 2 or 4 weeks based on a patient’s bodyweight and the baseline total IgE antibody levels."),


        //15


        new FAQDrugSAA("What are the sites at which Xolair will be injected?",
                       "Subcutaneous injection of Xolair is given into the upper arm. The injection can be administered in the thigh if administration into " +
                       "the arm is not possible. It can be divided into 2 or more injection sites when a dose is above 150 mg. "),


        //16


        new FAQDrugSAA("What are the disadvantages of Xolair as powder/solvent for reconstitution form?",
                       "The powder/solvent (for reconstitution) form of Xolair has two major drawbacks. Firstly, it takes 15-20 minutes for the powder " +
                       "to dissolve as it has a high protein content. Secondly, it takes 5-10 seconds to administer the required dose as the solution is " +
                       "slightly viscous in nature."),


        //17


        new FAQDrugSAA("What are the other formulations of Xolair?",
                       "In some countries including those in the European Union, Xolair is also available as a pre-filled syringe in 2 strengths: " +
                       "75 mg and 150 mg."),


        //18


        new FAQDrugSAA("What are the advantages of Xolair in the form of a pre-filled syringe as compared to powder for reconstitution formulation?",
                       "Xolair as a pre-filled syringe does not require reconstitution and thus using it saves time. Moreover it is less viscous than the " +
                       "reconstituted lyophilized formulation and also has a lower injection volume of 1.0 mL  (150 mg). Also, there is less wastage of Xolair" +
                       " compared to the other formulation."),


        //19


        new FAQDrugSAA("How is the dosing schedule of Xolair assessed?",
                       "The appropriate dose and dosing frequency of Xolair is determined by baseline immunoglobulin E (IgE) (IU/mL), measured before " +
                       "the start of treatment, and bodyweight (kg)."),


        //20


        new FAQDrugSAA("Does the level of allergic antibodies IgE in the blood affect the dosing schedule of Xolair?",
                       "Yes. Before the start of Xolair treatment the allergic antibody (IgE) levels in the blood and the body weight are measured, and " +
                       "depending upon these measurements the patients may need to receive 1  to 4 injections of 75 to 600 mg of Xolair."),


        //21


        new FAQDrugSAA("Does our bodyweight affect the dosing schedule of Xolair?",
                       "Yes. Before the start of Xolair treatment bodyweight and allergic antibody (IgE) levels in the blood are measured, and depending" +
                       " upon these measurements the patients may need to receive 1  to 4 injections of 75 to 600 mg of Xolair."),


        //22


        new FAQDrugSAA("What is the recommended dosing schedule of Xolair?",
                       "Based on IgE levels and body weight, the recommended dose of Xolair is 75 to 600 mg in 1 to 4 injections. "),


        //23


        new FAQDrugSAA("How long does each Xolair injection session take?",
                       "Although it takes less than a minute to administer each Xolair injection, you may need to remain at a healthcare facility or clinic" +
                       " for at least 2 hours after receiving a Xolair injection. During this time, your doctor would monitor you for allergic reactions or" +
                       "other side effects before sending you home. "),


        //24


        new FAQDrugSAA("What is the usual starting dose of Xolair in children?",
                       "The dose of a Xolair injection depends on your child's body weight and immunoglobulin (IgE) levels found in his or her blood sample. " +
                       "The minimum starting dose is 75 mg and the maximum dose is 375 mg of Xolair. "),


        //25


        new FAQDrugSAA("Can men and women suffering from allergic asthma take the same dose of Xolair?",
                       "The dose of Xolair depends on a person's body weight and immunoglobulin (IgE) levels. The dose of Xolair is not determined by other" +
                       " factors such as gender, race or ethnicity. "),

    
    
    };


    //PACK INFORMATION


    public static List<FAQDrugSAA> packInformation = new List<FAQDrugSAA> 
    { 
    
    
        //1


        new FAQDrugSAA("How is Xolair available in the market?",
                       "Xolair is supplied as a 75 mg or 150 mg powder and 2mL water solvent. The powder is reconstituted in the water before it is injected " +
                       "by a doctor or nurse. Xolair is also available as a prefilled syringe containing 0.5 ml/1 ml solution which has 75/150 mg of omalizumab."),
    
    
        //1


        new FAQDrugSAA("What is the shelf life of Xolair?",
                       "4 years. After reconstitution, the chemical and physical stability have been demonstrated for 8 hours at 2°C to 8°C and for 4 hours at 30°C."),

    
        //1


        new FAQDrugSAA("By which company is Xolair manufactured?",
                       "Novartis Europharm Limited, Ireland manufactures Xolair."),

    
        //1


        new FAQDrugSAA("How is Xolair stored?",
                       "Keep this medicine out of the sight and reach of children. Do not use this medicine after the expiry date which is stated on the label. " +
                       "The expiry date refers to the last day of that month. Store in a refrigerator (2°C - 8°C). Do not freeze."),

    
        //1


        new FAQDrugSAA("In what form is Xolair available?",
                       "Xolair is supplied as a powder for reconstitution along with the solvent (sterile water) prior to injection."),


    };


    //PRECAUTIONANDWARNING


    public static List<FAQDrugSAA> warning = new List<FAQDrugSAA> 
    {


        //1


        new FAQDrugSAA("What are the signs and symptoms of anaphylaxis that I should I be aware of when I am taking Xolair?",
                       "One should be aware of the risk of anaphylactic reactions occurring with Xolair use. If you notice any serious sudden signs of allergy " +
                       "or combination of signs such as rash, itching or hives on the skin, swelling of the face, lips, tongue, larynx (voice box), windpipe or " +
                       "other parts of the body, fast heart beat, dizziness and light-headedness, shortness of breath, wheezing or trouble breathing, or any " +
                       "other new symptoms, tell your doctor or nurse immediately. "),


        //2


        new FAQDrugSAA("When can the warning signs of anaphylaxis usually appear after taking Xolair?",
                       "Side-effects of Xolair generally occur within 2 hours after the first dose and subsequent dose of Xolair but may also occur beyond" +
                       " 2 hours or even 24 hours after starting treatment. "),


        //3


        new FAQDrugSAA("Can Xolair overdose cause toxicities?",
                       "The maximum tolerated dose of Xolair has not been determined. Single intravenous doses up to 4,000 mg have been administered to" +
                       " patients without evidence of dose-limiting toxicities. The highest cumulative dose administered to patients was 44,000 mg over " +
                       "a 20-week period, and this dose did not result in any untoward acute effects.If an overdose is suspected, the patient should be " +
                       "monitored for any abnormal signs or symptoms.  Medical treatment should be sought and instituted appropriately."),


        //4


        new FAQDrugSAA("What are the possible side effects of Xolair?",
                       "Like all medicines, Xolair can also cause side effects, although not everybody gets them. The side effects caused by Xolair are usually " +
                       "mild to moderate but can occasionally be serious. The very common side effects that may affect children are headache, fever and upper " +
                       "abdominal pain. Injection site reactions like pain, swelling, itching and redness are also commonly seen. The serious side effects " +
                       "include- sudden severe allergic reactions (anaphylaxis) and systemic lupus erythematosus (SLE). You may also experience other signs " +
                       "such as fever, weight loss, and fatigue. Low blood platelet count with symptoms such as bleeding or bruising more easily than normal. " +
                       "If you experience any of these, tell your doctor or nurse right away.Uncommon side effects  include feeling dizzy, sleepy or tired, " +
                       "tingling or numbness of the hands or feet, fainting, low blood pressure while sitting or standing (postural hypotension), flushing, " +
                       "sore throat, coughing, acute breathing problems, feeling sick (nausea), diarrhea, indigestion, itching, hives, rash, increased " +
                       "sensitivity of the skin to sun, weight increase, flu-like symptoms and swelling of the arms. Rare side effects include parasitic infection."),


        //5


        new FAQDrugSAA("What do I do if I get a side effect which has not been listed?",
                       "If you get any side effects, talk to your doctor, pharmacist or nurse. This includes any possible side effects not listed " +
                       "in this leaflet. You can also report side effects directly via the national reporting system. By reporting side effects you " +
                       "can help provide more information on the safety of this medicine."),


        //6


        new FAQDrugSAA("What do I do if I develop an allergic reaction to Xolair?",
                       "If you have any of the allergic reactions after taking Xolair, contact your doctor or hospital as soon as possible.Signs of allergic " +
                       "reaction after taking Xolair include rashes, difficulty in breathing, swelling or fainting like episodes.There is a certain type of " +
                       "allergic reaction called serum sickness after taking Xolair. Symptoms of serum sickness include joint pain with or without swelling " +
                       "or stiffness, rash, fever, swollen lymph nodes and muscle pain. "),


        //7


        new FAQDrugSAA("What are the precautions to be kept in mind after having Xolair?",
                       "Low incidence of allergic reactions including anaphylaxis and anaphylactic shock have been reported with Xolair treatment. Most of " +
                       "these reactions occur within 2 hours after Xolair injection. Therefore, treatment for anaphylaxis should always be available for use " +
                       "following Xolair administration. Further, all patients should be informed prior to taking the drug that allergy is possible and emergency" +
                       " medical treatment without any delay should be taken in case allergic reactions occur. It has been recommended that all patients receiving " +
                       "Xolair should be observed for an appropriate period of time, post the injection."),


        //8


        new FAQDrugSAA("Will I be under observation after my Xolair injections?",
                       "An observation period of 2 hours for the first 3 injections, and 30 minutes for subsequent injections, has been recommended after " +
                       "the Xolair administration. "),


        //9


        new FAQDrugSAA("Is Xolair associated with an increased risk of cancer in children or adults?",
                       "No. There is no association between Xolair treatment and risk of cancer. There is no relationship between omalizumab " +
                       "therapy and cancer."),


        //10


        new FAQDrugSAA("Can Xolair be given during pregnancy?",
                       "Xolair should be given during pregnancy only if clearly needed."),


        //11


        new FAQDrugSAA("Can Xolair be given to a individual with kidney problems?",
                       "Yes. However, Xolair should be given with caution in these individuals."),


        //12


        new FAQDrugSAA("Can Xolair be given to a individual with liver problems?",
                       "Yes. However, Xolair should be given with caution in these individuals."),


        //13


        new FAQDrugSAA("Is any dose adjustment required for an individual with kidney problems?",
                       "No. No particular dose adjustment is recommended. However, it should be given with caution in individuals with kidney problems. "),


        //14


        new FAQDrugSAA("Is any dose adjustment required for an individual with liver problems?",
                       "No. No particular dose adjustment is recommended. However, it should be given with caution in individuals with liver problems. "),


        //15


        new FAQDrugSAA("Can a person develop anti-omalizumab antibodies following treatment with Xolair?",
                       "It has been seen in clinical trials, that antibodies to omalizumab were detected in low numbers in treated patients concluding that" +
                       " the risk of immunogenicity is low with Xolair."),


        //16


        new FAQDrugSAA("Is there anything I need to worry about while traveling after receiving a Xolair injection?",
                       "There is nothing much to worry about during travel while you are on Xolair therapy provided you do not miss a scheduled dose of the " +
                       "injection. Discuss rescheduling an appointment with a doctor if needed. If you are traveling to countries with high prevalence of  " +
                       "helminth (worm) infections, such as those in sub-Saharan Africa, the Americas, China and East Asia; you might be at risk of catching " +
                       "a worm infection. Discuss with your doctor what you can do in order to avoid a helminth infection. "),


        //17


        new FAQDrugSAA("Do I need to undergo a blood test before taking Xolair?",
                       "Your doctor will advise you to undergo a skin test and/or a blood test to check:1. if your asthma is induced by allergy or other " +
                       "reasons 2. if the allergen that is causing you trouble is seasonal or present throughout the year 3. the dose required for starting " +
                       "Xolair based on IgE levels in the blood"),


        //18


        new FAQDrugSAA("Will I see any changes on my skin at the injection site?",
                       "You may sometimes experience redness, warmth, burning or itching sensation at the site of injection. This may occur within one hour " +
                       "of injection and can last for a few days. Inform your doctor if you notice any of these symptoms. The intensity and frequency of these " +
                       "injection site reactions will decrease as you receive more injections."),


        //19


        new FAQDrugSAA("Will I get a fever after having a Xolair injection?",
                       "Injection of Xolair will not cause fever ideally. But, few people have reported incidents of fever, rash and joint pain within 1 to " +
                       "5 days of taking Xolair. Please meet your doctor if you develop fever or any other discomfort after taking Xolair. "),


        //20


        new FAQDrugSAA("Can I take alcohol or tobacco occasionally when I am on Xolair therapy?",
                       "It is not known whether Xolair will interact with alcohol or tobacco. Please discuss the safety of taking alcohol or tobacco while " +
                       "using Xolair therapy with your doctor."),


        //21


        new FAQDrugSAA("Can Xolair be given to diabetic patients with asthma?",
                       "Yes, Xolair can be given to diabetic patients with severe allergic asthma. However, since Xolair contains sucrose, blood glucose " +
                       "levels may rise after injection with Xolair. Please inform your doctor if you have diabetes. Your doctor may adjust the dose of Xolair " +
                       "if required."),


        //22


        new FAQDrugSAA("Can Xolair lower my immunity?",
                       "No, Xolair contains omalizumab which is not associated with an increased risk of infections or a reduction in immunity."),


        //23


        new FAQDrugSAA("Will Xolair therapy lead to weight gain?",
                       "Treatment with Xolair is generally not expected to cause any weight gain, but may happen in some people. "),


        //24


        new FAQDrugSAA("Will I feel drowsy after taking Xolair?",
                       "Although not common, some people may experience tiredness or dizziness after receiving a Xolair injection. If you do feel tired or dizzy on the " +
                       "day you receive Xolair, refrain from driving or operating heavy machinery until your condition improves. "),


        //25


        new FAQDrugSAA("Can Xolair therapy cause hair loss?",
                       "Frequency of hair loss after Xolair therapy cannot be estimated from the available data. "),


        //26


        new FAQDrugSAA("Can Xolair cause indigestion?",
                       "Although it is not common, you may experience nausea, diarrhea or indigestion after taking Xolair. Contact your doctor immediately " +
                       "if you do experience a combination of symptoms such as loss of appetite, tiredness and numbness or tingling sensation in the arms" +
                       "and legs and raised patches on skin. "),


        //27


        new FAQDrugSAA("Will I get headache after receiving a Xolair injection?",
                       "Some patients may experience a headache after receiving a Xolair injection. The headache is usually mild-to-moderate in intensity. " +
                       "Please talk to your doctor if you get a headache after taking Xolair. "),


        //28


        new FAQDrugSAA("What should I do to reduce the itchiness and redness at the injection site after taking Xolair?",
                       "If you experience any mild itchiness or swelling at the injection site of Xolair, you may apply an ice pack or cold compress to it." +
                       " Do not take any over-the-counter medications without the consent of your doctor to relieve the itching and swelling. Call your doctor " +
                       "if the itchiness and swelling does not subside after a few hours."),


        //29


        new FAQDrugSAA("Can Xolair affect my child's normal growth?",
                       "Xolair has been tested to be safe and beneficial in treating children with severe allergic asthma. There is no scientific evidence " +
                       "suggesting any negative impact on the growth and development of children receiving Xolair injections. Studies have shown that taking " +
                       "Xolair improved the quality of life in children with allergic asthma not controlled by other medications. "),


        //30


        new FAQDrugSAA("Is it safe to take Xolair for many years?",
                       "Xolair is intended for long-term treatment of severe allergic asthma. It is generally safe and well-tolerated by the majority " +
                       "of patients. You need not worry if your doctor advises you to continue Xolair treatment for many years."),

    };


    //TREATMENT EFFECT OF XOLAIR IN ASTHMA


    public static List<FAQDrugSAA> treatmentofXolair = new List<FAQDrugSAA> 
    {


        //1


        new FAQDrugSAA("How can Xolair help me?",
                       "Adding Xolair may help reduce the number of asthma attacks you have. Xolair has also been shown to help reduce daytime and night-time" +
                       " symptoms.Daytime symptoms include: chest tightness, shortness of breath, coughing and wheezing.Night-time symptoms include awakenings " +
                       " and breathing problems requiring rescue medication use."),


        //2


        new FAQDrugSAA("Does Xolair help in reducing my day-time symptoms?",
                       "Yes. Xolair is indicated as an add-on therapy to improve asthma control in patients with severe persistent allergic asthma with " +
                       "frequent daytime symptoms."),


        //3


        new FAQDrugSAA("Will taking Xolair help me to reduce my night-time awakenings?",
                       "Yes. Xolair is indicated as an add-on therapy to improve asthma control in patients with frequent night-time awakenings."),


        //4


        new FAQDrugSAA("Will I see immediate improvement after taking Xolair?",
                       "You may not see an immediate improvement in your asthma symptoms after beginning Xolair therapy. It usually takes between 12 and " +
                       "16 weeks to have the full effect."),


        //5


        new FAQDrugSAA("How long does Xolair take to show the effect?",
                       "You may not see an immediate improvement in your asthma after beginning Xolair therapy. It usually takes between 12 and 16 weeks" +
                       " to have the full effect."),


        //6


        new FAQDrugSAA("Does Xolair provides instant relief from asthma?",
                       "No. Xolair is intended for long-term treatment. Patients should be assessed for treatment effectiveness 16 weeks after initiating Xolair. " +
                       "The decision to continue it should be based on whether a marked improvement in overall asthma control is seen as dose reduction at" +
                       " 6 months results in recurrence of asthma symptoms."),


        //7


        new FAQDrugSAA("Will I have to take Xolair indefinitely?",
                       "No. A small study of patients has reported that the beneficial effects of 6 years of Xolair treatment were sustained for 3 years after " +
                       "withdrawal. There has been a report indicating that Xolair treatment may not need to be continued indefinitely. "),


        //8


       new FAQDrugSAA("For how long will the beneficial effects of long term Xolair treatment last?",
                       " A small study of patients has reported that the beneficial effects of 6 years of Xolair treatment were sustained for 3 years after " +
                       "withdrawal. There has been a report indicating that Xolair treatment may not need to be continued indefinitely. Discontinuation " +
                       "generally results in a return  of asthma associated symptoms. However, further research is required."),


        //9


        new FAQDrugSAA("Do different formulations have different effects?",
                       "No. Both formulations have same effect on the body."),


        //10


       new FAQDrugSAA("How long  will it take to see the effect of Xolair?",
                       "Xolair is meant for long-term treatment, and signs of improvement in asthma symptoms may take at least 12–16 weeks to manifest."),



        //11


        new FAQDrugSAA("Are there any predictors of my response to Xolair?",
                       "Yes. The estimation of allergic antibody (IgE) level in your blood at baseline can help in predicting how you will respond to Xolair " +
                       "treatment. It has been shown in randomized clinical trials that physicians' overall assessment after 16 weeks of treatment is the " +
                       "most meaningful measure of treatment response."),


        //12


        new FAQDrugSAA("How will Xolair benefit me?",
                       "If you are suffering from severe persistent allergic asthma, continuous treatment with Xolair will bring down the asthma symptoms " +
                       "and reduce the frequency of asthma attacks thereby improve your quality of life. You may also benefit from a gradual reduction in" +
                       " your doses of asthma inhalers and rescue medications. Allergic reactions will also appear less frequently with regular use of Xolair. "),


        //13


        new FAQDrugSAA("Why is my asthma not improving after 16 weeks of Xolair injections?",
                       "After 16 weeks of Xolair therapy, your doctor will evaluate your response to Xolair therapy. Xolair has been proven to reduce asthma " +
                       "symptoms and episodes in most patients by 16 weeks. However, some patients may be late responders and would need more sessions to " +
                       "achieve improvement in symptoms. It is recommended to continue the Xolair therapy as per your doctor's suggestion. "),


        //14


        new FAQDrugSAA("For how many days does the effect of one Xolair injection stay in the body?",
                       "Once injected, the medicinal effect of a Xolair injection will stay in your body for up to 22 days. Hence, the doctor will advise you " +
                       "to receive one injection every 4 weeks. However, some patients may require more than 300 mg a month depending on their age, body weight " +
                       "and severity of asthma. Such patients will have to receive Xolair injections every 2 weeks. "),


        //15

       
        new FAQDrugSAA("Why cannot I see any change in asthma symptoms on the first day of a Xolair injection?",
                       "After Xolair is injected into the skin, one dose of the drug will take about a week to get completely absorbed and reach " +
                       "the blood stream. Therefore, it will take about 7 days for Xolair to start its action on circulating immunoglobulins."),
  

        //16


           new FAQDrugSAA("What is the success rate of Xolair therapy in the treatment of allergic asthma?",
                       "A post-marketing survey conducted across various countries, including France, Germany, Belgium and Italy, has shown remarkable" +
                       " benefit among patients with severe allergic asthma who received Xolair for 5 to 12 months. Additionally, results from various " +
                       "clinical studies across the globe have also confirmed the benefit of taking Xolair therapy for allergic asthma. The patients who " +
                       "received Xolair regularly reported lesser rates of asthma symptoms and asthma attacks. They also reported significant improvement " +
                       "in their quality of life and were able to reduce their need for inhalers and rescue medications gradually. "),


        //17


        new FAQDrugSAA("Is Xolair a cost effective therapy for allergic asthma?",
                      "Regular use of Xolair injections has been shown to reduce the frequency of severe asthma attacks and the need for rescue medications " +
                       "by patients. Patients also reported a reduced incidence of emergency room visits and hospitalizations. Patients were additionally" +
                       " able to reduce their dosages of other asthma medications and inhalers gradually. Considering these responses, we believe that Xolair " +
                       "therapy is a cost-effective long-term treatment in patients with severe uncontrollable asthma. Xolair therapy can also improve the " +
                       "quality of life in such patients.")
     
    };


    //USEOFXOLAIR


    public static List<FAQDrugSAA> useofXolair = new List<FAQDrugSAA>
    { 
    

        //1


        new FAQDrugSAA("Who can take Xolair?",
                      "Adults, adolescents and children who are 6 years of age and older can be prescribed with Xolair. It is indicated as an add-on therapy " +
                       "to improve asthma control in patients with severe persistent allergic asthma.Xolair can prevent asthma from getting worse by controlling " +
                       "symptoms of severe persistent allergic asthma such as frequent daytime symptoms or night-time awakenings, or a reduced lung function " +
                       "(FEV1 <80%) or multiple severe asthma exacerbations despite taking their daily medications.Severe persistent allergic asthma patients " +
                       "have either a positive skin test or a perennial aeroallergen reaction."),


        //2


        new FAQDrugSAA("Is Xolair indicated for geriatric use?",
                      "There are limited data available on the use of Xolair in patients older than 65 years but there is no evidence that elderly" +
                       "patients require a different dose from younger adult patients."),


        //3


        new FAQDrugSAA("Is Xolair indicated for pediatric use?",
                      "The safety and efficacy of Xolair in children below age 6 have not been established. No data are available."),


        //4


        new FAQDrugSAA("Who can receive/take Xolair?",
                      "Xolair is indicated for adults and children of 6 years of age and above with moderate to severe persistent allergic asthma whose " +
                       "symptoms are inadequately controlled with steroid inhalers."),


        //5


        new FAQDrugSAA("What other diseases is Xolair used for?",
                      "In addition to allergic asthma, Xolair is used in the treatment of long standing hives (raised itchy skin rash) that occurs due " +
                       "to unknown causes. Chronic Spontaneous Urticaria CSU"),

    };


    //Contraindications


    //1


    public static List<FAQDrugSAA> contraIndications = new List<FAQDrugSAA> 
    {
    
    
        //1


        new FAQDrugSAA("What are the contra-indications of using Xolair?",
                      "Xolair should not be given if there is hypersensitivity to the active substance or to any of the constituents (Sucrose, " +
                       "L-histidine, L-histidine hydrochloride monohydrate, Polysorbate 20). "),

    
        //2


        new FAQDrugSAA("What are the conditions in which Xolair is not used?",
                      "Xolair is not indicated for the treatment of acute asthma exacerbations, acute bronchospasm or status asthmaticus. It has not been " +
                       "studied in patients with hyperimmunoglobulin E syndrome or allergic bronchopulmonary aspergillosis or for the prevention of " +
                       "anaphylactic reactions, including those provoked by food allergy, atopic dermatitis, or allergic rhinitis. Xolair is not indicated " +
                       "for the treatment of these conditions. Xolair therapy has not been studied in patients with autoimmune diseases, immune complex-mediated " +
                       "conditions, or pre-existing renal or hepatic impairment. "),

    
        //3


        new FAQDrugSAA("To whom should Xolair not be given?",
                      "Whenever there is a hypersensitivity to the active substance or to any of the excipients, Xolair is contra-indicated."),

    

    };


    //Drug interactions


    public static List<FAQDrugSAA> drugInteraction = new List<FAQDrugSAA> 
    {
    
    
        //1


        new FAQDrugSAA("What are the interactions of Xolair with other medicinal products?",
                      "Since IgE may be involved in the immunological response to some helminth infections, Xolair may indirectly reduce the efficacy " +
                       "of medicinal products for the treatment of helminthic or other parasitic infections"),

    
        //2


        new FAQDrugSAA("I have been taking Xolair for sometime now and my symptoms seem to have improved. Can I stop my concurrent medications like " +
                       "steroids and anti-leukotrienes?",
                      "The other existing medications for asthma control such as oral steroids, inhalers containing steroids and anti-leukotrienes should " +
                       "not be abruptly stopped. "),

    
        //3


        new FAQDrugSAA("Can I reduce the number of my other concurrent medications after my symptoms have improved following treatment with Xolair?",
                      "There has been reports that Xolair treatment can significantly reduce the need for steroids by injections or inhalers.  In some " +
                       "reports, it was seen that patients on Xolair were able to completely withdraw steroid medications too. Along with that it has " +
                       "been seen that the dose of inhaled/oral steroids is also significantly reduced from baseline.However, it should be kept in mind " +
                       "that dose reduction or discontinuation of concurrent medications should always be  done under the direct guidance of a physician."),

    
        //4


        new FAQDrugSAA("What is the procedure for reduction in my steroid medications after my symptoms have improved following treatment with Xolair?",
                      "Inhalers or injections containing steroids should not be discontinued abruptly upon initiation of Xolair. If the patient's asthma " +
                       "is well under control, then these concurrent medications can be gradually reduced. However, this should be done under the direct " +
                       "guidance of a physician."),

    
        //5


        new FAQDrugSAA("Can I abruptly stop my steroid inhaler or injections after initiating treatment with Xolair?",
                      "No, you should not stop your inhalers or injections containing steroids abruptly after initiating treatment with Xolair.  However," +
                       " they can be reduced gradually, provided your asthma is well controlled and it is done under the direct guidance of a physician."),

    
        //6


        new FAQDrugSAA("Do I need to consult my doctor or physician before reducing my steroid inhaler or injections after initiating treatment with Xolair?",
                      "Yes, you should consult your doctor or physician before reducing the dose of steroid inhaler or injections after initiating treatment " +
                       "with Xolair. You should not stop your inhalers or injections containing steroids abruptly after initiating treatment with Xolair."),

    
        //7


        new FAQDrugSAA("Can I stop using my asthma inhaler after starting Xolair?",
                      "Do not discontinue your asthma medications or inhalers abruptly upon initiation of Xolair therapy. Treatment with Xolair will " +
                       "take few weeks to show an effect and control your asthma symptoms. Your doctor will evaluate your condition and may reduce the " +
                       "dosage of your asthma medications gradually during subsequent visits after starting Xolair."),

    
        //8


        new FAQDrugSAA("Can I take vitamin tablets while receiving Xolair therapy?",
                      "It is important to tell your doctor which vitamin tablets you are taking while receiving Xolair therapy. There is no scientific" +
                       " evidence of any potential interaction between Xolair and other medications or vitamin supplements. "),

    };


 


}
