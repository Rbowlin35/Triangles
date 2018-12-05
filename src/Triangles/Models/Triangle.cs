using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangles.Models;

namespace Triangles.Models
{
    public enum Alpha { A = 1, B, C, D, E, F }

    public class Triangle
    {
        internal const int sideLegth = 10;

        public Triangle(Alpha row, int col)
        {
            Row = row;
            Col = col;
        }

        public Alpha Row { get; set; }
        public int Col { get; set; }
        public string Location
        {
            get
            {
                return $"{Enum.GetName(typeof(Alpha), Row)}{Col}";
            }
        }

        public List<Point> Vertices
        {
            get
            {
                var vertices = new List<Point>();
                vertices.Add(vertexA);
                vertices.Add(vertexB);
                vertices.Add(vertexC);

                return vertices;
            }
        }

        #region Helpers
        internal bool isEvenColumn
        {
            get
            {
                return (Col % 2 == 0);
            }
        }

        internal int rootY
        {
            get
            {
                return ((int)Row - 1) * sideLegth;
            }
        }
        internal int rootX
        {
            get
            {
                return ((Col - 1) / 2) * sideLegth;
            }
        }

        internal Point vertexA
        {
            get
            {
                return new Point(rootX, rootY);
            }
        }

        internal Point vertexB
        {
            get
            {
                return new Point(rootX + sideLegth, rootY + sideLegth);
            }
        }

        internal Point vertexC
        {
            get
            {
                return new Point(rootX + (isEvenColumn ? sideLegth : 0), rootY + (isEvenColumn ? 0 : sideLegth));
            }
        }
        #endregion

        #region Static Methods
        internal static bool TryFind(string id, out Triangle triangle)
        {
            triangle = null;
            Alpha row;
            int col;
            if ( Triangle.TryParse(id, out row, out col))
            {
                triangle = TraingleFactory.Triangles.Where(t => t.Row == row && t.Col == col).First();
            }

            return triangle != null;
        }

        internal static bool TryParse(string id, out Alpha row, out int col)
        {
            row = Alpha.A;
            col = 0;

            var alpha = id.FirstOrDefault().ToString().ToUpper();
            Alpha a;
            if (!Enum.TryParse(alpha, out a))
                return false;
            else
                row = a;

            var colString = id.ToUpper().Replace(alpha, "");
            if (!Int32.TryParse(colString, out col))
                return false;

            return true;
        }

        internal static bool TryFind(List<Point> list, out Triangle triangle)
        {
            triangle = null;
            foreach( Triangle item in TraingleFactory.Triangles)
            {
                bool match = true;
                foreach (var v in item.Vertices)
                {
                    if (!list.Contains(v))
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    triangle = item;
                    break;
                }
            }

            return triangle != null;
        }
        #endregion
    }
}