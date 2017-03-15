using NUnit.Framework;
using System;
using System.Linq;

namespace AntPathMatching
{
    [TestFixture]
    public class AntDirectoryTests
    {
        [TestCase("/assets/**/*.txt", true)]
        [TestCase("/assets/**/*.xml", false)]
        [TestCase("/assets/dir1/*/file?.txt", true)]
        [TestCase("/assets/dir1/*/file???.txt", false)]
        [TestCase("/assets/**/*.*", true)]
        public void SearchRecursively_FromTable_ReturnsExpected(
            string pattern,
            bool expected)
        {
            var ant = new AntDirectory(new Ant(pattern));
            var match = ant.SearchRecursively(AppDomain.CurrentDomain.BaseDirectory);

            Assert.That(match, expected ? Is.Not.Empty : Is.Empty);
        }

        [TestCase(true), TestCase(false)]
        public void SearchRecursively_WithDirectoryPathSetting_ReturnsExpectedValue(
            bool includeDirectoryPath)
        {
            var dirPath = AppDomain.CurrentDomain.BaseDirectory;

            var ant = new AntDirectory(new Ant("/assets/dir1/*/file?.txt"));
            var match = ant.SearchRecursively(dirPath, includeDirectoryPath);

            if (includeDirectoryPath)
            {
                Assert.That(match.FirstOrDefault(), Does.Contain(dirPath));
            }
            else
            {
                Assert.That(match.FirstOrDefault(), Does.Not.Contain(dirPath));
            }
        }

        [Test]
        public void SearchRecursively_WhenDirectoryNotFound_DoesNotThrow()
        {
            var ant = new AntDirectory(new Ant("*.txt"));

            Assert.DoesNotThrow(() => ant.SearchRecursively(@"C:\Octopus\Applications\production\Containers").ToList());
        }
    }
}
