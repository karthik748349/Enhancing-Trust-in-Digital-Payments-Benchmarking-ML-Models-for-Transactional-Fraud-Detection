using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bank
{
    public class MlAlgo
    {
        // ---------- INTERNAL TRAINING DATA ----------
        private double[][] trainingData =
        {
        new double[]{1200, 6, 1, 1},
        new double[]{250, 1, 0, 0},
        new double[]{1800, 7, 1, 1},
        new double[]{300, 1, 0, 0},
        new double[]{950, 4, 1, 0}
    };

        private int[] labels = { 1, 0, 1, 0, 1 };

        // ---------- COMMON UTILITY ----------
        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double Normalize(double value, double max)
        {
            return value / max;
        }

        // ---------- LOGISTIC REGRESSION ----------
        private int LogisticRegression(double[] input)
        {
            double[] weights = { 0.35, 0.25, 0.20, 0.20 };
            double bias = -0.4;

            double z = bias;
            for (int i = 0; i < input.Length; i++)
            {
                z += input[i] * weights[i];
            }

            double probability = Sigmoid(z);
            return probability >= 0.5 ? 1 : 0;
        }

        // ---------- KNN ----------
        private int KNN(double[] input, int k)
        {
            List<Tuple<double, int>> distances = new List<Tuple<double, int>>();

            for (int i = 0; i < trainingData.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    sum += Math.Pow(input[j] - trainingData[i][j], 2);
                }

                double distance = Math.Sqrt(sum);
                distances.Add(new Tuple<double, int>(distance, labels[i]));
            }

            distances.Sort((a, b) => a.Item1.CompareTo(b.Item1));

            int fraudVotes = 0;
            for (int i = 0; i < k; i++)
            {
                fraudVotes += distances[i].Item2;
            }

            return fraudVotes > k / 2 ? 1 : 0;
        }

        // ---------- SVM ----------
        private int SVM(double[] input)
        {
            double[] weights = { 0.6, 0.15, 0.15, 0.10 };
            double bias = -1.2;

            double decision = bias;
            for (int i = 0; i < input.Length; i++)
            {
                decision += weights[i] * input[i];
            }

            return decision >= 0 ? 1 : 0;
        }

        // ---------- CNN (SIMULATED) ----------
        private int CNN(double[] input)
        {
            double[] kernel = { 0.25, 0.35, 0.30, 0.10 };
            double convolution = 0;

            for (int i = 0; i < input.Length; i++)
            {
                convolution += input[i] * kernel[i];
            }

            // ReLU Activation
            if (convolution < 0)
                convolution = 0;

            // Fully Connected Layer
            double fcWeight = 0.8;
            double output = convolution * fcWeight;

            return output > 500 ? 1 : 0;
        }

        // ---------- MAIN FRAUD PREDICTION ----------
        public string PredictFraud(double amount, int txnCount, int locationRisk, int timeRisk)
        {
            // Feature normalization
            double[] input =
            {
            Normalize(amount, 2000),
            Normalize(txnCount, 10),
            locationRisk,
            timeRisk
        };

            int lrResult = LogisticRegression(input);
            int knnResult = KNN(input, 3);
            int svmResult = SVM(input);
            int cnnResult = CNN(input);

            int finalScore = lrResult + knnResult + svmResult + cnnResult;

            if (finalScore >= 3)
                return "Fraudulent Transaction";
            else
                return "Legitimate Transaction";
        }
    }
}