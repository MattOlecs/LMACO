﻿using System.Diagnostics;
using COLLM.Interfaces.Services;
using Python.Runtime;

namespace COLLM.Services;

internal class PythonScriptExecutor : IPythonScriptExecutor
{
    public PythonScriptExecutor()
    {
        if (PythonEngine.IsInitialized)
        {
            return;
        }

        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine("Start python initialization");
        PythonEngine.Initialize();
        PythonEngine.BeginAllowThreads();
        timer.Stop();
        Console.WriteLine($"Finised python initialization [{timer.Elapsed.Seconds} sec]");
    }
    
    public double GetSentencesSimilarityUsingSpacy(string firstSentence, string secondSentence)
    {
        string result;
        var timer = new Stopwatch();
        using (Py.GIL())
        {
            Console.WriteLine($"Using gil took: {timer.Elapsed.Seconds} sec");
            timer.Restart();
            //python -m spacy download en_core_web_lg
            dynamic spacy = Py.Import("spacy");
            dynamic nlp = spacy.load("en_core_web_lg");

            dynamic doc1 = nlp(firstSentence);
            dynamic doc2 = nlp(secondSentence);

            dynamic similarity = doc1.similarity(doc2);
            result = similarity.ToString();
            Console.WriteLine($"Calc took {timer.Elapsed.Seconds} sec");
        }

        return Convert.ToDouble(result);
    }
}