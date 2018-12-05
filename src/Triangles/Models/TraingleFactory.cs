using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triangles.Models
{
    public class TraingleFactory
    {
        internal static List<Triangle> triangles;

        internal static void CreateTriangles()
        {
            if (triangles == null)
            {
                triangles = new List<Triangle>();
                foreach (Alpha a in Enum.GetValues(typeof(Alpha)))
                {
                    for (int i = 1; i < 13; i++)
                    {
                        triangles.Add(new Triangle(a, i));
                    }
                }
            }
        }

        public static List<Triangle> Triangles
        {
            get
            {
                CreateTriangles();
                return triangles;
            }
        }

    }
}