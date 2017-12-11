namespace AssessmentBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using System.Diagnostics;

    [Serializable]
    public class Test
    {
        public int test_id;
        public List<Question> questions;

        public class Question
        {
            public int item_id;
            public string question;
            public string right_answer;
            public List<string> hints;
            public List<string> multiple_answers;
        }

        public static List<Test> LoadJson()
        {
            using (StreamReader r = new StreamReader("test1.json"))
            {
                string json = r.ReadToEnd();
                List<Test> tests = JsonConvert.DeserializeObject<List<Test>>(json);
                return tests;
            }
        }
    }
}