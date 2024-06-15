using System.Reflection;

namespace Expendium.Transactions.Processor.Tests
{
    /// <summary>
    /// Provides helper methods for working with test files.
    /// </summary>
    public static class TestFileHelper
    {
        /// <summary>
        /// Gets the full path of a test file.
        /// </summary>
        /// <param name="relativePath">The relative path of the test file.</param>
        /// <returns>The full path of the test file.</returns>
        public static string GetTestFilePath(string relativePath)
        {
            string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            return Path.Combine(assemblyLocation, relativePath);
        }

        /// <summary>
        /// Opens a FileStream for a test file.
        /// </summary>
        /// <param name="relativePath">The relative path of the test file.</param>
        /// <returns>A FileStream for the test file.</returns>
        public static FileStream GetFileStream(string relativePath)
        {
            string fullPath = GetTestFilePath(relativePath);
            return File.OpenRead(fullPath);
        }
    }
}