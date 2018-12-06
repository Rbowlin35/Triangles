using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Triangles;
using Triangles.Controllers;
using Triangles.Models;

namespace Triangles.Tests.Controllers
{
    [TestFixture]
    public class ValuesControllerTest
    {
        [Test]
        public void Get()
        {
            // Arrange
            TrianglesController controller = new TrianglesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(72, result.Count());
            Assert.AreEqual("{\"Row\":1,\"Col\":1,\"Location\":\"A1\",\"Vertices\":[\"0, 0\",\"10, 10\",\"0, 10\"]}", result.ElementAt(0));
        }

        [Test]
        [TestCase("A1", "{\"Row\":1,\"Col\":1,\"Location\":\"A1\",\"Vertices\":[\"0, 0\",\"10, 10\",\"0, 10\"]}")]
        [TestCase("a2", "{\"Row\":1,\"Col\":2,\"Location\":\"A2\",\"Vertices\":[\"0, 0\",\"10, 10\",\"10, 0\"]}")]
        [TestCase("B1", "{\"Row\":2,\"Col\":1,\"Location\":\"B1\",\"Vertices\":[\"0, 10\",\"10, 20\",\"0, 20\"]}")]
        [TestCase("B2", "{\"Row\":2,\"Col\":2,\"Location\":\"B2\",\"Vertices\":[\"0, 10\",\"10, 20\",\"10, 10\"]}")]
        [TestCase("B3", "{\"Row\":2,\"Col\":3,\"Location\":\"B3\",\"Vertices\":[\"10, 10\",\"20, 20\",\"10, 20\"]}")]
        [TestCase("B4", "{\"Row\":2,\"Col\":4,\"Location\":\"B4\",\"Vertices\":[\"10, 10\",\"20, 20\",\"20, 10\"]}")]
        [TestCase("F11", "{\"Row\":6,\"Col\":11,\"Location\":\"F11\",\"Vertices\":[\"50, 50\",\"60, 60\",\"50, 60\"]}")]
        [TestCase("F12", "{\"Row\":6,\"Col\":12,\"Location\":\"F12\",\"Vertices\":[\"50, 50\",\"60, 60\",\"60, 50\"]}")]
        [TestCase("G1", "Error: Invalid Id.  Must be in [A-F][1-12] range.")]
        [TestCase("A0", "Error: Invalid Id.  Must be in [A-F][1-12] range.")]
        [TestCase("A23", "Error: Invalid Id.  Must be in [A-F][1-12] range.")]
        public void GetById(string input, string expected)
        {
            // Arrange
            TrianglesController controller = new TrianglesController();

            // Act
            string result = controller.Get(input);

            // Assert
            Assert.AreEqual(expected, result, input);
        }

        [Test]
        [TestCase("[\"0, 0\",\"10, 10\",\"0, 10\"]", "A1")]
        [TestCase("[\"0, 0\",\"10, 10\",\"10, 0\"]", "A2")]
        [TestCase("[\"50, 50\",\"60, 60\",\"60, 50\"]", "F12")]
        [TestCase("Invalid", "Error: Invalid Vertices.")]
        public void GetByVertices(string input, string expected)
        {
            // Arrange
            TrianglesController controller = new TrianglesController();

            // Act
            string result = controller.Vertices(input);

            // Assert
            Assert.AreEqual(expected, result, input);
        }


    }
}
