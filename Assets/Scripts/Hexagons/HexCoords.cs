using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagons
{
    public class HexCoords : IEquatable<HexCoords>
    {
        public readonly int Q, R, S;

        public HexCoords(int q, int r)
        {
            Q = q;
            R = r;
            S = -q - r;
            
            if (Q + R + S != 0)
                throw new Exception("Q + R + S != 0");
        }

        public HexCoords(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
            
            if (Q + R + S != 0)
                throw new Exception("Q + R + S != 0");
        }
        
        public static readonly Dictionary<HexCoords, float> Directions = new()
        {
            { new HexCoords(1, 0, -1), 90f }, // Right
            { new HexCoords(1, -1, 0), 45f }, // Top-Right
            { new HexCoords(0, -1, 1), -45f }, // Top-Left
            { new HexCoords(-1, 0, 1), -90f }, // Left
            { new HexCoords(-1, 1, 0), -150f }, // Down-Left
            { new HexCoords(0, 1, -1), -210f } // Down-Right
        };

        public static HexCoords operator +(HexCoords a, HexCoords b)
        {
            return new HexCoords(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static HexCoords operator -(HexCoords a, HexCoords b)
        {
            return new HexCoords(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static HexCoords operator *(HexCoords a, int k)
        {
            return new HexCoords(a.Q * k, a.R * k, a.S * k);
        }

        public bool Equals(HexCoords other)
        {
            return other != null && Q == other.Q && R == other.R && S == other.S;
        }

        public override bool Equals(object obj)
        {
            return obj is HexCoords other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Q, R, S);
        }

        public static int Length(HexCoords hex)
        {
            return (Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2;
        }

        public static int Distance(HexCoords a, HexCoords b)
        {
            return Length(a-b);
        }

        public static Vector3Int ToVector3Int(HexCoords hexCoords)
        {
            return new Vector3Int(hexCoords.Q, hexCoords.R, hexCoords.S);
        }

        public static HexCoords FromVector3Int(Vector3Int hexCoords)
        {
            return new HexCoords(hexCoords[0], hexCoords[1], hexCoords[2]);
        }

        public static HexCoords FromPosition(Vector3 position, float hexSize)
        {
            var x = position.x / hexSize;
            var z = -position.z / hexSize;

            var q = Mathf.Sqrt(3f) / 3f * x - 1f / 3f * z;
            var r =  2f / 3f * z;
            
            return RoundToHex(q, r);
        }
        
        private static HexCoords RoundToHex(float q, float r) {
            var s = -q - r;

            var roundQ = Mathf.RoundToInt(q);
            var roundR = Mathf.RoundToInt(r);
            var roundS = Mathf.RoundToInt(s);

            var deltaQ = Mathf.Abs(roundQ - q);
            var deltaR = Mathf.Abs(roundR - r);
            var deltaS = Mathf.Abs(roundS - s);

            if (deltaQ > deltaR && deltaQ > deltaS) {
                roundQ = -roundR - roundS;
            }
            else if (deltaR > deltaS) {
                roundR = -roundQ - roundS;
            }
            else {
                roundS = -roundQ - roundR;
            }

            return new HexCoords(roundQ, roundR, roundS);
        }
    }
}
