using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.IO.Path;

namespace Common
{
    public static class Testing
    {
        /// <summary>
        /// Checks that the values in two series of lists are equal
        /// </summary>
        public static void AssertNestedListsEqual<T>(IEnumerable<List<T>> expected,
                                                     IEnumerable<List<T>> actual)
        {
            foreach (var list in expected.Zip(actual, (e, a) => (Expected: e, Actual: a)))
            {
                CollectionAssert.AreEqual(list.Expected, list.Actual);
            }
        }

        /// <summary>
        /// A convenient way of loading test file data as the
        /// <see cref="DeploymentItemAttribute"/> is not implemented in
        /// this version of .NET Core
        /// </summary>
        public static string GetTestFileContents(string fileName)
        {
            var inputFile = Combine(GetTestProjectDirectory(), fileName);
            return File.ReadAllText(inputFile);
        }

        private static string GetTestProjectDirectory()
        {
            var assemblyDirectory = Assembly.GetExecutingAssembly().Location;
            return GetFullPath(Combine(assemblyDirectory, "..", "..", "..", ".."));
        }
    }
}