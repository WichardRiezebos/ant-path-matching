using NUnit.Framework;
using System;

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
    }
}
