using OtusHomeworkTools;
using System.Diagnostics;

namespace ParallelFileReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseConsoleActions.PrepareConsole("Параллельное считывание файлов");

            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            ConsoleMenu menu = new ConsoleMenu("Меню настроек")
            {
                MenuItems =
                {
                    new ConsoleMenuItem("Прочитать три файла (папка Files в папке выполняемого файла)", 0, (s, e) => { CheckThreeFilesLoad(); }),
                    new ConsoleMenuItem("Прочитать файлы из папки (по пути до папки)", 0, (s, e) => { CheckFileLoadByFolder(); }),
                }
            };
            menu.OnItemAdded += (sender, e) =>
            {
                menu.DisplayMenu();
            };
            menu.DisplayMenu();
        }

        private async static void CheckThreeFilesLoad()
        {
            Console.Clear();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            FileGenerator.ClearDirectory(path);
            FileGenerator.GenerateTextFilesInDirectory(path);

            var filePaths = FileGenerator.GetAllTxtFilesInDirectory(path);

            await FileSpaceCounter.MeasureTotalTimeAsync(async () =>
            {
                int spacesInFiles = await FileSpaceCounter.CountSpacesInFilesAsync(filePaths);
                Console.WriteLine();
                Console.Write($"Количество пробелов в трех файлах: {spacesInFiles}");
            });

            BaseConsoleActions.PressAnyToContinue(ShowMainMenu);
        }

        private async static void CheckFileLoadByFolder(string folderPath = "")
        {
            Console.Clear();
            folderPath = string.IsNullOrEmpty(folderPath) ? BaseConsoleActions.AskForValidDirectoryPath() : folderPath;

            await FileSpaceCounter.MeasureTotalTimeAsync(async () =>
            {
                int spacesInFolder = await FileSpaceCounter.CountSpacesInFolderAsync(folderPath);
                Console.WriteLine();
                Console.Write($"Количество пробелов во всех файлах папки: {spacesInFolder}");
            });

            BaseConsoleActions.PressAnyToContinue(ShowMainMenu);
        }
    }
}
