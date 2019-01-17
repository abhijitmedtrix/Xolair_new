
using System.Collections.Generic;


public class CSUFAQDRUG : faQ.FAQ
{
    public CSUFAQDRUG(string question,string answer)
    {

        this.Question = question;
        this.Answer = answer;
    }

    public static List<CSUFAQDRUG> XolairAndCSU = new List<CSUFAQDRUG>{

        //1
        new CSUFAQDRUG("What is the goal of treatment for CSU?",
                   "The goal of CSU treatment is complete symptom control, which means no more hives,itching or deep tissue swelling (angioedema)."),

        //2

        new CSUFAQDRUG("How are hives/rashes treated?",
                   "First, the doctor will prescribe antihistamines. You will start taking 1 dose daily for 2 weeks. If symptoms are still " +
                   "aggravated, the doctor will double the daily dose. If the symptoms don't get better, the dose can be increased to up to 4 times a day." +
                   " After taking such a high dose of antihistamines," +
                   " if the symptoms are not adequately controlled, the doctor will begin treating the patient with Xolair; " +
                   "by subcutaneous (under-the-skin) injection once every 4 weeks."),

        //3


        new CSUFAQDRUG("What are antihistamines?",
                   "Antihistamines are the first line of treatment for CSU and they work by blocking markers found on the surface of cells, known as " +
                   "receptors, preventing histamine from binding and affecting the cell. However, up to half of CSU patients have an inadequate response " +
                   "to H1-antihistamines at approved doses."),

        //4

        new CSUFAQDRUG("Do all patients respond to treatment with antihistamines?",
                   "No. It has been seen that up to half of CSU patients have an inadequate response " +
                   "to H1-antihistamines at approved doses. And hence additional medications are required."),       

        //5

        new CSUFAQDRUG("How do anti-histamines help to treat CSU?",
                   "Antihistamines bind to proteins found on the cells called receptors, blocking them, " +
                   "thus preventing histamines from binding with these receptors and starting the allergic reaction."),

        //6

        new CSUFAQDRUG("What is Xolair?",
                   "Xolair is used for the treatment of CSU. The active substance of Xolair is omalizumab. Omalizumab is a protein that " +
                   "is similar to natural proteins produced by the body; it belongs to a class of medicines called monoclonal antibodies."),

        //7

        new CSUFAQDRUG("What should I keep in mind before starting treatment with Xolair?",
                   "An important part of starting treatment is understanding how it is given and what you should expect during the administration process."),


        //8

        new CSUFAQDRUG("Are steroids used in the treatment of hives?",
                   "A short course of oral corticosteroids may be occasionally prescribed for the treatment of severe cases of urticaria and for the acute" +
                   " exacerbations of CSU. According to guideline recommendations a short course of steroid treatment of a maximum of upto 10 days may be " +
                   "helpful to reduce disease activity/duration. Long term use of corticosteroids are not recommended."),

        //9

        new CSUFAQDRUG("How can I avoid triggers which cause hives in inducible urticaria?",
                   "If the cause of hives can be identified, the best treatment is to avoid the trigger or eliminate it. To achieve this, following these steps:"+
"1. Don’t eat foods that have been identified as causes of your symptoms."+
"2. Avoid harsh soaps. Frequent baths may reduce itching and scratching, which would be beneficial because itching and scratching can make the hives feel worse."+
"3. Avoid tight clothing. Pressure hives can be relieved by wearing loose-fitting clothes."+
"4. If you develop hives when exposed to cold, do not swim alone in cold water and always carry an epinephrine autoinjector. Avoid exposure to cold air and use" +
" a scarf around your nose and mouth in cold weather. If you must be out in the cold, wear warm clothing."+
"5. Wear protective clothing; apply sunblock.6. Notify your physician or pharmacist immediately if you suspect that a specific medication is causing your hives."),

        //10


        new CSUFAQDRUG("What is the recommended treatment algorithm for chronic urticaria?",
                  "The International EAACI/GA2LEN/EDF/WAO guidelines for the management of CSU, updated in 2013 and published in 2014 recommend first-line " +
                   "treatment with a second generation H1-antihistamine at the licensed dose. Second-line treatment with a second generation H1-antihistamine " +
                   "at up to 4-fold increased dosage. Third-line treatment, of CSU refractory to high-dose H1-antihistamine therapy, by addition of either" +
                   "omalizumab, cyclosporin A or montelukast to existing H1-antihistamine treatment."),

        //11

        new CSUFAQDRUG("What will happen if I stop my CSU treatment?",
                  "Do not stop treatment with Xolair unless instructed to do so by your doctor. Interrupting or stopping treatment with Xolair may cause " +
                   "CSU symptoms to return. However, if you are being treated for CSU, your doctor may stop Xolair treatment from time to time so that your" +
                   "symptoms can be assessed. Follow your doctor’s instructions."),

        //12

        new CSUFAQDRUG("Can a person develop antibodies to Xolair?",
                  "Antibodies to omalizumab have been detected in a low number of patients in clinical trials. The clinical relevance of anti-Xolair antibodies" +
                   " is not well understood."),

        //13

        new CSUFAQDRUG("Can Xolair treatment affect a person's ability to drive and operate machinery?",
                  "It is unlikely that Xolair will affect your ability to drive and use machines."),

        //14

        new CSUFAQDRUG("How does Xolair help in the treatment of CSU?",
                   "It is thought that Xolair works by inhibiting the pathway that causes histamine to be released from mast cells.")

    };

    public static List<CSUFAQDRUG> DosageAdministration = new List<CSUFAQDRUG>
    {


        //1


        new CSUFAQDRUG("How is Xolair administered for CSU treatment?",
                   "Xolair is administered by a doctor or nurse in the form of an injection under the skin on the upper region of the arm, every four weeks. " +
                   "You will receive two 150 mg injections of Xolair and each injection will take between 5 and 10 seconds to administer. After you receive your " +
                   "injections, your doctor or nurse will monitor you for a period of time for signs of any sudden severe allergic reactions (anaphylaxis)."),


        //2


        new CSUFAQDRUG("What are the doses of Xolair for CSU in the market?",
                   "Xolair is available as 150 mg by subcutaneous injection, administered every 4 weeks."),


        //3


        new CSUFAQDRUG("How frequently do I need Xolair injections?",
                   "Xolair is administered as a subcutaneous injection every 4 weeks. The recommended dose is 300 mg which is given as 2 injections every " +
                   "4 weeks. The appropriate duration of therapy for CIU has not been evaluated. Your doctor will periodically reassess the need for " +
                   "continued therapy."),


        //4


        new CSUFAQDRUG("I am obese, will the dosing of Xolair for treating urticaria differ for me?",
                   "No. Dosing of Xolair for the treatment of CSU is not dependent on body weight."),


        //5


        new CSUFAQDRUG("What is the maximum dose of Xolair that can be administered per injection site?",
                   "It is advised not to administer more than 150 mg of Xolair per injection site."),


        //6


        new CSUFAQDRUG("How is a dose of 300 mg of Xolair administered? ",
                   "Administration of more than 150 mg of Xolair can be done by dividing such doses into two or more injections."),


        //7


        new CSUFAQDRUG("What is the maximum tolerated dose of Xolair?",
                   "The maximum tolerated dose of Xolair has not been determined. Single intravenous doses of up to 4,000 mg have been administered to patients" +
                   " without evidence of dose limiting toxicities. The highest cumulative dose administered to patients was 44,000 mg over a 20 week period, " +
                   "which was not associated with any untoward acute effects."),


        //8


        new CSUFAQDRUG("Would I need to change my Xolair dose as I age?",
                   "Clinical trials demonstrate that in CSU patients with Xolair no dose adjustments are necessary for age (12 to 75 years), race/ethnicity," +
                   "gender, body weight, body mass index or baseline IgE level."),


        //9


        new CSUFAQDRUG("Do women on Xolair require dose adjustment?","Clinical trials demonstrate that in CSU patients with Xolair no dose adjustments are necessary " +
                   "for age (12 to 75 years), race/ethnicity, gender, body weight, body mass index or baseline IgE level."),


        //10


        new CSUFAQDRUG("Do I need to do a blood test before initiating Xolair treatment for CSU?",
                   "In patients with chronic hives, a blood test is not necessary to determine the dose or dosing frequency. Analyses suggest that " +
                   "no dose adjustments are necessary in patients with CSU for age (12-75 years), race/ethnicity, gender, body weight, body mass index " +
                   "and baseline IgE."),


        //11


        new CSUFAQDRUG("Can I administer Xolair injection by myself.",
                   "There is limited experience with self-administration of Xolair. Therefore treatment is intended to be administered by a healthcare " +
                   "provider only."),


        //12


        new CSUFAQDRUG("How long does the Xolair reconstituted solution last?",
                   "Use the solution for subcutaneous administration within 8 hours following reconstitution when stored in the vial at 2°C to 8°C, or " +
                   "within 2 hours when stored at 25°C."),


        //13


        new CSUFAQDRUG("What do I do if I miss a dose of Xolair?",
                   "Contact your doctor or hospital as soon as possible to reschedule your appointment."),


        //14


        new CSUFAQDRUG("Where are Xolair injections given?",
                   "The injections are administered subcutaneously in the deltoid region of the arm. Alternatively, the injections can be administered " +
                   "in the thigh if there is any reason precluding administration in the deltoid region."),


        //15


        new CSUFAQDRUG("How long does it take to administer a Xolair injection?",
                   "It may take between 5 and 10 seconds to administer a Xolair solution by subcutaneous injection. This is because the Xolair solution " +
                   "is slightly viscous in nature."),


       

      };


    //PACK INFORMATION


    public List<CSUFAQDRUG> PackageInformation = new List<CSUFAQDRUG>{
       


        //1


        new CSUFAQDRUG("How is Xolair supplied in market?",
                   "Xolair is supplied as a lyophilized, sterile powder in a single-use vial without preservatives. Solvent for solution for injection is " +
                   "also supplied. The powder is reconstituted in the water before it is injected by a doctor or nurse."),



        //2


       
        new CSUFAQDRUG("How much Xolair can a vial hold?",
                   "One vial contains 150 mg of omalizumab. After reconstitution one vial contains 125 mg/ml of omalizumab (150 mg in 1.2 ml)."),



        //3


       
        new CSUFAQDRUG("How many times can the vial with Xolair be used?",
                   "Xolair 150 mg powder for solution for injection is supplied in a single-use vial."),



        //4


       
        new CSUFAQDRUG("Can Xolair be administered beyond its expiration date?",
                   "Do not use Xolair beyond the expiration date which is stated on the label."),



        //5


       
        new CSUFAQDRUG("How is Xolair stored?",
                   "Store Xolair under refrigerated conditions 2°C  to 8°C."),
    
    
    };


    //PRECAUTION AND WARNING

    public List<CSUFAQDRUG> Warning = new List<CSUFAQDRUG>
    {


        //1


        new CSUFAQDRUG("Are there any side effects of using Xolair? If yes, what can they be?",
                   "The most common side effects of Xolair that may affect up to 1 in 10 people are: headache, joint pain, respiratory tract infection, " +
                   "feeling of pressure or pain in the cheeks (sinusitis, sinus headache) and reactions at the injection site like pain, swelling, redness " +
                   "and itching."),


        //2


        new CSUFAQDRUG("Can Xolair overdose cause toxicities.",
                   "The maximum tolerated dose of Xolair has not been determined. Single intravenous doses of up to 4,000 mg have been administered to " +
                   "patients without evidence of dose limiting toxicities. The highest cumulative dose administered to patients was 44,000 mg over a " +
                   "20 week period, this dose did not result in any untoward acute effects. If an overdose is suspected, the patient should be monitored " +
                   "for any abnormal signs or symptoms. Medical treatment should be sought and instituted appropriately."),


        //3


        new CSUFAQDRUG("What are the things to be kept in mind while Xolair is being administered?",
                   "It is important that Xolair be administered only in a healthcare setting by healthcare providers who can manage anaphylaxis. It is " +
                   "advised that these patients must be observed closely for an appropriate period of time after administration of Xolair. Patients should " +
                   "also be informed about the signs and symptoms of anaphylaxis and instructed to seek immediate medical care should the signs and symptoms occur."),


        //4


        new CSUFAQDRUG("What should I do if I experience a side-effect?",
                   "As with all treatments, side effects can occur when you receive Xolair, although not everybody gets them. It is important that you " +
                   "understand what can occur and when to seek medical attention. If you think you are experiencing side effects, including any that are not " +
                   "listed here, talk to your doctor or nurse. Your doctor or nurse will be able to talk to you about any questions or concerns you may have " +
                   "about side effects."),


        //5


        new CSUFAQDRUG("Can Xolair be used in patients with liver problems?",
                   "There have been no studies on the effect of impaired renal or hepatic function on the pharmacokinetics of Xolair. Because omalizumab " +
                   "clearance at clinical doses is dominated by the reticular endothelial system (RES) it is unlikely to be altered by renal or hepatic " +
                   "impairment. While no particular dose adjustment is recommended for these patients, Xolair should be administered with caution."),


        //6


        new CSUFAQDRUG("My heartbeat has increased after taking Xolair? What could be the reason?",
                   "This could be a side-effect of Xolair called anaphylaxis. Anaphylaxis a serious side effect of Xolair may present sudden signs of allergy " +
                   "or combination of signs such as rash, itching or hives on the skin, swelling of the face, lips, tongue, larynx (voice box), windpipe or other" +
                   " parts of the body, fast heart beat, dizziness and light-headedness, shortness of breath, wheezing or trouble breathing. In case you " +
                   "experience any of these, it is best to seek immediate medical care from your healthcare provider."),


        //7


        new CSUFAQDRUG("Why do I experience  a sudden difficulty in breathing after Xolair injections?",
                   "This could be due to a serious side-effect of Xolair called anaphylaxis. Anaphylaxis a serious side effect of Xolair may present sudden" +
                   " signs of allergy or combination of signs such as rash, itching or hives on the skin, swelling of the face, lips, tongue, larynx (voice box)," +
                   " windpipe or other parts of the body, fast heart beat, dizziness and light-headedness, shortness of breath, wheezing or trouble breathing. " +
                   "In case you experience any of these, it is best to seek immediate medical care from your healthcare provider."),

        //8


        new CSUFAQDRUG("Can I feel dizzy following Xolair injections?",
                   "Fainting could be a symptom of an allergic reaction, a side effect of taking Xolair. Anaphylaxis a serious side effect of Xolair may " +
                   "present sudden signs of allergy or combination of signs such as rash, itching or hives on the skin, swelling of the face, lips, tongue, " +
                   "larynx (voice box), windpipe or other parts of the body, fast heart beat, dizziness and light-headedness, shortness of breath, wheezing or " +
                   "trouble breathing. In case you experience any of these, it is best to seek immediate medical care from your healthcare provider."),


        //9


        new CSUFAQDRUG("Why do I experience swelling in my tongue or throat following Xolair injections?",
                   "This could be a side-effect of Xolair called anaphylaxis. Anaphylaxis a serious side effect of Xolair may present sudden signs of allergy " +
                   "or combination of signs such as rash, itching or hives on the skin, swelling of the face, lips, tongue, larynx (voice box), windpipe or" +
                   "other parts of the body, fast heart beat, dizziness and light-headedness, shortness of breath, wheezing or trouble breathing. In case you " +
                   "experience any of these, it is best to seek immediate medical care from your healthcare provider."),


        //10


        new CSUFAQDRUG("Can side-effects of Xolair be dangerous?",
                   "Serious side effects like anaphylaxis and anaphylactic shock may occur when taking Xolair. Patients should be informed that such " +
                   "reactions are possible and prompt medical attention should be sought if allergic reactions occur. Therefore medicinal products for the " +
                   "treatment of anaphylactic reactions should always be available for immediate use following administration of Xolair."),


         //11


        new CSUFAQDRUG("How long would I have increased serum total IgE following treatment discontinuation?",
                   "Elevated serum total IgE levels may persist for up to one year following discontinuation of Xolair."),


        //12


        new CSUFAQDRUG("Is there any risk of cancer with Xolair?",
                   "Results from the EXCELS study with a median follow up of 5 years over 7000 patients suggest that Xolair is not associated with an " +
                   "increased risk of malignancy."),


        //13


        new CSUFAQDRUG("Why do I have swelling around the area Xolair is injected?",
                   "This could be a side-effect of Xolair treatment. The various types of injection site reactions include swelling, redness, pain and itching." +
                   " It is best to seek immediate medical care from your healthcare provider."),


        //14


        new CSUFAQDRUG("Why do I have redness around the area where Xolair is injected?",
                   "This could be a side-effect of Xolair treatment. The various types of injection site reactions include swelling, redness, pain and itching." +
                   " It is best to seek immediate medical care from your healthcare provider."),


        //15


        new CSUFAQDRUG("Why do I have unbearable pain around the area where Xolair is injected?",
                   "This could be a side-effect of Xolair treatment. The various types of injection site reactions include swelling, redness, pain and itching. " +
                   "It is best to seek immediate medical care from your healthcare provider."),


        //16


        new CSUFAQDRUG("Why do I have itching around the area where Xolair is injected?",
                   "This could be a side-effect of Xolair treatment. The various types of injection site reactions include swelling, redness, pain and itching. " +
                   "It is best to seek immediate medical care from your healthcare provider."),


        //17


        new CSUFAQDRUG("Does the use of Xolair cause hair loss?",
                   "It is not clearly known if Xolair can cause hair loss. It is best to seek medical care from your healthcare provider. "),

        //18


        new CSUFAQDRUG("Can Xolair affect my platelet count?",
                   "In clinical trials few patients had platelet counts below the lower limit of the normal laboratory range. None of these changes were " +
                   "associated with bleeding episodes or a decrease in haemoglobin. No pattern of persistent decrease in platelet counts, as observed in" +
                   "non-human primates, has been reported in humans (patients above 6 years of age), even though isolated cases of idiopathic thrombocytopenia, " +
                   "including severe cases, have been reported in the post-marketing setting."),


        //19


        new CSUFAQDRUG("What should I tell my healthcare provider before receiving Xolair?",
                   "Before receiving XOLAIR, tell your doctor, pharmacist or nurse if you are taking, have recently taken or might take any other medicines."+
                  "This is especially important if you are taking:- medicines to treat an infection caused by a parasite, as Xolair may reduce the effect of your medicines,"+
                   "- inhaled corticosteroids and other medicines for allergic asthma"),


        //20


        new CSUFAQDRUG("What is the most important information I should know about Xolair's serious side effects?",
                   "There are a few things you would need to know about Xolair. Xolair may cause some serious side effects which can include sudden severe " +
                   "allergic reactions which can include sudden signs of allergy or combination of signs such as rash, itching or hives on the skin, swelling " +
                   "of the face, lips, tongue, larynx (voice box), windpipe or other parts of the body, fast heart beat, dizziness and light-headedness, " +
                   "shortness of breath, wheezing or trouble breathing. Systemic lupus erythematosus is another serious side effect of Xolair. Symptoms may " +
                   "include muscle pain, joint pain and swelling, and rash."),


        //21


        new CSUFAQDRUG("Do I need to be monitored when I am taking Xolair.",
                   "After you receive an injection, your doctor or nurse will monitor you for a period of time for signs of any sudden and severe allergic reactions (anaphylaxis)."),


        //22


        new CSUFAQDRUG("Are the adverse effects dose dependent?",
                   "It is not known whether the adverse events seen after Xolair are dose dependent. However, in clinical trials it was observed that " +
                   "serious adverse effects were more common in the group treated with the highest dose of Xolair."),


        //23


        new CSUFAQDRUG("What is most severe form of an allergic reaction called? ",
                   "Anaphylaxis is the most severe side effect of Xolair. It is a severe allergic reaction which would occur immediately after you receive" +
                   " your injection or over the following days."),


        //24


        new CSUFAQDRUG("Is anaphylaxis common after Xolair?",
                   "Anaphylaxis can affect up to 1 in 1,000 people, and in clinical trials with over 700 CSU patients treated with Xolair, there were no cases found."),


        //25


        new CSUFAQDRUG("Do I need medical attention if I experience anaphylaxis?",
                   "Yes. If you experience any symptoms of anaphylaxis or any allergic reaction, you must seek immediate medical attention."),


        //26


        new CSUFAQDRUG("How would I know if I have anaphylaxis?",
                   "The symptoms of anaphylaxis would include: wheezing, shortness of breath, dizziness or fainting, itchiness or hives that feel or look " +
                   "different from your CSU symptoms, abdominal pain, nausea and vomiting, swelling of the throat or tongue, tightness of the throat, " +
                   "hoarseness or trouble swallowing."),


        //27


        new CSUFAQDRUG("Can Xolair be used in patients with kidney problems?",
                       "There have been no studies on the effect of impaired renal or hepatic function on the pharmacokinetics of Xolair. Because omalizumab " +
                       "clearance at clinical doses is dominated by the reticular endothelial system (RES) it is unlikely to be altered by renal or hepatic " +
                       "impairment. While no particular dose adjustment is recommended for these patients, Xolair should be administered with caution."),


        //28


        new CSUFAQDRUG("Does Xolair affect fertility?",
                       "There are no human fertility data for omalizumab. In specifically-designed non-clinical fertility studies in non-human primates, " +
                       "including mating studies, no impairment of male or female fertility was observed following repeated dosing with omalizumab at dose " +
                       "levels up to 75 mg/kg. Furthermore, no genotoxic effects were observed in separate non-clinical genotoxicity studies"),


        //29


        new CSUFAQDRUG("What should I do if a Xolair overdose is suspected?",
                       "If an overdose is suspected, the patient should be monitored for any abnormal signs or symptoms. Medical treatment should be sought" +
                       " and instituted appropriately."),


        //30


        new CSUFAQDRUG("What are the common GI side-effects of Xolair, especially seen in children?",
                       "In clinical trials in children 6 to <12 years of age, the most commonly reported adverse reactions were headache, pyrexia and " +
                       "upper abdominal pain. Most of the reactions were mild or moderate in severity."),

    };




    //TREATMENT OF XOLAIR



    public static List<CSUFAQDRUG> Treatment = new List<CSUFAQDRUG> 
    {


        //1


        new CSUFAQDRUG("Does Xolair help to improve my daily activities.",
                      "Yes. Clinical trials show that after 12 weeks of Xolair, patients with an inadequate response to H1-antihistamines achieved, " +
                       "on average, a 78% reduction in daily activity impairment with Xolair. "),


        //2


        new CSUFAQDRUG("How do we know if Xolair is effective?",
                      "In clinical trials, disease severity was measured by a weekly urticaria activity score (UAS7, range 0–42), which is a composite of " +
                       "the weekly itch severity score (range 0–21) and the weekly hive count score (range 0–21). All patients were required to have a " +
                       "UAS7 of ≥ 16, and a weekly itch severity score of ≥ 8 for the 7 days prior to randomization, despite having used an H1 antihistamine " +
                       "for at least 2 weeks."),


        //3


        new CSUFAQDRUG("Does Xolair help to improve hives?",
                      "Yes. Clinical trials show that after 12 weeks of Xolair treatment, patients with an inadequate response to H1-antihistamines achieved, " +
                       "on average, a 76% reduction in hives with Xolair. "),


        //4


        new CSUFAQDRUG("Does Xolair reduce itchiness?",
                      "Yes. Clinical trials show that after 12 weeks of Xolair, patients with an inadequate response to H1-antihistamines achieved, on average, " +
                       "a 71% reduction in itch with Xolair. "),


        //5


        new CSUFAQDRUG("Can I see immediate improvement in CSU symptoms once I start Xolair?",
                      "Evidence shows that there may not be immediate improvement in urticaria symptoms after beginning Xolair therapy."),


        //6


        new CSUFAQDRUG("Can Xolair be used for long-term treatment?",
                      "Clinical trial experience of long-term treatment of urticaria beyond 6 months is limited."),


        //7


        new CSUFAQDRUG("When can Xolair treatment effects be seen?",
                      "Xolair is intended for long-term treatment. Clinical trials have demonstrated that it takes at least 12-16 weeks for Xolair treatment " +
                       "to demonstrate that it is effective. At 16 weeks after commencing Xolair therapy patients should be assessed by their physician for " +
                       "treatment effectiveness before further injections are administered."),

    };


    //USE OF XOLAIR



    public static List<CSUFAQDRUG> UseOfXolair = new List<CSUFAQDRUG>
    {


        //1


        new CSUFAQDRUG("What is Xolair used for?",
                       "Xolair is used to treat CSU in adults and adolescents (12 years of age or older) who are already receiving antihistamines but whose" +
                       " CSU symptoms are not well controlled by these medicines."),


        //2


        new CSUFAQDRUG("What is the age group of patients who can receive Xolair?",
                       "Xolair is indicated for chronic idiopathic urticaria in adults and adolescents 12 years of age and older who remain symptomatic " +
                       "despite H1 antihistamine treatment. "),


        //3


        new CSUFAQDRUG("Is it safe to use Xolair in children below 12 years?",
                       "The safety and efficacy of Xolair in pediatric patients with CIU below 12 years of age have not been established. No data " +
                       "are available. Xolair is indicated for chronic idiopathic urticaria in adults and adolescents 12 years of age and older who " +
                       "remain symptomatic despite H1 antihistamine treatment."),


        //4


        new CSUFAQDRUG("Is Xolair safe in elderly patients?",
                       "There are limited data available on the use of Xolair in patients older than 65 years but there is no evidence that elderly patients " +
                       "require a different dose from younger adult patients."),


      };


    //CONTRA-INDICATIONS



    public List<CSUFAQDRUG> ContraIndications = new List<CSUFAQDRUG>
    {


        //1


        new CSUFAQDRUG("Can I use Xolair if I have an allergy to Xolair or any of its ingredients?",
                       "No, the use of Xolair is not advised for patients who develop hypersensitivity reaction to Xolair (active ingredient) or any of its excipients. "),


        //2


        new CSUFAQDRUG("If I have sudden acute asthma, will the use of Xolair affect me?",
                       "Xolair is not indicated for the treatment of acute asthma exacerbations, acute bronchospasm or status asthmaticus."),


        //3


        new CSUFAQDRUG("Can Xolair be used in patients with autoimmune diseases?",
                       "Xolair therapy has not been studied in patients with autoimmune diseases or immune complex-mediated conditions. Caution should be" +
                       " exercised when administering Xolair in these patient populations"),


        //4


        new CSUFAQDRUG("Is Xolair safe for pregnant women?",
                       "There are limited data from the use of omalizumab in pregnant women. Animal studies do not indicate direct or indirect harmful" +
                       " effects with respect to reproductive toxicity. Omalizumab crosses the placental barrier and the potential for harm to the foetus " +
                       "is unknown. Omalizumab has been associated with age-dependent decreases in blood platelets in non-human primates, with a greater " +
                       "relative sensitivity in juvenile animals. Xolair should not be used during pregnancy unless clearly necessary."),



        //5


        new CSUFAQDRUG("Is breastfeeding safe while using Xolair?",
                       "It is unknown whether omalizumab is excreted in human milk. Available data in non-human primates have shown excretion of omalizumab " +
                       "into milk. A risk to the newborns/infants cannot be excluded. Omalizumab should not be given during breast-feeding.")


    };



    //DrugInteraction



    public List<CSUFAQDRUG> DrugInteraction = new List<CSUFAQDRUG> 
    {


        //1


        new CSUFAQDRUG("Is it safe to use Xolair when I am on corticosteroids?",
                      "Limited data are available on the use of Xolair in combination with specific immunotherapy (hypo-sensitisation therapy). In a clinical " +
                       "trial where Xolair was co-administered with immunotherapy, the safety and efficacy of Xolair in combination with specific immunotherapy " +
                       "were found to be no different to that of Xolair alone."),


        //2


        new CSUFAQDRUG("Can Xolair interact with other immunosuppressive drugs?",
                      "Limited data are available on the use of Xolair in combination with specific immunotherapy (hypo-sensitisation therapy). Cytochrome " +
                       "P450 enzymes, efflux pumps and protein-binding mechanisms are not involved in the clearance of omalizumab; thus, there is little " +
                       "potential for drug-drug interactions. In a clinical trial where Xolair was co-administered with immunotherapy, the safety and efficacy " +
                       "of Xolair in combination with specific immunotherapy were found to be no different to that of Xolair alone."),


        //3


        new CSUFAQDRUG("Can I decrease the dose of other medications for urticaria once I am on Xolair?",
                      "You are not allowed to decrease the dose of other urticaria medications unless otherwise instructed by your physician. A decrease in " +
                       "dose may need to be performed gradually under the direct supervision of your physician."),


        //4


        new CSUFAQDRUG("Can I stop other medications for urticaria when I start Xolair?",
                      "No. You are not allowed to either abruptly stop or decrease the dose of other urticaria medications unless otherwise instructed by your physician.")
    };
}
