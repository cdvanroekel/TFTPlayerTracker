namespace Algorithm.FuzzyStrings.Tests.Models
{
    public enum AlgorithmTypes
    {
        Dice,
        LevenshteinDistance,
        LongestCommonSubsequence,
        DoubleMetaphone,
        Average
    }

    public class AlexaInterpretation
    {
        public AlexaInterpretation()
        {
        }

        public AlexaInterpretation(string originalValue, string actualValue)
        {
            OriginalValue = originalValue;
            ActualValue = actualValue;

            CalculateCoefficentValues();
        }

        private void CalculateCoefficentValues()
        {
            DiceCoefficentValue = OriginalValue.DiceCoefficient(ActualValue);

            var LevenshteinDistanceValue = OriginalValue.LevenshteinDistance(ActualValue);
            LevenshteinDistanceCoefficentValue = 1.0 / (1.0 * (LevenshteinDistanceValue + 0.2));
            LevenshteinDistanceCoefficentValue = LevenshteinDistanceCoefficentValue > .99 ? .99 : LevenshteinDistanceCoefficentValue;

            LongestCommonSubsequenceCoefficentValue = OriginalValue.LongestCommonSubsequence(ActualValue).Item2;
            LongestCommonSubsequenceCoefficentValue = LongestCommonSubsequenceCoefficentValue > .99 ? .99 : LongestCommonSubsequenceCoefficentValue;

            string originalValueDoubleMetaphone = OriginalValue.ToDoubleMetaphone();
            string ActualValueDoubleMetaphone = ActualValue.ToDoubleMetaphone();

            int matchCount = 0;
            if (originalValueDoubleMetaphone.Length == 4 && ActualValueDoubleMetaphone.Length == 4)
            {
                for (int i = 0; i < originalValueDoubleMetaphone.Length; i++)
                {
                    if (originalValueDoubleMetaphone[i] == ActualValueDoubleMetaphone[i]) matchCount++;
                }
            }
            DoubleMetaphoneCoefficentValue = matchCount == 0 ? 0.0 : matchCount / 4.0;
            DoubleMetaphoneCoefficentValue = DoubleMetaphoneCoefficentValue == 1.0 ? .90 : DoubleMetaphoneCoefficentValue;

            AverageCoefficentValue = (DiceCoefficentValue + LongestCommonSubsequenceCoefficentValue + LevenshteinDistanceCoefficentValue + DoubleMetaphoneCoefficentValue) / 4.0;
        }

        public AlgorithmTypes? AlgorithmType
        {
            get
            {
                if (DiceCoefficentValue >= LevenshteinDistanceCoefficentValue && DiceCoefficentValue >= LongestCommonSubsequenceCoefficentValue && DiceCoefficentValue >= DoubleMetaphoneCoefficentValue)
                {
                    return AlgorithmTypes.Dice;
                }

                if (LevenshteinDistanceCoefficentValue >= DiceCoefficentValue && LevenshteinDistanceCoefficentValue >= LongestCommonSubsequenceCoefficentValue && LevenshteinDistanceCoefficentValue >= DoubleMetaphoneCoefficentValue)
                {
                    return AlgorithmTypes.LevenshteinDistance;
                }

                if (LongestCommonSubsequenceCoefficentValue >= DiceCoefficentValue && LongestCommonSubsequenceCoefficentValue >= LevenshteinDistanceCoefficentValue && LongestCommonSubsequenceCoefficentValue >= DoubleMetaphoneCoefficentValue)
                {
                    return AlgorithmTypes.LongestCommonSubsequence;
                }

                if (DoubleMetaphoneCoefficentValue >= DiceCoefficentValue && DoubleMetaphoneCoefficentValue >= LevenshteinDistanceCoefficentValue && DoubleMetaphoneCoefficentValue >= LongestCommonSubsequenceCoefficentValue)
                {
                    return AlgorithmTypes.DoubleMetaphone;
                }

                return AlgorithmTypes.Average;
            }
        }

        public string OriginalValue { get; set; }

        public string ActualValue { get; set; }

        public double DiceCoefficentValue { get; set; }

        public double LevenshteinDistanceCoefficentValue { get; set; }

        public double LongestCommonSubsequenceCoefficentValue { get; set; }

        public double DoubleMetaphoneCoefficentValue { get; set; }

        public double AverageCoefficentValue { get; set; }
    }
}