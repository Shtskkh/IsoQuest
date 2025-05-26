using System;
using UnityEngine;

namespace Hexagons
{
    public class HexCoords
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

        public static readonly HexCoords[] Directions = {
            new (1, 0, -1), // Right
            new (1, -1, 0), // Top-Right
            new (0, -1, 1), // Top-Left
            new (-1, 0, 1), // Left
            new (-1, 1, 0), // Down-Left
            new (0, 1, -1) // Down-Right
        };

        public static readonly float[] RotationAngles =
        {
            0, 60, 120, 180, 240, 300
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

        public static int Length(HexCoords hex)
        {
            return (Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2;
        }

        public static int Distance(HexCoords a, HexCoords b)
        {
            return Length(a-b);
        }

        public static HexCoords Direction(int direction)
        {
            return Directions[direction % 6];
        }

        public static HexCoords Neighbor(HexCoords hex, int direction)
        {
            return hex + Direction(direction);
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
