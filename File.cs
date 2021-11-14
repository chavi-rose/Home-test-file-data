using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace HomeTestDataOfAFile
{
    class File
    {
        string Path;
        Dictionary<string, int> allFileWords = new Dictionary<string, int>();
        StreamWriter resultsFile = new StreamWriter("C:/Users/1/Desktop/results.txt");
        public File(string Path)
        {
            this.Path = Path;
        }
        public void CloseResultFile()
        {
            resultsFile.Close();
        }
        //the function read the file and fill a dictionary with all the words and the amount of each one.
        public void ReadFileByLines()
        {
            String line;
            int LineNum = 0;
            char g = '"';
            try
            {
                StreamReader sr = new StreamReader(Path);
                line = sr.ReadLine();
                while (line != null)
                {
                    LineNum++;
                    string[] words = line.Trim().Split(new[] { " ", ",", "?", "!", ".", ":", ";", "'", g.ToString() }, StringSplitOptions.RemoveEmptyEntries);
                    line = sr.ReadLine();
                    foreach (var item in words)
                    {
                        if (allFileWords.ContainsKey(item))
                        {
                            allFileWords[item]++;
                        }
                        else
                        {
                            allFileWords.Add(item, 1);
                        }
                    }
                }
                sr.Close();
                resultsFile.WriteLine($"question number 1. The number of lines in the file is: {LineNum}.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        //the function iterate the dictionary and find the sum of words and the popular and unique words.
        public void WordsData()
        {
            int cuontWords = 0, uniqueWords = 0, popularWord = 0, i;
            string thePopularWord = null;
            string[] grammerWords = { "the", "that", "had", "by", "for", "not", "don't", "as", "am", "is", "are", "him", "and", "of", "to", "a", "in", "I", "his", "was", "he", "it", "with", "at", "on", "her", "be", "were", "them" };

            foreach (var item in allFileWords)
            {
                cuontWords += item.Value;
                if (item.Value == 1)
                {
                    uniqueWords++;
                }
                if (item.Value > popularWord)
                {
                    for (i = 0; i < grammerWords.Length; i++)
                    {
                        if (grammerWords[i] == item.Key)
                        {
                            break;
                        }
                    }
                    if (i == grammerWords.Length)
                    {
                        popularWord = item.Value;
                        thePopularWord = item.Key;
                    }
                }
            }
            resultsFile.WriteLine($"question number 2. The number of words is: {cuontWords} words.");
            resultsFile.WriteLine($"question number 3. The number of unique words is:{uniqueWords} words.");
            resultsFile.WriteLine($"question number 5.The popular word: {thePopularWord}");
        }

        //the function go over the file and count words in each sentence.
        public void SentenceData()
        {
            int sumOfallwords = 0, maxLengthOfSentence = 0;
            try
            {
                StreamReader sr = new StreamReader(Path);
                string allText = sr.ReadToEnd();
                WordsWithoutK(allText);
                string[] sentences = allText.Trim().Split(".");
                foreach (var item in sentences)
                {
                    string[] wordsInSentence = item.Split(" ");
                    sumOfallwords += wordsInSentence.Length;
                    if (maxLengthOfSentence < wordsInSentence.Length)
                    {
                        maxLengthOfSentence = wordsInSentence.Length;
                    }
                }
                sr.Close();
                resultsFile.WriteLine($"question number 4.1. The length of the avarage sentence is: {sumOfallwords / sentences.Length} words.");
                resultsFile.WriteLine($"question number 4.2. The max length of sentence is: {maxLengthOfSentence} words.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

        public void ColorsInFile()
        {
            Colors colors = new Colors();
            List<Color> allColors = colors.GetAllColors();
            int[] numOfColorAppearInText = new int[allColors.Count];
            foreach (var item in allFileWords)
            {
                for (int i = 0; i < allColors.Count; i++)
                {
                    //check the color  ignore case.
                    bool result = item.Key.Contains(allColors[i].Name, StringComparison.InvariantCultureIgnoreCase);
                    if (result)
                    {
                        numOfColorAppearInText[i]++;
                    }
                }

            }
            for (int j = 0; j < numOfColorAppearInText.Length; j++)
            {
                if (numOfColorAppearInText[j] > 0)
                {
                    resultsFile.WriteLine($"question number 8. the color: {allColors[j].Name} appears {numOfColorAppearInText[j]}times ");
                }
            }
        }
        public void WordsWithoutK(string file)
        {
            int maxLength = 0, first = 0, last = 0, temporaryfirst = 0, temporaryLast = 0;
            string theText = null;
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] != 'k')
                {
                    temporaryLast = i;
                }
                else
                {
                    if (maxLength < temporaryLast - temporaryfirst)
                    {
                        first = temporaryfirst;
                        last = temporaryLast;
                        maxLength = temporaryLast - temporaryfirst;
                    }
                    temporaryfirst = temporaryLast + 2;
                }
            }
            for (int i = first; i <= last; i++)
            {
                theText += file[i];
            }
            resultsFile.WriteLine($"question number 6. The longest word sequence that does not contain the letter k is: {theText}");
        }

    }

}

