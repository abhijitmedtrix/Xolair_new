
using System.Collections.Generic;
namespace FAQ
{
    public class Faq
    {
        //public FAQ(string question, string answer)
        //{
        //    this.question = question;
        //    this.answer = answer;
        //}
        private string question;
        private string answer;
        public string Question
        {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value;
            }
        }
        public string Answer
        {
            get
            {
                return this.answer;
            }
            set
            {
                this.answer = value;
            }
        }

    }

}

