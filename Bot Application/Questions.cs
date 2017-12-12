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

        public static implicit operator Test(Question v)
        {
            throw new NotImplementedException();
        }
        [Serializable]
        public class Question
        {
            public int item_id;
            public string question;
            public string right_answer;
            public List<string> hints;
            public List<string> multiple_answers;
        }
    }
}