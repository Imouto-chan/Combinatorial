using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinatorial
{
    public class Algorithm
    {
        public struct StockPiece
        {
            public int Length;
            public int Width;
            public int Amount;
            public int Area;

            public StockPiece(int _length, int _width, int _amount)
            {
                Length = _length;
                Width = _width;
                Amount = _amount;
                Area = _length * _width;
            }
        };

        public StockPiece[] pieces =   
            {
                new StockPiece(2, 1, 1),
                new StockPiece(3, 2, 2),
                new StockPiece(3, 3, 2),
                new StockPiece(3, 4, 5),
                new StockPiece(8, 2, 3),
                new StockPiece(3, 7, 1),
                new StockPiece(8, 4, 2)
            };

        public static int SheetLength = 15;
        public static int SheetWidth = 10;
        public const float Beta = 0.00f;
        public static int SheetArea = SheetWidth * SheetLength;

        public Build RunWangAlgorithm(List<StockPiece> pieces, int sheetWidth, int sheetLength, double beta, int sheetArea)
        {
            // L will store all builds
            HashSet<Build> L = new HashSet<Build>();

            // Initialize L(0) with the base pieces (including rotated versions)
            foreach (var sp in pieces)
            {
                // Usage keeps track of how many times a piece is used in a build using the Area
                Dictionary<int, int> usage = new Dictionary<int, int> { { sp.Area, 1 } };
                Build b = new Build(sp.Width, sp.Length, sp.Area, 0, usage);
                L.Add(b);
                // Add rotated version if dimensions differ
                if (sp.Length != sp.Width)
                {
                    Build bRot = new Build(sp.Length, sp.Width, sp.Area, 0, usage);
                    L.Add(bRot);
                }
            }

            Build bestSolution = null;
            double bestTotalTL = double.MaxValue;
            bool newBuildFound = true;

            // Generate new builds
            while (newBuildFound)
            {
                newBuildFound = false;
                HashSet<Build> F_next = new HashSet<Build>();
                List<Build> currentList = L.ToList();

                // Try all ordered pairs (A, B) from the current set
                for (int i = 0; i < currentList.Count; i++)
                {
                    for (int j = 0; j < currentList.Count; j++)
                    {
                        Build A = currentList[i];
                        Build B = currentList[j];

                        // Horizontal join: place A and B side by side
                        int newWidth = A.Width + B.Width;
                        int newLength = Math.Max(A.Length, B.Length);

                        // Make sure it fits
                        if (newWidth <= sheetWidth && newLength <= sheetLength)
                        {
                            int newArea = newWidth * newLength;
                            int piecesArea = A.PiecesArea + B.PiecesArea; // How much area the pieces in each build in total take up
                            double internalTL = newArea - piecesArea; // Internal trim loss

                            // Satisfies trim loss <= beta * sheet area
                            if (internalTL <= beta * sheetArea)
                            {
                                Dictionary<int, int> usage = CombineUsage(A.BaseUsage, B.BaseUsage);

                                // Make sure we are not using too many of a piece
                                if (ValidateUsage(usage, pieces))
                                {
                                    Build newBuild = new Build(newWidth, newLength, piecesArea, internalTL, usage);
                                    double totalTL = (sheetArea - piecesArea) + internalTL;

                                    // If this build is better we use it
                                    if (totalTL < bestTotalTL)
                                    {
                                        bestSolution = newBuild;
                                        bestTotalTL = totalTL;
                                    }

                                    // If we don't already have this build, add it
                                    if (!L.Contains(newBuild))
                                    {
                                        F_next.Add(newBuild);
                                        newBuildFound = true;
                                    }
                                }
                            }
                        }

                        // Vertical join: place A above B.
                        int newWidthV = Math.Max(A.Width, B.Width);
                        int newLengthV = A.Length + B.Length;
                        if (newWidthV <= sheetWidth && newLengthV <= sheetLength)
                        {
                            int newAreaV = newWidthV * newLengthV;
                            int piecesAreaV = A.PiecesArea + B.PiecesArea; // How much area the pieces in each build in total take up
                            double internalTLV = newAreaV - piecesAreaV; // Internal trim loss

                            // Satisfies trim loss <= beta * sheet area
                            if (internalTLV <= beta * sheetArea)
                            {
                                Dictionary<int, int> usage = CombineUsage(A.BaseUsage, B.BaseUsage);

                                // Make sure we are not using too many of a piece
                                if (ValidateUsage(usage, pieces))
                                {
                                    Build newBuildV = new Build(newWidthV, newLengthV, piecesAreaV, internalTLV, usage);
                                    double totalTL = (sheetArea - piecesAreaV) + internalTLV;

                                    // If this build is better we use it
                                    if (totalTL < bestTotalTL)
                                    {
                                        bestSolution = newBuildV;
                                        bestTotalTL = totalTL;
                                    }

                                    // If we don't already have this build, add it
                                    if (!L.Contains(newBuildV))
                                    {
                                        F_next.Add(newBuildV);
                                        newBuildFound = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // Add all new builds to L
                foreach (var build in F_next)
                {
                    L.Add(build);
                }
            }
            return bestSolution;
        }

        // Combines 2 usage dictionaries
        Dictionary<int, int> CombineUsage(Dictionary<int, int> usage1, Dictionary<int, int> usage2)
        {
            Dictionary<int, int> result = new Dictionary<int, int>(usage1);
            foreach (var kv in usage2)
            {
                if (result.ContainsKey(kv.Key))
                    result[kv.Key] += kv.Value;
                else
                    result[kv.Key] = kv.Value;
            }
            return result;
        }

        // Checks that the usage of each Piece does not exceed its limit
        bool ValidateUsage(Dictionary<int, int> usage, List<StockPiece> pieces)
        {
            foreach (var kv in usage)
            {
                // Find the StockPiece using the key in the dictionary
                var sp = pieces.FirstOrDefault(x => x.Area == kv.Key);
                if (sp.Amount < kv.Value)
                    return false;
            }
            return true;
        }
    }
}

