using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

class Program
{
    static void Main(string[] args)
    {
        const string directoryPath = "/home/jadatix/dotnetLabs";

        Console.Write("Введіть рядок для пошуку: ");
        string? searchQuery = Console.ReadLine();

        var files = Directory.GetFiles(directoryPath, "*.txt");

        var progress = new Dictionary<string, int>();
        var results = new Dictionary<string, int>();

        var progressLock = new object();
        var resultsLock = new object();

        var options = new ExecutionDataflowBlockOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount // Process files in parallel
        };

        var searchBlock = new TransformBlock<string, KeyValuePair<string, int>>(
            async filePath =>
            {
                int lineCount = File.ReadLines(filePath).Count();
                int processedLines = 0;
                int matches = 0;

                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.Contains(searchQuery))
                        {
                            matches += line.Split(searchQuery).Length - 1;
                        }

                        processedLines++;

                        lock (progressLock)
                        {
                            progress[filePath] = processedLines * 100 / lineCount;
                        }
                    }
                }

                lock (resultsLock)
                {
                    results[filePath] = matches;
                }

                return new KeyValuePair<string, int>(filePath, matches);
            }, options);

        var displayProgressBlock = new ActionBlock<KeyValuePair<string, int>>(
            result =>
            {
                lock (progressLock)
                {
                    Console.WriteLine($"{Path.GetFileName(result.Key)} - оброблено {progress[result.Key]}%");
                }
            });

        var storeResultsBlock = new ActionBlock<KeyValuePair<string, int>>(
            result =>
            {
                lock (resultsLock)
                {
                    Console.WriteLine($"{Path.GetFileName(result.Key)} - Кількість входжень: {results[result.Key]}");
                }
            });

        var timer = new System.Timers.Timer(500); // Таймер з періодом 0.5 секунди

        timer.Elapsed += (sender, e) =>
        {
            lock (progressLock)
            {
                foreach (var filePath in progress.Keys.ToList())
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)} - оброблено {progress[filePath]}%");
                }
            }
        };

        timer.Start();

        var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

        searchBlock.LinkTo(displayProgressBlock, linkOptions);
        searchBlock.LinkTo(storeResultsBlock, linkOptions);

        foreach (var file in files)
        {
            searchBlock.Post(file);
        }

        searchBlock.Complete();

        Task.WhenAll(displayProgressBlock.Completion, storeResultsBlock.Completion).Wait();

        timer.Stop();

        Console.WriteLine("Пошук завершено.");

        foreach (var file in results.Keys)
        {
            Console.WriteLine($"{Path.GetFileName(file)} - Кількість входжень: {results[file]}");
        }
    }
}