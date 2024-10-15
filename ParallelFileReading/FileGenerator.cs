using System.Text;

namespace ParallelFileReader
{
    internal class FileGenerator
    {
        public static readonly string SmallFileName = "Small_500words.txt";
        public static readonly string MediumFileName = "Medium_1000words.txt";
        public static readonly string LargeFileName = "Large_2000words.txt";
        public static readonly int SmallWordCount = 100;
        public static readonly int MediumWordCount = 10000;
        public static readonly int LargeWordCount = 1000000;

        public static void GenerateTextFilesInDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ReplaceTextFile(Path.Combine(path, SmallFileName), SmallWordCount);
            ReplaceTextFile(Path.Combine(path, MediumFileName), MediumWordCount);
            ReplaceTextFile(Path.Combine(path, LargeFileName), LargeWordCount);
        }

        public static void ReplaceTextFile(string filePath, int wordCount)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            StringBuilder contentBuilder = new StringBuilder();

            for (int i = 1; i <= wordCount; i++)
            {
                contentBuilder.Append($"word{i} ");
            }

            File.WriteAllText(filePath, contentBuilder.ToString());
        }

        public static string[] GetAllTxtFilesInDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");
                return txtFiles;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        public static void ClearDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string filePath in Directory.GetFiles(folderPath))
                {
                    File.Delete(filePath);
                }

                foreach (string directory in Directory.GetDirectories(folderPath))
                {
                    Directory.Delete(directory, true);
                }
            }
        }
    }
}
