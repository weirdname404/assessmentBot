namespace AssessmentBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;
    using System.Collections.Generic;

    [Serializable]
    public class Tests
    {

        public static string firstQuestion = "The binary system uses powers of\\n" + System.Environment.NewLine +
                "A. 2\\n" + System.Environment.NewLine +
                "B. 10\\n" + System.Environment.NewLine +
                "C. 8\\n" + System.Environment.NewLine +
                "D. 16\\n" + System.Environment.NewLine +
                "E. None of the above";


        public static Dictionary<string, string> easyTest = new Dictionary<string, string>
        {
            {"The binary system uses powers of\n\n" +
                "A. 2\n" +
                "B. 10\n" +
                "C. 8\n" +
                "D. 16\n" +
                "E. None of the above",
                "A" },

            {"A computer program that converts assembly language to machine language is. \n" +
                "A. Compiler\n" +
                "B.	Interpreter\n" +
                "C.	Assembler\n" +
                "D.	Comparator\n" +
                "E.	None of the above",
                "C" },

            {"2+2?", "4" }

        };
    }
}