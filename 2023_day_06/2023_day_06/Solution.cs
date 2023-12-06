using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_day_06 {
    internal class Solution {
        public ulong Result_PartOne;
        public ulong Result_PartOneBetter;
        public List<ulong> Times = new List<ulong>();
        public List<ulong> Distances = new List<ulong>();
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            uint Impossiblesum = 0;
            string[] lineArray;
            //fist line
            lineOfText = reader.ReadLine();
            lineArray = lineOfText.Split(":");
            foreach(string item in lineArray[1].Trim().Split()) {
                if (string.IsNullOrEmpty(item)) {
                    continue;
                }
                Times.Add(Convert.ToUInt64(item));  
            }
            //second line
            lineOfText = reader.ReadLine();
            lineArray = lineOfText.Split(":");
            foreach (string item in lineArray[1].Trim().Split()) {
                if (string.IsNullOrEmpty(item)) {
                    continue;
                }
                Distances.Add(Convert.ToUInt64(item));
            }

        }

        public void PartOne() {
            //Part One and PartTwo
            Result_PartOne = 1;
            for (int Race = 0; Race < Times.Count; Race++) {
                ulong Wins = 0;
                for(uint accTime =0;accTime <= Times[Race]; accTime++) {
                    ulong Timespentmoving = Times[Race] - accTime;
                    ulong Distance = Timespentmoving * accTime;
                    if(Distance > Distances[Race]) {
                        Wins++;
                    }
                }
                Result_PartOne *= Wins;
            }
        }

        public void BetterSolution() {
            Result_PartOneBetter = 1;
            for (int Race = 0; Race < Times.Count; Race++) {
                QuadraticEquation equation = new QuadraticEquation(1, Times[Race], Distances[Race]);
                double[] roots = equation.SolveEquation();
                Result_PartOneBetter *=(ulong)( roots[1] -roots[0]);
            }
            // Solve the equation and get the roots.

        }
        public class QuadraticEquation {
            public double A { get; private set; }
            public double B { get; private set; }
            public double C { get; private set; }

            // <summary>
            // Constructs a new quadratic equation using the provided coefficients.
            //
            // Parameters:
            // - a: Coefficient for the quadratic term (x^2) in the equation.
            // - b: Coefficient for the linear term (x) in the equation.
            // - c: The constant term in the equation.
            // </summary>
            public QuadraticEquation(double a, double b, double c) {
                // Assign coefficients.
                A = a; // Set the quadratic coefficient.
                B = b; // Set the linear coefficient.
                C = c; // Set the constant term.
            }

            // <summary>
            // Solves the quadratic equation and calculates the roots.
            //
            // Returns:
            // - An array of doubles representing the roots of the equation.
            // - If the equation has two real roots, the array will contain both roots.
            // - If the equation has one real root, the array will contain that root twice.
            // - If the equation has no real roots, the array will be empty.
            // </summary>
            public double[] SolveEquation() {
                double discriminant = B * B - 4 * A * C;

                if (discriminant > 0) {
                    double root1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                    double root2 = (-B - Math.Sqrt(discriminant)) / (2 * A);
                    return new double[] { root1, root2 };
                } else if (discriminant == 0) {
                    double root = -B / (2 * A);
                    return new double[] { root, root };
                } else {
                    return new double[] { };
                }
            }
        }

    }
}
