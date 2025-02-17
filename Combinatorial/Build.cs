using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinatorial
{
    // Represents a build (which may be a base piece or a composition of builds)
    public class Build
    {
        public int Width { get; }
        public int Length { get; }
        // Sum of the areas of the base pieces used
        public int PiecesArea { get; }
        // Internal trim loss is calculated as (BuildArea - PiecesArea)
        public double InternalTrimLoss { get; }
        // The key is the StockPiece.Area
        public Dictionary<int, int> BaseUsage { get; }

        public Build(int width, int length, int piecesArea, double internalTrimLoss, Dictionary<int, int> baseUsage)
        {
            Width = width;
            Length = length;
            PiecesArea = piecesArea;
            InternalTrimLoss = internalTrimLoss;
            BaseUsage = baseUsage;
        }

        public override bool Equals(object obj)
        {
            if (obj is Build other)
            {
                return Width == other.Width && Length == other.Length &&
                       PiecesArea == other.PiecesArea &&
                       InternalTrimLoss == other.InternalTrimLoss &&
                       DictionaryEqual(BaseUsage, other.BaseUsage);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Width.GetHashCode();
            hash = hash * 23 + Length.GetHashCode();
            hash = hash * 23 + PiecesArea.GetHashCode();
            hash = hash * 23 + InternalTrimLoss.GetHashCode();
            foreach (var key in BaseUsage.Keys.OrderBy(k => k))
            {
                hash = hash * 23 + key.GetHashCode();
                hash = hash * 23 + BaseUsage[key].GetHashCode();
            }
            return hash;
        }

        // Check if the dictionaries are equal
        private bool DictionaryEqual(Dictionary<int, int> d1, Dictionary<int, int> d2)
        {
            if (d1.Count != d2.Count)
                return false;

            foreach (var kv in d1)
            {
                if (!d2.TryGetValue(kv.Key, out int value) || value != kv.Value)
                    return false;
            }
            return true;
        }
    }
}
