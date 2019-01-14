using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Triangles.Models
{
    public enum Alpha { A = 1, B, C, D, E, F }

    public class Triangle
    {
        internal const int sideLegth = 100;

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

        public int CSSTop
        {
            get
            {
                return rootY;
            }
        }

        public int CSSLeft
        {
            get
            {
                return rootX;
            }
        }

        public string CSSStyle
        {
            get
            {
                return isEvenColumn ? "right" : "left";
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
                triangle = new Triangle(row, col);
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

            return col > 0 && col < 13;
        }

        internal static bool TryFind(List<Point> list, out Triangle triangle)
        {
            triangle = null;
            var centroid = GetCentroid(list);

            int row = GetRow(centroid);
            int col = GetCol(centroid);

            if (row > 0 && row <= (int)Alpha.F
                && col > 0 && col < 13)
                triangle = new Triangle((Alpha)row, col);

            return triangle != null;
        }

        private static int GetCol(Point centroid)
        {
            return (((centroid.Y / sideLegth) + 1) * ((centroid.X > centroid.Y) ? 2 : 1));
        }

        private static int GetRow(Point centroid)
        {
            return (centroid.X / sideLegth) + 1;
        }

        internal static Point GetCentroid(List<Point> list)
        {
            return new Point()
            {
                X = list.Sum(s => s.X) / 3,
                Y = list.Sum(s => s.Y) / 3
            };

        }
        #endregion
    }
}
