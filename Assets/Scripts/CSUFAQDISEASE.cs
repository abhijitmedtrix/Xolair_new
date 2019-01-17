using System.Collections.Generic;


public class CSUFAQDISEASE : faQ.FAQ
{
    public CSUFAQDISEASE(string question, string answer)
    {

        this.Question = question;
        this.Answer = answer;
    }


    //ABOUT CSU


    public static List<CSUFAQDISEASE> AboutCsu = new List<CSUFAQDISEASE>{


        //1


        new CSUFAQDISEASE("What is CSU?",
           "CSU stands for chronic spontaneous urticaria. Chronic spontaneous urticaria is a condition which is caused by no apparent trigger."),


        //2


        new CSUFAQDISEASE("Are there other forms of chronic urticaria.",
           "Yes, there are two types of chronic urticaria: chronic spontaneous urticaria and chronic inducible urticaria"),


        //3


        new CSUFAQDISEASE("Is urticaria a common disease?",
           "Urticaria is very common. It can affect about 20 percent of people at some time during their lifetime. About 1 in 5 people will have acute urticaria" +
                          "once in their lifetime. CSU affects up to 1% of the population at any given time, accounting for approximately two-thirds of cases of " +
                          "CU. It has been seen that the female to male ratio of CSU is 2:1 i.e. females suffer twice as often as men do. All age groups can be " +
                          "affected, but the peak incidence is between 20–40 years of age."),


        //4


        new CSUFAQDISEASE("What happens in chronic spontaneous urticaria?",
           "The itching, hives and angioedema related to CSU are caused by raised levels of histamine. Histamine is a chemical that is released by mast " +
                         "cells - a special type of white blood cell that makes up part of your immune system. Histamine is usually released from your mast " +
                          "cells in response to an infection or allergen (such as pollen)."),


        //5


        new CSUFAQDISEASE("What happens during a flare-up?",
           "During a CSU flare-up, histamine is released by your mast cells without the presence of an infection or allergen. The release of histamine " +
                          "relaxes your blood vessels, causing them to become wider and release fluid into your skin. This build-up of fluid presents as " +
                          "angioedema, or hives"),


        //6


        new CSUFAQDISEASE("What happens when histamine is released?",
           "The release of histamine relaxes your blood vessels, causing them to become wider and release fluid into your skin. This build-up of fluid " +
                          "presents as angioedema, or hives."),


        //7


        new CSUFAQDISEASE("How many people have CSU?",
           ""),


        //8


        new CSUFAQDISEASE("",
           "CSU affects 0.5%-1% of the world population."),


        //9


        new CSUFAQDISEASE("Will urticaria affect my daily activities?",
           "Yes, CSU can decrease quality of life by affecting performance at work and school."),


        //10


        new CSUFAQDISEASE("Is CSU recurrent?",
           "Yes. Urticaria is a frequent skin disorder which is characterized by recurrent, transient, pruritic wheal and flare-type skin reactions."),


        //11


        new CSUFAQDISEASE("Who are commonly affected with urticaria?",
           "Women are twice as likely to be affected with rashes than men."),


        //12


        new CSUFAQDISEASE("Does the treatment of urticaria improve one's quality of life?",
           "Yes, chronic spontaneous urticaria is treated for the purpose of providing complete symptom control as safely as possible, thus improving " +
                          "quality of life."),


        //13


        new CSUFAQDISEASE("What happens if CSU remains untreated?",
           "If more patients with CSU remain untreated, then CSU may cause a significant humanistic and economic burden."),


        //14


        new CSUFAQDISEASE("Does CSU affect everyone the same way?",
           "The severity of urticaria varies from one patient to another, depending on the type and severity of the hive."),


        //15


        new CSUFAQDISEASE("What is chronic idiopathic urticaria?",
           "Chronic  idiopathic urticaria is a type of  urticaria wherein the cause or triggering factors responsible for the rashes are not known. These " +
                          "rashes may be widespread and recur frequently. The rashes may be short-lived, occurring for a few hours daily or sometimes may " +
                          "last for more than 6 weeks. About 75% of the patients suffering from chronic urticaria have idiopathic urticaria.  "),


        //16


        new CSUFAQDISEASE("Am I suffering from chronic idiopathic urticaria?",
           "The symptoms of chronic idiopathic urticaria do not differ from other types of urticaria. They are itchy skin rashes that can occur anywhere on " +
                          "the body. The rashes may become red and sometimes will turn white after receiving pressure. These rashes have clear edges and can" +
                          " be moved around. The symptoms can fade and reappear over short periods of time. "),


        //17


        new CSUFAQDISEASE("Which age group is affected by urticaria?",
           "Urticaria can affect people of all age groups, but is more common between 20 to 40 years of age."),


        //18


        new CSUFAQDISEASE("Is urticaria an autoimmune disease?",
           "CSU is a mast cell driven disease.  Autoimmune mechanisms like skin mast cell activation is a frequent underlying cause of CSU."),


        //19


        new CSUFAQDISEASE("Is urticaria related to some other underlying disease?",
           "Urticaria is not generally associated with any underlying disease conditions. Although rare,  long standing (chronic) urticaria were found to be" +
                          " associated with diseases such as thyroid autoimmunity, mastocytosis, systemic lupus erythematosus, vasculitis, hepatitis, and " +
                          "lymphoma."),


        //20


        new CSUFAQDISEASE("Are there any other skin diseases that look like urticaria?",
           "There are many skin conditions that can be confused with urticaria. They include insect bites, atopic dermatitis, contact dermatitis, " +
                          "erythema multiforme, drug allergy, viral exanthem, etc."),


        //21


        new CSUFAQDISEASE("Is chronic urticaria a life long disease?",
           "Urticaria is not always a life long disease. Research suggests that about 35% of people suffering from chronic urticaria will be symptom-free " +
                          "within one year. Another 29% of people may experience a reduction in symptoms within a year. Chronic idiopathic urticaria may resolve " +
                          "in 48% of patients within three years of onset. On the other hand, about 20% of  patients with severe recurrent urticaria may " +
                          "experience symptoms for 10 years or so."),


        //22


        new CSUFAQDISEASE("Will chronic urticaria be damaging to a person's overall health?",
           "Although the symptoms of urticaria will resolve without any complications, recurrent episodes of urticaria can have a negative impact on a " +
                          "patient's life. Severe itching may lead to scratches on the skin that can get infected. In some cases angioedema may cause swelling " +
                          "in the throat and block the airways, which might lead to anaphylaxis. Recurrent urticaria will also impair work and social life on " +
                          "affected days. It can also lead to emotional stress. "),


        //23


        new CSUFAQDISEASE("Is chronic idiopathic urticaria hereditary?",
           "Although chronic idiopathic urticaria (CIU) can affect any individual, research shows that a large number of people suffering from CIU have a " +
                          "positive family history for CIU when compared with the general population."),


        //24


        new CSUFAQDISEASE("Does urticaria affect children?",
           "Yes. Urticaria can affect children. However, chronic (long standing) urticaria is less common in children. Compared with adults, children are " +
                          "prone to acute urticaria which occurs secondary to food allergy or viral infection."),


        //25


        new CSUFAQDISEASE("Can urticaria occur in women?",
           "Yes, urticaria can affect both men and women. Studies show that chronic urticaria is relatively more common among middle-aged women."),


        //26


        new CSUFAQDISEASE("What is idiopathic urticaria?",
           "Idiopathic urticaria is a term used to describe the type of urticaria for which the cause has not yet been identified. "),


        //27


        new CSUFAQDISEASE("Is urticaria common in Arab countries?",
           "There is limited data on the prevalence of urticaria in Arab or countries in the Middle East. A study conducted in a general hospital in Makkah, " +
                          "Saudi Arabia showed that urticaria was prevalent among 0.4% of patients. Chronic idiopathic urticaria (CIU) was seen in 44.3% of " +
                          "these cases which was comparable with another study based in Qatar which had 50.1% of CIU cases. Another study conducted in Kuwait " +
                          "showed the prevalence of chronic urticaria to be 2%. Out of this, 80% of patients had CIU. "),


        //28


        new CSUFAQDISEASE("What is cold urticaria?",
           "In patients with cold urticaria, hives develop after being exposed to cold temperatures, and contact with cold objects. The condition is mostly not " +
                          "serious but severe reactions, or anaphylaxis, do occur – typically when the whole body is impacted by cold, such as when swimming in " +
                          "cold water. This can cause a drop in blood pressure, fainting and even death. Swelling of the tongue or throat can lead to difficulty " +
                          "breathing."),


        //29


        new CSUFAQDISEASE("Can you tell me about dermatographism?",
           "Dermatographism is a type of urticaria in which the skin is hypersensitive to pressure. This is the most frequent cause of physical urticaria. " +
                          "The rashes develop suddenly at a site or part of the body which has been rubbed or scratched and fades away within 60 minutes or so."),


        //30


        new CSUFAQDISEASE("What is chronic inducible urticaria?",
           "Chronic inducible urticaria is that which is caused by a particular trigger.")


    };


    //DIAGONISING CSU


    public static List<CSUFAQDISEASE> DiagoniseCsu = new List<CSUFAQDISEASE>
    { 
    
    
    //1


        new CSUFAQDISEASE("How is urticaria diagnosed?",
                          "First, the doctor will ask the patient about his/her disease history and symptoms. Secondly, the doctor will examine the patient's " +
                          "body, especially the affected parts, to determine the severity of the hive. Thirdly, the doctor will ask the patient to run some " +
                          "blood tests to rule out other causes of hives and/or angioedema." ),


        //2


        new CSUFAQDISEASE("Are there any special tests to confirm chronic idiopathic urticaria?",
                          "There are no specific laboratory investigations to confirm the diagnosis of chronic idiopathic urticaria. Your doctor or allergist " +
                          "will conduct various tests to identify and rule out the cause of urticaria. The usually recommended tests include FBE, ELFT, CRP, " +
                          "Serum EPP, TSH, Hepatitis , HIV, stool OCP, PCR, specific igE test, etc  " ),


        //3


        new CSUFAQDISEASE("Is it necessary to undergo a skin prick test?",
                          "Routine skin prick or allergy testing is not necessary in all patients with urticaria. Your doctor may advise a skin prick test be" +
                          "done in some situations if any allergy or trigger factor is suspected. " ),


        //4


        new CSUFAQDISEASE("What are all of the tests  usually needed to check if it a patient has chronic urticaria?",
                          "After clinical evaluation, your doctor or allergist will decide which tests are required for you to confirm a case of urticaria. " +
                          "There are no specific tests to confirm urticaria as it can occur due to various causes. Some of the commonly recommended  " +
                          "investigations include the skin prick test, complete and differential blood count, erythrocyte sedimentation rate(ESR), " +
                          "c-reactive protein test, thyroid stimulating hormone(TSH) test, liver function test and urine analysis. " )



    };


    //ABOUT CSU SYMPTOMS


    //1

    public static List<CSUFAQDISEASE> CsuSymptoms = new List<CSUFAQDISEASE>
    {

        //1


        new CSUFAQDISEASE("What is a wheal?",
                         " A wheal/hive consists of three typical features:1. Central swelling of variable size, usually surrounded by a reflex erythema" +
                          "2.Associated itching (pruritus), or sometimes a burning sensation" +
                          "3.Usually resolves within a few hours and always by 24 hours"),

        //2


        new CSUFAQDISEASE("What is angioedema?",
                         "A sudden and painful swelling caused by fluid build up in the deeper layers of a person's skin is called angioedema. It is often severe in nature. It is typically characterized by:" +
                          "1. Sudden, pronounced swelling of the lower dermis and subcutis" +
                          "2. Sometimes pain rather than itchin" +
                          "3. Frequent involvement below mucous membranes" +
                          "4. Up to 72 hours for resolution"),

        //3


        new CSUFAQDISEASE("Where can angioedema appear?",
                         "Angioedema can affect any part of the body but mostly occurs in eyelids and lips. It can also affect  genitals, tongue, hands and feet."),

        //4


        new CSUFAQDISEASE("How do I know I'm having hives?",
                         "The skin rash seen in urticaria or hives has certain typical characteristics. The rashes may be red or pink wheals of variable size" +
                          " and shape with surrounding erythema and are itchy in nature."),

        //5


        new CSUFAQDISEASE("What do the hives look like?",
                         "Hives or rashes in urticaria appear as a central swelling of variable size. It is surrounded by an area of redness. It is a" +
                          "ssociated with itching or sometimes a burning sensation." ),

        //6


        new CSUFAQDISEASE("Can my rash spread to other family members?",
                         "No, the rashes are not contagious. In fact, urticaria is an autoimmune disease."),

        //7


        new CSUFAQDISEASE("Why am I getting urticaria rashes frequently?",
                         "In about 40% to 50% of the people suffering from urticaria, the cause cannot be identified and is called chronic idiopathic urticaria. The common " +
                          "causes attributed to recurrent urticaria include the following: (1) autoimmune disorder (2) hypersensitivity to changes in " +
                          "temperature, pressure, sun exposure (3) certain medications such as ACE inhibitors, aspirin (4) infections such as parasites, EBV, " +
                          "hepatitis (5) allergy to latex, animal dander or food . Kindly discuss with your doctor or allergist to learn more about the " +
                          "exact cause or causes of your urticaria."),

        //8


        new CSUFAQDISEASE("Can you tell me more about the rashes of urticaria?",
                         "The rashes of urticaria are slightly raised, red in color and itchy. They can appear anywhere on the body. The rashes can vary in " +
                          "size, ranging from a few millimeters in size to hand-sized. Itching may be mild or severe. Rashes may change shape, coalesce, move " +
                          "around, and appear and fade away within a short stretch of time. The center of the red bump may become white after receiving " +
                          "pressure; this is known as blanching."),

        //9


        new CSUFAQDISEASE("Can urticaria cause swelling on the face?",
                         "Yes, urticaria can sometimes be associated with swelling under the skin known as angioedema. It usually affects the skin around the " +
                          "face (mouth, lips, throat or eyes), but can affect any part of the body. Angioedema can occur either on its own or along with " +
                          "urticaria. "),

        //10


        new CSUFAQDISEASE("How long will it take for my rashes to disappear and my skin to return to it its normal appearance?",
                         "The appearance of the affected area may change within 24 hours, and the rash will usually settle within a few days. Usually, " +
                          "rashes in acute urticaria completely clear up within six weeks. If it doesn't clear up that quickly, you are suffering from " +
                          "chronic urticaria. In chronic urticaria, the rashes may take a few months or even years to subside and can recur frequently. For " +
                          "those cases of the disease, consulting your physician is recommended."),

        //11


        new CSUFAQDISEASE("Where do rashes commonly  occur?",
                         "The rashes may appear on one part of the body or can be spread across large areas. Rashes spread on the trunk, arms, legs and " +
                          "face, and you won't be able to stop scratching your skin, which increases irritation of the hives. "),

        //12


        new CSUFAQDISEASE("What is the normal size of the rashes?",
                         "The rashes seen in urticaria range in size from a few millimeters to the size of a hand."),

        //13


        new CSUFAQDISEASE("How long does the swelling in angioedema last for?",
                         "Angioedema/swelling  resolves slowly and can last for up to 72 hours."),

        //14


        new CSUFAQDISEASE("Can hives in urticaria become life-threatening?",
                         "Mild cases of rashes and itching may subside on their own within a few hours. But if you are suffering from severe itching and " +
                          "rashes, you may need to take medication or an allergy shot. Sometimes, the itching and swelling may affect the throat and cause" +
                          " breathing difficulty. Such situations can be life threatening and will require immediate emergency medical care."),

        //15


        new CSUFAQDISEASE("Why do rashes develop in urticaria patients?",
                         "The causes of urticaria are varied - it can be immunologic, non-immunologic or idiopathic.  One percent of the population worldwide " +
                          "has urticaria, of which around 70% of the cases of this disease are of unknown cause (chronic spontaneous urticaria)"),

        //16


        new CSUFAQDISEASE("Why does skin become red in urticaria?",
                         "The high levels of histamine released from the mast cells cause the blood vessels in the affected area of the skin to open up, " +
                          "which causes redness or pinkness."),

        //17


        new CSUFAQDISEASE("When will the rash (hives) get better?",
                         "Usually, hives last for 30 minutes to 24 hours."),

        //18


        new CSUFAQDISEASE("Can CSU affect sleep?",
                         "Patients with CSU lack quality sleep."),

        //19


        new CSUFAQDISEASE("Can CSU cause depression?",
                         "CSU is often accompanied by psychiatric co-morbidities like anxiety and depression with every second or third person being affected by these " +
                          "other conditions."),

        //20


        new CSUFAQDISEASE("Can CSU cause anxiety?",
                         "Patients with CSU frequently exhibit psychiatric co-morbidities such as anxiety and depression."),

        //21


        new CSUFAQDISEASE("What is vasculitis?",
                         "Vasculitis is an inflammation of the blood vessels. It can also cause hives.  These hives are more painful than itchy, last for " +
                          "more than a day and leave a bruise on the skin."),

        //22


        new CSUFAQDISEASE("When should I seek medical advice for hives?",
                         "If your symptoms do not subside within 48 hours, seek medical advice. You should also contact your doctor, if your symptoms are " +
                          "severe, cause distress, disrupt daily activities and occur alongside other symptoms."),

        //23


        new CSUFAQDISEASE("Can urticaria cause swelling in the feet after standing for a long duration?",
                         "Delayed pressure urticaria can appear as red swelling six to eight hours after pressure (belts or constrictive clothing, for example) " +
                          "has been applied. Symptoms can also occur in parts of the body under constant pressure, such as the soles of the feet."),

        //24


        new CSUFAQDISEASE("What is pruritis?",
                         "Pruritis is a medical term used to describe intense skin itchiness that occurs with some conditions such as urticaria."),

        //25


        new CSUFAQDISEASE("Can urticaria cause fatigue?",
                         "Urticaria can lead to sleep disturbances and emotional stress which can in turn lead to tiredness or fatigue."),

        //26


        new CSUFAQDISEASE("Will the urticaria rashes leave any scars behind?",
                         "The rashes of urticaria or hives will not usually leave behind any marks or scars on the skin. However, it is important to restrict " +
                          "the urge of itching to prevent any skin from bruising. "),

        //27


        new CSUFAQDISEASE("Is the swelling and itchy rash life threatening?",
                         "The rashes and itchiness may usually resolve within 24 hours. The swelling (angioedema) may usually take up to 72 hours to subside." +
                          " If the swelling affects your throat or makes breathing difficult, seek immediate medical care. "),

        //28


        new CSUFAQDISEASE("How long does chronic idiopathic urticaria usually last?",
                         "The symptoms of chronic idiopathic urticaria will persist for 5years on average, but may be longer in more severe cases, particularly " +
                          " in those with simultaneous angioedema or in urticaria due to autoimmune disorder. "),

        //29


        new CSUFAQDISEASE("Why does a patient with urticaria  feel like scratching his or her skin all the time?",
                         "The high levels of histamine released from the mast cells cause the blood vessels in the affected area of skin to open up and become " +
                          "leaky. This extra fluid in the tissues causes swelling and itchiness."),

        //30


        new CSUFAQDISEASE("Is urticaria an allergic disorder?",
                         "No. The symptoms of urticaria may often resemble skin allergy. However, the causes of urticaria can be various and are not always " +
                          "related to allergy. Urticaria is often confused with allergy as exposure to certain factors can often trigger or worsen the symptoms."),

        //31


        new CSUFAQDISEASE("What is urticaria?",
                         "Urticaria, commonly known as hives, are itchy rashes that appear on the skin. They may sometimes turn into red swollen patches. " +
                          "The itching may be mild to severe in nature. They can appear on any part of the body."),

        //32


        new CSUFAQDISEASE("What are the different types of rashes?",
                         "There are two types of urticaria: acute urticaria and chronic urticaria."),

        //33


        new CSUFAQDISEASE("What is acute urticaria?",
                         "Urticaria is considered acute when the patient has had it for less than 6 weeks. This includes occurrence of spontaneous wheals " +
                          "(also known as hives), swelling (angioedema) or both for less than 6 weeks."),

        //34


        new CSUFAQDISEASE("What is chronic urticaria?",
                         "Urticaria is considered chronic when the patient has it for more than 6 weeks. This includes experiencing spontaneous wheals " +
                          "(also known as hives), swelling (angioedema) or both for greater than 6 weeks."),


        //35


        new CSUFAQDISEASE("How do I know whether I am suffering from urticaria?",
                         "The entire body is covered with red, itchy and very painful hives, and the severity increases at night, which may lead to a lack of " +
                          "sleep. A rash spreads on the trunk, arms, legs and face, and the patient can't stop scratching his/her skin, which increases " +
                          "irritation of the hive."),


        //36


        new CSUFAQDISEASE("Can urticaria cause any other symptoms in my body?",
                         "In addition to the itchy rashes, severe episodes of urticaria may cause  headache, dizziness, hoarseness of voice,  lumpy sensation" +
                          " in the throat, shortness of breath or wheezing, nausea, vomiting, abdominal pain, diarrhea and joint pain in some cases.")


    };


    //MORE ABOUT CSU TRIGGERS


    public static List<CSUFAQDISEASE> CsuTrigger = new List<CSUFAQDISEASE> 
    { 
    

        //1


        new CSUFAQDISEASE("What are the usual triggers for hives or urticaria?",
                          "Urticaria or hives can be triggered by many substances or situations. The most common triggers are foods such as peanuts, " +
                          "eggs, nuts, shell fish; certain medications (antibiotics, aspirin, ibuprofen); insect bites; changes in weather or sun exposure; " +
                          "latex; certain viral infections; pet dander; pollen and some plants (poison ivy, poison oak etc.)."),


        //1


        new CSUFAQDISEASE("Which are the common medications that cause urticaria?",
                          "Certain medicines can trigger urticaria. They are generally associated with acute urticaria but may also be responsible for " +
                          "chronic urticaria. These include aspirin, penicillin, narcotics (codeine, morphine), ibuprofen, diclofenac, atracurium, chlorhexidine" +
                          " and oral contraceptives. "),


        //1


        new CSUFAQDISEASE("Can urticaria occur due to worm infections?",
                          "Yes. Urticaria may occur due to parasitic infections in some people."),


        //1


        new CSUFAQDISEASE("Can obesity lead to chronic idiopathic urticaria?",
                          "Yes. Recent research shows an association between obesity (excess body weight) and chronic idiopathic urticaria."),


        //1


        new CSUFAQDISEASE("Can high cholesterol lead to chronic idiopathic urticaria?",
                          "Yes, recent research supports an association between high cholesterol levels (hyperlipidemia) and chronic idiopathic urticaria."),


        //1


        new CSUFAQDISEASE("Can urticaria occur due to warm temperature?",
                          "Cholinergic urticaria is due to an increase in body temperature because of sweating, exercise, hot showers and/or anxiety."),


        //1


        new CSUFAQDISEASE("Can dust or pollen cause urticaria?",
                          "Inhalation or contact with dust or pollen  has been found to trigger urticaria in some cases."),


        //1


        new CSUFAQDISEASE("Will drinking alcohol cause urticaria?",
                          "Consumption of alcohol can trigger or worsen the urticaria in affected people. "),


        //1


        new CSUFAQDISEASE("Will urticaria worsen after exercising?",
                          "Yes, urticaria may worsen or be aggravated by exercise in some people. It may also appear while working in hot and sweaty conditions."),


        //1


        new CSUFAQDISEASE("Can stress cause CSU?",
                          "There is no substantial evidence about the impact of stress on chronic spontaneous urticaria. However, studies have demonstrated" +
                          " that stress can act as an aggravation factor in some patients suffering from chronic spontaneous urticaria. "),


        //1


        new CSUFAQDISEASE("Can some food items cause hives?",
                          "Although certain food items may trigger urticaria, food allergy is not commonly associated with urticaria . The common food items " +
                          "include peanuts, eggs, nuts , shell fish, strawberries and tomatoes. Certain additives or chemical preservatives present in food " +
                          "may also trigger urticaria. However, it is rare for any food to cause urticaria.")
    };
}
