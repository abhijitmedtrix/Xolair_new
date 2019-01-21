
using System.Collections.Generic;


public class FAQDiseaseSAA : FAQ.Faq
{
    public FAQDiseaseSAA(string question, string answer)
    {

        this.Question = question;
        this.Answer = answer;
    }


    //About asthma


    public static List<FAQDiseaseSAA> aboutAsthma = new List<FAQDiseaseSAA>
    {


        //1


        new FAQDiseaseSAA("What does asthma mean?",
                          "Asthma is a condition in which your airways get chronically inflamed. This leads to a number of respiratory symptoms such as wheezing, " +
                          "shortness of breath, chest tightness and cough that vary over time and in intensity."),


        //2


        new FAQDiseaseSAA("Can asthma get serious?",
                          "Signs and symptoms of asthma may resolve spontaneously or in response to medications.  At times, patients can experience" +
                          " episodic flare-ups of asthma that may be life-threatening and carry a significant burden to patients."),


        //3


        new FAQDiseaseSAA("How is my lung affected in asthma?",
                          "In asthma, lung function may vary from being completely normal to severely obstructed. The characteristic feature of the lung " +
                          "dysfunction is a variable expiratory airflow i.e. the amount of expiratory airflow varies in time and magnitude compared with " +
                          "healthy individuals."),


        //4


        new FAQDiseaseSAA("What are the different types of asthma?",
                          "There are 2 types of asthma: Allergic and Non-allergic asthma."),


        //5


        new FAQDiseaseSAA("How is allergic asthma different from non-allergic asthma?",
                          "The underlying mechanisms differ between these two types of asthma.Allergic asthma usually affects younger patients and is " +
                          "IgE-mediated, dependent on T-helper cells, and involves mast cells, basophils and eosinophils. In contrast, non-allergic asthma " +
                          "often has an adult onset, and is associated with non-allergic co-morbidities such as rhinosinusitis and gastroesophageal reflux." +
                          "Allergic asthma tends to be more responsive to inhaled corticosteroids, while non-allergic asthma is less so."),


        //6


        new FAQDiseaseSAA("What causes allergic asthma?",
                          "Allergic asthma results from an allergic reaction to foreign substances such as inhaled  aeroallergens, pollen, dust mites, mold, " +
                          "animal dander, or ingested foods, beverages, or drugs. Patients are also more likely to have other allergic conditions, such as " +
                          "eczema, hay-fever or food allergy."),


        //7


        new FAQDiseaseSAA("When does allergic asthma occur?",
                          "Allergic asthma tends to develop at a younger age with a clear allergic trigger and a seasonal symptom pattern that reflects " +
                          "the environmental occurrence of the trigger."),


        //8


        new FAQDiseaseSAA("What is non-allergic asthma?",
                          "Non-allergic asthma is caused due to non-allergic co-morbidities, such as rhinosinusitis and gastroesophageal reflux. Patients " +
                          "with non-allergic asthma are more likely to be female, be older at asthma onset and to have nasal polyps or FEV1 <80% predicted. " +
                          "Smoking is more common in patients with Non-allergic than allergic asthma."),


        //9


        new FAQDiseaseSAA("When does non-allergic asthma occur?",
                          "Patients with non-allergic asthma are more likely to be older at asthma onset."),


        //10


        new FAQDiseaseSAA("How do I know whether I am suffering from allergic asthma?",
                          "Allergic asthma is the most easily recognized asthma phenotype, which often commences in childhood. It is often associated with " +
                          "a past and/or family history of allergic disease such as eczema, allergic rhinitis or food or drug allergy."),


        //11


        new FAQDiseaseSAA("How do I know whether I am suffering from non-allergic asthma?",
                          "Non-allergic asthma is caused due to non-allergic co-morbidities, such as rhinosinusitis and gastroesophageal reflux." +
                          "Patients with Non-allergic asthma are more likely to be male, be older at asthma onset and to have nasal polyps or " +
                          "FEV1 <80% predicted. Smoking is more common in patients with Non-allergic than allergic asthma. Examination of the sputum in " +
                          "non-allergic asthma often reveals the cellular profile to be neutrophilic or eosinophilic containing a few inflammatory cells. "),


        //12


        new FAQDiseaseSAA("How severe is my asthma?",
                          "Patients may perceive their asthma as severe if they have intense or frequent symptoms, but this  does not necessarily indicate " +
                          "underlying severe disease, as symptoms may rapidly become well controlled with inhaled corticosteroids. It is important that health" +
                          " professionals communicate clearly to patients what they mean by the word severe. Firstly, asthma severity involves both the severity" +
                          " of the underlying disease and its responsiveness to treatment. Asthma severity is also not a static feature of asthma but " +
                          "may change over months or years. In addition, the asthma severity is a poor predictor of which treatments a patient should " +
                          "receive or what their response to treatment may be."),


        //13


        new FAQDiseaseSAA("When is my asthma considered uncontrolled?",
                          "Based on the clinical guidelines of the National Heart, Lung, and Blood Institute (NHLBI), your asthma may be considered " +
                          "uncontrolled if you: • Have 2 or more attacks a year that require oral steroids• Wake up at night 1-3 times per week due to asthma " +
                          "symptoms • Have wheezing, chest tightness, a cough, or feel out of breath 2 or more times a week"),


        //14


        new FAQDiseaseSAA("What is an allergen?",
                          "An allergen is a substance that can cause an allergic reaction. Environmental factors that may provoke asthma attacks " +
                          "include inhaled allergens (commonly dust mites and animal fur; less commonly pollens, moulds, and allergens encountered in " +
                          "the workplace); and inhaled irritants (cigarette smoke, fumes from cooking, heating or vehicle exhausts, cosmetics," +
                          " and aerosol sprays), and medicines (including aspirin)."),


        //15


        new FAQDiseaseSAA("What is IgE?",
                          "IgE is an antibody that is produced by the body's immune system in response to a perceived threat. Immunoglobulin E is " +
                          "associated with allergic responses, including asthma and to a lesser degree with immunitty to parasites."),


        //16


        new FAQDiseaseSAA("What happens in an asthmatic attack?",
                          "When a person with allergic asthma breathes in an allergen like pet dander, dust mites etc, the body responds by producing " +
                          "a substance called IgE. When the allergen is breathed in again, IgE attaches to the allergen and to the surface of inflammatory " +
                          "cells. This releases a substance that causes inflammation- swelling and narrowing in the airways. This inflammation can cause " +
                          "asthma symptoms that can lead to an attack."),


        //17


        new FAQDiseaseSAA("Am I the only one suffering from asthma?",
                          "As per latest statistics, around 339 million people worldwide are affected with asthma. "),


        //18


        new FAQDiseaseSAA("Can you tell me about allergic asthma?",
                          "Allergic asthma is the most common type of asthma. It is a disease condition wherein the presence of certain allergens " +
                          "such as dust, mold or pollen might lead to an asthmatic episode resulting in a cough, wheezing and breathing difficulty in some " +
                          "individuals. "),


        //19


        new FAQDiseaseSAA("Can I have both asthma and an allergy?",
                          "Allergy and asthma are two different from each other. But it is very common for a person with an allergy to develop asthma. " +
                          "An allergy is the hypersensitive reaction of your body to certain irritants, which may cause sneezing, watery eyes, runny nose " +
                          "and skin rashes or hives. On the other hand, asthma is a condition in which the airways of lungs become swollen and inflamed. " +
                          "Asthma may occur due to allergy or non-allergic causes. Asthma that occurs when a person comes in contact with or inhales" +
                          " allergens is known as allergy induced asthma. Irritants that are responsible for allergy can also trigger an asthma episode. " +
                          "The majority of patients with an allergy are prone to suffer from asthma later in life."),


        //20


        new FAQDiseaseSAA("Is my allergic asthma completely curable?",
                          "Unfortunately, there is no complete cure for asthma. However, you can take precautionary measures to avoid exposure to the " +
                          "triggering factors. There are also medications that will ease your symptoms and help you to lead a normal life. Consult your" +
                          " doctor regarding the best treatment option for your allergic asthma."),



        //21


        new FAQDiseaseSAA("Can you tell me about the IgE test?",
                          "Your doctor may advise you to undergo an Immunoglobulin E or IgE test to check if you have an allergy. Immunoglobulins are " +
                          "antibodies produced in the body to protect itself from invading microorganisms or allergens. IgE is a type of immunoglobulin " +
                          "that is produced in high levels when an allergen enters the body. Hence, a person with an allergy will have high levels of IgE " +
                          "in their blood. So the IgE test is used as a screening test for patients suspected of having allergies."),


        //22


        new FAQDiseaseSAA("How long will an asthma attack last?",
                          "The duration of an asthma attack can vary depending on what caused the asthma attack and the extent of your respiratory airway " +
                          "inflammation. Mild attacks may last for a few minutes; while a moderate-to-severe asthma attack may last for several hours or " +
                          "days. It is important to follow your doctor's advice during an asthma attack and take the medications before the symptoms worsen. "),


        //23


        new FAQDiseaseSAA("What are dust mites?",
                          "Dust mites are tiny microscopic insects found inside homes, especially in ones that are dusty and humid. These insects usually " +
                          "settle on carpets, curtains, drapes and bed linens. They feed on skin flakes shed by humans and animals and are a common cause " +
                          "of asthma in children. "),


        //24


        new FAQDiseaseSAA("Is my asthma under control?",
                          "Generally, your asthma can be considered to be under control if:1. you do not have day-time symptoms more than 2 days per week " +
                          "2. you are able to carry out day-to-day activities at ease " +
                          "3. night-time symptoms occur no more than once a month in children and no more than twice a month in adults and children " +
                          "above 12 years 4. you need rescue medications less than 2 days per week 5. the peak flow meter reading is above 80% of personal best"),


        //25


        new FAQDiseaseSAA("What is a peak flow meter?",
                          "Your doctor may advise you to use a peak flow meter at home. It is a simple device that you can use at home to check how well your" +
                          " airway passages are functioning and the progress of your asthma treatment. It will also help you to understand if you are recovering " +
                          "from an asthma episode. To use a peak flow meter, take a deep breath and blow air into the device as hard and fast as you can. " +
                          "The marker in the device will then move up the scale and show your score. You can record and compare the scores on the peak " +
                          "flow meter daily as your treatment progresses. "),


        //26


        new FAQDiseaseSAA("How many people suffer from asthma in the world?",
                          "It is estimated that about 339 million people in the world suffer from asthma. Although asthma affects people all over the world, " +
                          "it is relatively more common in Australia and New Zealand, some countries in Africa, the Middle East, South America, and " +
                          "North-Western Europe. "),


        //27


        new FAQDiseaseSAA("Is asthma life threatening?",
                          "It is estimated that 1 in 10,000 persons with asthma die due to a fatal asthma attack. The risk is a lot higher in patients" +
                          " suffering from severe persistent (uncontrolled) asthma. Experts say that deaths from asthma attacks can be prevented if " +
                          "appropriate treatment is taken. Death from asthma is bound to happen only if the patient fails to recognize the severity of " +
                          "the disease, ignores or skips the prescribed  medications and/or delays seeking medical attention during an attack. "),

    };


    //TOOLS MEASURING ASTHMA SYMPTOMS


    public static List<FAQDiseaseSAA> measureAsthma = new List<FAQDiseaseSAA>
    {


        //1


        new FAQDiseaseSAA("Is there any way to check my asthma symptoms?",
                         "The Asthma Control Test (ACT) is a tool with a short quiz that provides a numerical score to help you and your asthma specialist " +
                          "determine if your asthma symptoms are well controlled. It is a patient self-administered tool for identifying those with poorly" +
                          " controlled asthma.The Asthma Control Questionnaire (ACQ) is a simple questionnaire to measure the adequacy of asthma control and " +
                          "change in asthma control which occurs either spontaneously or as a result of treatment."),


        //2


        new FAQDiseaseSAA("How do I use the Asthma Control Test (ACT)?",
                         "The Asthma Control Test (ACT) is a tool with a short quiz that provides a numerical score to help you and your asthma specialist " +
                          "determine if your asthma symptoms are well controlled. It is a patient self-administered tool for identifying those with poorly " +
                          "controlled asthma. The scores range from poor control (5) to total asthma control (25). ‘At least well-controlled’ asthma is defined " +
                          "as an ACT score of ≥20 and ‘not well-controlled’ asthma is defined as an ACT score of ≤19. No matter what kind of asthma you have, " +
                          "it’s important to know if it’s uncontrolled or not."),


        //3


        new FAQDiseaseSAA("How does the asthma control test help me?",
                         "The Asthma Control Test helps to assess the frequency of symptoms like shortness of breath and general asthma symptoms, use of" +
                          " rescue medications, the effect of  asthma on daily activities and overall self-assessment of asthma control."),


        //4


        new FAQDiseaseSAA("I do not understand how to interpret ACT scores?",
                         "In the Asthma Control Test, scores range from 5-25. If you get a total score of 5 it means that your asthma is poorly controlled" +
                          " and if it is 25 it means that your asthma is completely controlled. A score > 19 indicates well-controlled asthma. "),


        //5


        new FAQDiseaseSAA("What is the Asthma control questionnaire (ACQ)?",
                         "The Asthma Control Questionnaire (ACQ) is a simple questionnaire that measures how effectively your asthma is controlled and " +
                          "if the change in asthma control has occurred spontaneously or as a response to treatment."),


        //6


        new FAQDiseaseSAA("What can the ACQ tell me about my asthma?",
                         "The Asthma Control Questionnaire is a seven-item questionnaire that can help record your asthma symptoms and the frequency" +
                          " of use of your rescue inhaler for up to 1 week."),


        //7


        new FAQDiseaseSAA("How can I interpret the ACQ scores?",
                         "The Asthma Control Questionnaire has 7 items and a patient's performance on each item is assigned a rating of 0 to 7. It " +
                          "has a multidimensional construct assessing symptoms (5 items--self-administered) and rescue in bronchodilator" +
                          " use (1 item-self-administered), and FEV1% (1 item) completed by clinic staff. In the scale, '0' stands for no impairment " +
                          "and '6' stands for maximum impairment of symptoms and rescue use. In the Asthma Control Questionnaire, scores range from '0' " +
                          "which stands for 'totally controlled' and '6' which stands for 'severely uncontrolled'.")


    };


    //DiagonisingAsthma


    public static List<FAQDiseaseSAA> diagoniseAsthma = new List<FAQDiseaseSAA>
    {


        //1


        new  FAQDiseaseSAA("Are there any lab tests that confirm my asthma?",
                           "Asthma can be diagnosed by specific lung function tests such as Forced Expiratory Volume in 1 sec (FEV1) and  Peak Flow " +
                           "Expiratory (PEF). These tests should be carried out by well-trained operators with well maintained and regularly calibrated" +
                           " equipment (spirometer)."),



        //2


        new  FAQDiseaseSAA("What is FEV1?",
                           "Forced Expiratory Volume in 1 sec is the maximal volume of air that can be exhaled in a specific time interval following a " +
                           "maximal (deep) inhalation."),


        //3


        new  FAQDiseaseSAA("Is FEV1 a specific test for asthma?",
                           "No, FEV1 is indicated for other lung diseases as well. However a reduced ratio of FEV1/FVC  indicates airflow limitation."),


        //4


        new  FAQDiseaseSAA("How can I know if I have allergic asthma?",
                           "Allergy testing is done using skin prick testing or by measuring the level of specific immunoglobulin E(sIgE) in the serum."),

    };


    //ABOUT ASTHMA SYMPTOMS


    public static List<FAQDiseaseSAA> asthmSymptoms = new List<FAQDiseaseSAA>
    {
    
    
    //1


        new FAQDiseaseSAA("What are the signs and symptoms of asthma?",
                          "You are suffering from asthma when you experience or have had a history of respiratory symptoms such as wheezing, " +
                          "shortness of breath, chest tightness and cough that vary in duration and in intensity."),
         
    
    //2


        new FAQDiseaseSAA("What are the symptoms of an asthma attack?",
                          "Symptoms of allergic asthma or another type of asthma include: ■ wheezing ■ shortness of breath ■ chest tightness ■ coughing"),
         
    
    //3


        new FAQDiseaseSAA("What are the triggering factors of asthma?",
                          "Symptoms of asthma are triggered by various factors. These include foreign substances such as inhaled  aeroallergens, pollen, dust" +
                          " mites, mold, animal dander, or ingested foods, beverages, or drugs. Other triggering factors include viral infections (colds), " +
                          "exercise, changes in weather, laughter, or irritants such as car exhaust fumes, smoke or strong smells."),
         
    
    //4


        new FAQDiseaseSAA("What are the respiratory symptoms which are characteristically seen in Asthma?",
                          "The following features are typically seen in Asthma: More than 1 symptom (wheeze, shortness of breath, chest tightness, cough), " +
                          "especially in adults. Symptoms worsening at night or in the early morning. Symptoms vary over time and in intensity. Symptoms being " +
                          "triggered by viral infections, exercise, allergen exposure, changes in weather, laughter or irritants such as car exhaust fumes, " +
                          "smoke or strong smells."),
         
    
    //5


        new FAQDiseaseSAA("Why do I get asthma episodes more often at night?",
                          "You may be getting asthma episodes more often at night if there are dust mites, mold, pet dander or other allergens in your bedroom. " +
                          "Asthma symptoms may also flare up if the bedroom is too cold, too warm or too humid. Lying down on the bed may also lead to drainage " +
                          "of mucus from the nose, sinus and throat, which will accumulate in the airway and cause asthma symptoms. Additionally, your body's " +
                          "immune system also goes into a resting phase during sleep which may increase the chances of asthma episodes at night. "),
         
    
    //6


        new FAQDiseaseSAA("What do you mean by wheezing?",
                          "Wheezing is a term used to describe a high pitched whistle-like sound that is produced during breathing. It occurs when the air " +
                          "passes through narrow airways in the lungs. It is commonly seen in asthmatic patients. It can also occur in various other disease " +
                          "conditions. "),
         
    
    //7


        new FAQDiseaseSAA("Can asthma cause tiredness?",
                          "If your asthma is not under control, you may feel tired and weak. It could be due to frequent bouts of asthma episodes at night which" +
                          " disturb your sleep. Lack of proper sleep can make you feel drained and exhausted in the morning. Additionally, asthma can cause " +
                          "narrowing of the airways in the lungs, which may in turn reduce the oxygen supply to the body. Lacking oxygen in the body tissues" +
                          " can make you feel tired."),
    };


    //MOREABOUTASTHMATRIGERS



    public static List<FAQDiseaseSAA> asthmaTrigger = new List<FAQDiseaseSAA>
    {


        //1

    
        new FAQDiseaseSAA("Will using air conditioners in rooms worsen asthma?",
                         "Air conditioner use does not directly cause allergic asthma. However, the quality of indoor air can have a direct impact on triggering " +
                          "an asthmatic episode. A well ventilated, dry and clean (dust-free) home will reduce the chances of allergy and  asthma. Room " +
                          "temperature should be steady because drastic changes in room temperature may trigger asthma. Humidity inside a home should be " +
                          "ideally kept between 30 to 50 percent. Increased air humidity will lead to mold growth and trigger asthma. If weather permits, " +
                          "allowing clean outdoor air to enter the home will reduce indoor air allergens. During high pollen season, air conditioners will " +
                          "help to prevent outdoor air from entering your home. It is important to replace air filters at regular intervals."),


        //2

    
        new FAQDiseaseSAA("Can cockroaches cause asthma?",
                         "Yes. Cockroaches can cause asthma. The saliva, feces and shed body parts of cockroaches have been found to cause asthma and " +
                          "allergic reactions in many patients."),


        //3

    
        new FAQDiseaseSAA("Will swimming worsen my asthma?",
                         "Swimming is a safe and healthy exercise for people suffering from asthma. It helps to improve lung function. However, swimming in " +
                          "chlorinated water may worsen asthma symptoms. You should avoid swimming in poorly maintained and chlorinated pools. Doctors often " +
                          "recommend swimming as a good exercise for asthma patients provided the pool is chlorine-free. "),


        //4

    
        new FAQDiseaseSAA("Can having milk or milk products worsen my asthma?",
                         "No. It is a only a myth that drinking milk or eating milk products can increase mucous production, thereby triggering asthma. There " +
                          "is no scientific evidence of milk or a milk product triggering asthma attacks. In fact, it is important to consume adequate amounts" +
                          " of calcium and other nutrients in your diet to stay healthy. There is no need to restrict intake of milk and milk products unless " +
                          "you have been diagnosed with a specific allergy to milk products by your doctor. "),


        //5

    
        new FAQDiseaseSAA("Can obesity trigger asthma attacks?",
                         "Although obesity may not directly cause asthma, recent evidence show that being overweight or obese may increase the " +
                          "chances of developing asthma in some people. It can also worsen the asthma symptoms and affect the response to asthma treatment. "),


        //6

    
        new FAQDiseaseSAA("Am I coughing and having difficulty with breathing due to allergy?",
                         "Allergic asthma is the most common form of asthma. If you notice that you suffer from frequent cough, wheezing and shortness " +
                          "of breath, especially after being exposed to dust mites, pollen or pet dander; then you may have allergy-induced asthma. Some" +
                          " people with asthma do not have allergies. They may develop asthma after exercising, being exposed to cold air, or having a viral" +
                          " or respiratory infection. Consult a doctor to find out the exact cause of your symptoms. It is important to understand whether " +
                          "your asthma is due to allergy or non-allergic causes in order to initiate appropriate treatment. "),


        //7

    
        new FAQDiseaseSAA("Can cold or flu trigger asthma attacks?",
                         "Yes, if you are an asthmatic, even a mild cold or flu may trigger an asthma episode, especially in children. Also, asthma symptoms " +
                          "that start during a cold or flu can last for more days than usual. Hence, it is advisable to limit contact with anyone who's sick " +
                          "and to wash your hands if you come in contact with flu-infected people. "),


        //8

    
        new FAQDiseaseSAA("Is it true that some perfumes can cause allergic asthma?",
                         "Yes. Perfumes and fragrances present in various household items such as shampoo, soap, washing powder, cosmetics and hair dyes" +
                          " can trigger an allergic episode or an asthma attack in people suffering from allergic asthma."),


        //9

    
        new FAQDiseaseSAA("What do you mean by hypoallergenic pets? Does playing with them trigger an asthma attack?",
                         "People often assume that cats or dogs with less fur do not cause any allergy or asthma. Animals with less fur are often marketed " +
                          "as hypoallergenic pets. Although such pets shed less fur which may cause allergy or trigger asthma, it is better to remember that " +
                          "it is not just the fur that triggers allergy or asthma. A particular protein present in the saliva and urine of pets may also " +
                          "trigger allergy and asthma episodes. "),


        //10

    
        new FAQDiseaseSAA("Will asthma worsen while traveling? ",
                         "It is true that being exposed to a sudden change in weather while traveling can worsen your asthma. You may also be at risk " +
                          "of coming in contact with pollen or other allergens that you are not used to while traveling. However, your asthma should not " +
                          "stop you from taking vacations. Discuss your travel plans with your doctor and do not forget to carry all your medications during " +
                          "trips away from home. Also, be sure to keep your rescue medication within reach at all times. "),


        //11

    
        new FAQDiseaseSAA("How can I reduce the chances of night-time asthma episodes?",
                         "Keep a steady temperature inside your bedroom. The room should not be too cold, too warm or too humid. Wash and change the " +
                          "bedlinens and curtains regularly. Remove carpets if necessary. Do not let pets inside your bedroom. It is important to take the " +
                          "medications prescribed by your doctor regularly. Discuss with your doctor if the night-time asthma episodes are getting more frequent."),


        //12

    
        new FAQDiseaseSAA("Can my kid with asthma play sports?",
                         "Of course, your kid can participate in sports. Kids and teenagers with asthma can and should take part in sports and other " +
                          "physical activities as long as their asthma is under control and they are taking medications as prescribed by their doctor. In fact, " +
                          "being physically active and playing sports will help kids with asthma stay healthy and fit. It can also improve lung function and " +
                          "breathing capacity. It is good idea to inform your child's coaches and school teachers about your child's asthma and to ask them to " +
                          "watch for any signs of asthma flare-ups. Your child should also carry rescue medication all the time and take it if symptoms worsen " +
                          "while at school or playing sports. "),

    };


    //LIFESTYLE MODIFICATION FOR ASTHMA CONTROL


    public static List<FAQDiseaseSAA> modificationAsthma = new List<FAQDiseaseSAA>
    {


        //1


        new FAQDiseaseSAA("Are there any sports that are recommended for asthmatic children?",
        "As long as asthma is well-controlled and the child is taking the prescribed medications regularly, he or she can enjoy any sports or physical " +
                          "activity. Sports like gentle cycling, golf, baseball, football, gymnastics and shorter track and field events can be good choices. " +
                          "It is true that high intensity sports activity or rigorous exercises may trigger an asthma episode and can be challenging for " +
                          "children with severe asthma. This does not mean that asthmatic kids cannot take part in such activities. Tell your child's coach " +
                          "about your child's asthma action plan and the necessity of your child taking breaks to manage flare-ups."),


        //2


        new FAQDiseaseSAA("Should people with allergic asthma refrain from gardening?",
        "There is always a risk of asthma while gardening. If your asthma is under control and you are taking your medications as prescribed by your " +
                          "physician or asthma nurse, you may continue to enjoy gardening. Some of the preventive measures that may help you to avoid " +
                          "triggers of asthma while gardening include:1. Always wear a mask to avoid inhaling pollen or other allergens2. Wear a hat and coat " +
                          "to prevent allergens from settling on your skin and clothes3. Remember to leave the gardening coat and hat outside to prevent" +
                          " allergens from entering your home4. Take a shower or clean your hands and legs as soon as you finish gardening5. Avoid gardening " +
                          "during seasons with high pollen count or on windy and rainy days6. Limit exposure to pesticides or other chemicals that can trigger" +
                          " asthma7. Do not leave grass cuttings on your lawn after mowing8. Always keep your rescue medication handy and take it if needed"),


        //3


        new FAQDiseaseSAA("Can eating healthy food improve my asthma?",
        "Although there are no strict dietary instructions to be followed, it has been found that eating a healthy and well-balanced diet can improve your" +
                          " airways. Make sure to include de a good amount of vegetable and fruits in your diet. Eat foods made from whole grains instead of " +
                          "foods containing white refined grains, such as noodles, pasta, rice or white breads. Consume adequate amounts of lean meats, fïsh, " +
                          "eggs, seeds and legumes. Limit eating junk foods like chips, fried potatoes and soft drinks. If your doctor has determined that you" +
                          " have an allergy to a particular food, avoid eating that item."),


        //4


        new FAQDiseaseSAA("Will losing weight improve my asthma?",
        "If you are obese or overweight, losing weight will help in improving asthma symptoms and possibly reduce the frequency of asthma attacks. " +
                          "Losing excess body fat has also been shown to improve lung function and reduce the need of asthma medications. It has also " +
                          "been shown to cut hospitalizations. "),



    };
}