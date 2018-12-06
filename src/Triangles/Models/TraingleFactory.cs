using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triangles.Models
{
    public class TriangleFactory
    {
        internal static List<Triangle> ListTriangles()
        {
            var triangles = new List<Triangle>();
            foreach (Alpha a in Enum.GetValues(typeof(Alpha)))
            {
                for (int i = 1; i < 13; i++)
                {
                    triangles.Add(new Triangle(a, i));
                }
            }

            return triangles;
        }

    }
}
