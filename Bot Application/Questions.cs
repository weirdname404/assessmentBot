namespace AssessmentBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using System.Diagnostics;

    [Serializable]
    public class Questions
    {

        public static List<Test> LoadJson()
        {
            using (StreamReader r = new StreamReader("test1.json"))
            {
                string json = r.ReadToEnd();
                List<Test> tests = JsonConvert.DeserializeObject<List<Test>>(json);
                return tests;
            }
        }

        public class Test
        {
            public int test_id;
            public Dictionary<string, string> questions;
            public Dictionary<string, string> answers;
            public Dictionary<string, Dictionary<string, string>> hints;
        }

        public static Test currentTest = LoadJson()[0];
        public static String current_question = currentTest.questions["1"];

        [Prompt("YO")]
        public string firstQuestion { get; set; }

        [Prompt("Please choose the right answer \n{&}")]
        public string secondQuestion { get; set; }

        [Prompt("Please write the right answer below: \n{&}")]
        public string thirdQuestion { get; set; }
    }
}