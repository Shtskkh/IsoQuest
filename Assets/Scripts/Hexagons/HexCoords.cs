using System;
using UnityEngine;

namespace Hexagons
{
    public class HexCoords
    {
        public int Q, R, S;

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
