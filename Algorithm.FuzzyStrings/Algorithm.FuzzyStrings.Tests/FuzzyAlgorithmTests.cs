﻿using Xunit;
using Xunit.Abstractions;

namespace Algorithm.FuzzyStrings.Tests
{
    public class FuzzyAlgorithmTests
    {
        private readonly ITestOutputHelper output;

        public FuzzyAlgorithmTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void FuzzyMatchTests(string input, string match)
        {
            var result = input.FuzzyMatch(match);
            Assert.True(result > 0.0);
            output.WriteLine($"FuzzyMatch of \"{match}\" against \"{input}\" was {result}.");
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void DiceCoefficientTests(string input, string match)
        {
            var result = input.DiceCoefficient(match);
            Assert.True(result >= 0.0);
            output.WriteLine($"DiceCoefficient of \"{match}\" against \"{input}\" was {result}.");
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void LevenshteinDistanceTests(string input, string match)
        {
            var result = input.LevenshteinDistance(match);
            Assert.True(result > 0);
            output.WriteLine($"LevenshteinDistance of \"{match}\" against \"{input}\" was {result}.");
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void LongestCommonSubsequenceTests(string input, string match)
        {
            var result = input.LongestCommonSubsequence(match);
            Assert.True(result.Item2 >= 0.0);
            output.WriteLine($"LongestCommonSubsequence of \"{match}\" against \"{input}\" was \"{result.Item1}\", {result.Item2}.");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("Adams")]
        [InlineData("Benson")]
        [InlineData("Geralds")]
        [InlineData("Johannson")]
        [InlineData("Johnson")]
        [InlineData("Jensen")]
        [InlineData("Jordon")]
        [InlineData("Madsen")]
        [InlineData("Stratford")]
        [InlineData("Wilkins")]
        [InlineData("2689 East Milkin Ave.")]
        [InlineData("85 Morrison")]
        [InlineData("2350 North Main")]
        [InlineData("567 West Center Street")]
        [InlineData("2130 Fort Union Boulevard")]
        [InlineData("2310 S. Ft. Union Blvd.")]
        [InlineData("98 West Fort Union")]
        [InlineData("Rural Route 2 Box 29")]
        [InlineData("PO Box 3487")]
        [InlineData("3 Harvard Square")]
        public void ToDoubleMetaphoneTests(string input)
        {
            var result = input.ToDoubleMetaphone();
            Assert.NotNull(result);
            output.WriteLine($"ToDoubleMetaphone of \"{input}\" was {result}.");
        }
    }
}