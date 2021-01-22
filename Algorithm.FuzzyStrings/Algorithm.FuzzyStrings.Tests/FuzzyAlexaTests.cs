using Algorithm.FuzzyStrings.Tests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Algorithm.FuzzyStrings.Tests
{
    public class FuzzyAlexaTests
    {
        private string _appPath => @"C:\_CMM\git\applications\alexa.apps\Algorithm.FuzzyStrings\Alexa.FuzzyMLNet\";

        private readonly ITestOutputHelper output;

        public FuzzyAlexaTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Validates PhValidation for Train + Test Data set
        /// </summary>
        [Fact]
        public void AlexaVoicePhValidationTests()
        {
            List<AlexaInterpretation> results = new List<AlexaInterpretation>();

            var prospectDataExportList = File.ReadAllLines(Path.Combine(_appPath, "Data", "MAFord_VoicePhValidation_Export.csv"));

            foreach (var data in prospectDataExportList)
            {
                var dataSplit = data.Split(",");
                var dataOriginalWithoutQuotes = dataSplit[5].Replace("\"", "");
                var dataActualWithoutQuotes = dataSplit[6].Replace("\"", "");

                var alexaInterpretation = new AlexaInterpretation(dataOriginalWithoutQuotes, dataActualWithoutQuotes);
                output.WriteLine($"{alexaInterpretation.ActualValue}: {alexaInterpretation.OriginalValue} || {alexaInterpretation.AlgorithmType} | {alexaInterpretation.AverageCoefficentValue}");

                results.Add(alexaInterpretation);
            }

            // Shuffle
            var shuffledResults = results.OrderBy(a => Guid.NewGuid()).ToList();

            // Export
            var trainResults = shuffledResults.Take(results.Count / 2).ToList();
            CreateCsvFileFromList(trainResults, @"C:\Users\cmasden\Desktop\AlexaInterpretation_Train.csv");

            var testResults = shuffledResults.Skip(results.Count / 2).ToList();
            CreateCsvFileFromList(testResults, @"C:\Users\cmasden\Desktop\AlexaInterpretation_Test.csv");
        }

        /// <summary>
        /// Scrambles MAFord Prospect Data Export
        /// </summary>
        [Fact]
        public void AlexaInterpretationScrambleProspectTests()
        {
            List<AlexaInterpretation> results = new List<AlexaInterpretation>();

            var prospectDataExportList = File.ReadAllLines(Path.Combine(_appPath, "Data", "MAFord_Prospect_Export.txt"));

            foreach (var data in prospectDataExportList)
            {
                var dataWithoutQuotes = data.Replace("\"", "");
                var result = scrambleAlexaInterpretation(dataWithoutQuotes);

                results.AddRange(result);
            }

            foreach (var result in results)
            {
                output.WriteLine($"{result.ActualValue}: {result.OriginalValue} || {result.AlgorithmType} | {result.AverageCoefficentValue}");
            }
        }

        /// <summary>
        /// Scrambles MAFord Part Data Export
        /// </summary>
        [Fact]
        public void AlexaInterpretationScramblePartTests()
        {
            List<AlexaInterpretation> results = new List<AlexaInterpretation>();

            var partDataExportList = File.ReadAllLines(Path.Combine(_appPath, "Data", "MAFord_Part_Export.txt"));

            foreach (var data in partDataExportList)
            {
                var dataWithoutQuotes = data.Replace("\"", "");
                var result = scrambleAlexaInterpretation(dataWithoutQuotes);

                results.AddRange(result);
            }

            foreach (var result in results)
            {
                output.WriteLine($"{result.ActualValue}: {result.OriginalValue} || {result.AlgorithmType} | {result.AverageCoefficentValue}");
            }
        }

        /// <summary>
        /// Scrambles data based on a subsample of Alexa responses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<AlexaInterpretation> scrambleAlexaInterpretation(string input)
        {
            var alexaInterpretationList = new List<AlexaInterpretation>();

            if (input.Contains(" "))
            {
                alexaInterpretationList.Add(new AlexaInterpretation(input.Replace(" ", string.Empty), input));
            }
            else
            {
                alexaInterpretationList.Add(new AlexaInterpretation(Regex.Replace(input, "(\\B[A-Z])", " $1"), input));
            }

            var equivalentWordList = GetEquivalentWordList();

            foreach (var equivalentWords in equivalentWordList)
            {
                foreach (var word in equivalentWords)
                {
                    if (input.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        var wordListReplacements = equivalentWords.Where(w => w != word).ToList();

                        foreach (var wordReplacement in wordListReplacements)
                        {
                            alexaInterpretationList.Add(new AlexaInterpretation(Regex.Replace(input, word, wordReplacement, RegexOptions.IgnoreCase), input));
                        }
                    }
                }
            }

            return alexaInterpretationList;
        }

        private List<List<string>> GetEquivalentWordList()
        {
            var equivalentWordList = new List<List<string>>
            {
                new List<string> { " bee "," be ", "b" },
                new List<string> { " sea "," see ", "c" },
                new List<string> { " dee ","d" },
                new List<string> { " jay ","j" },
                new List<string> { " kay ","k" },
                new List<string> { " in ","n" },
                new List<string> { " you ", "u" },
                new List<string> { " pee ", " pea ", "p" },
                new List<string> { " tea ", " tee ", "t" },
                new List<string> { " why ", " wye ", "y" },

                new List<string> { " dash ", "-" },
                new List<string> { " slash ", "/", @"\" },

                new List<string> { " zero ", "o","0" },
                new List<string> { " one ", "1" },
                new List<string> { " two ", " too ", " to ", "2" },
                new List<string> { " three ", "3" },
                new List<string> { " four ", " for ", "4" },
                new List<string> { " five ", "5" },
                new List<string> { " six ", "6" },
                new List<string> { " seven ", "7" },
                new List<string> { " eight ", "8" },
                new List<string> { " nine ", "9" },
            };

            return equivalentWordList;
        }

        /// <summary>
        /// Creates a CSV File of a List<T>
        /// </summary>
        /// <remarks>
        /// NMS
        /// </remarks>
        private void CreateCsvFileFromList<T>(List<T> list, string filePath)
        {
            if (list == null || list.Count == 0) return;

            string newLine = Environment.NewLine;

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            var isFileEmpty = string.IsNullOrWhiteSpace(File.ReadAllText(filePath));

            using (var sw = new StreamWriter(filePath, true))
            {
                object o = Activator.CreateInstance(list[0].GetType());
                PropertyInfo[] props = o.GetType().GetProperties();

                if (isFileEmpty)
                {
                    sw.Write(string.Join(",", props.Select(d => d.Name).ToArray()) + newLine);
                }

                foreach (T item in list)
                {
                    var row = string.Join(",", props.Select(d => item.GetType()
                                                                    .GetProperty(d.Name)
                                                                    .GetValue(item, null)
                                                                    .ToString())
                                                            .ToArray());
                    sw.Write(row + newLine);
                }
            }
        }
    }
}