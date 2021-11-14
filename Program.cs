using System;

namespace HomeTestDataOfAFile
{
    class Program
    {
        static void Main(string[] args)
        {
            File file = new File("C:/Users/1/Desktop/myExample.txt");
            file.ReadFileByLines();
            file.WordsData();
            file.SentenceData();
            file.ColorsInFile();
            file.CloseResultFile();
        }
    }
}
