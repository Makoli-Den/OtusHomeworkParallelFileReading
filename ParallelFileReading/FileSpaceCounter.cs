using System.Diagnostics;
using OtusHomeworkTools;

namespace ParallelFileReader
{
    internal class FileSpaceCounter
    {
        public static async Task<int> CountSpacesInFilesAsync(string[] filePaths)
        {
            if (filePaths.Length != 3)
                throw new ArgumentException("Необходимо передать три файла.", nameof(filePaths));

            var tasks = filePaths.Select(CountSpacesInFileWithTimeAsync).ToArray();
            Console.WriteLine();
            var results = await Task.WhenAll(tasks);

            return results.Sum();
        }

        public static async Task<int> CountSpacesInFolderAsync(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Директория не найдена: {folderPath}");

            var filePaths = Directory.GetFiles(folderPath, "*.txt");

            if (filePaths.Length == 0)
                return 0;

            var tasks = filePaths.Select(CountSpacesInFileWithTimeAsync).ToArray();
            Console.WriteLine();
            var results = await Task.WhenAll(tasks);

            return results.Sum();
        }


        private static async Task<int> CountSpacesInFileWithTimeAsync(string filePath)
        {
            Console.WriteLine($"Начато чтение: {filePath}");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var content = await File.ReadAllTextAsync(filePath);
            int spaceCount = content.Count(c => c == ' ');

            stopwatch.Stop();
            Console.WriteLine($"Файл: {Path.GetFileName(filePath)}, Время чтения: {stopwatch.ElapsedMilliseconds} мс, Кол-во пробелов: {spaceCount}");

            return spaceCount;
        }

        public static async Task MeasureTotalTimeAsync(Func<Task> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await action();

            stopwatch.Stop();
            Console.Write($"; Общее время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}
