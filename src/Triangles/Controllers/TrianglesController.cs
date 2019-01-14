using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Http;
using Triangles.Models;

namespace Triangles.Controllers
{

    public class TrianglesController : ApiController
    {
        // GET api/triangles
        /// <summary>
        /// Get a list of all the triangles on the board
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Triangle> Get()
        {
            return TriangleFactory.ListTriangles();
        }

        // GET api/triangles/A12
        /// <summary>
        /// Get a single triangle based on row and column (format of [A-F][1-12]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(string id)
        {
            Triangle triangle;
            if (Triangle.TryFind(id, out triangle))
                return JsonConvert.SerializeObject(triangle);
            else
                return "Error: Invalid Id.  Must be in [A-F][1-12] range.";
        }

        /// <summary>
        /// Get a single triangle based on it's vertices
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        [HttpGet]
        public string Vertices(string vertices)
        {
            Triangle triangle;
            List<Point> list;
            if (TryParse(vertices, out list))
            {
                if (Triangle.TryFind(list, out triangle))
                    return triangle.Location;
            }

            return "Error: Invalid Vertices.";
        }

        private bool TryParse(string vertices, out List<Point> list)
        {
            try
            {
                list = JsonConvert.DeserializeObject<List<Point>>(vertices);
                return true;
            }
            catch(Exception ex)
            {
                list = null;
                return false;
            }
        }
    }
}
