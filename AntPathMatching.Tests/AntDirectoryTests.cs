using NUnit.Framework;
using System;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FluentAssertions;
using XFS = System.IO.Abstractions.TestingHelpers.MockUnixSupport;

namespace AntPathMatching
{
    [TestFixture]
    public class AntDirectoryTests
    {
        [Test]
        public void SearchRecursively_RecursiveFilesExist_ReturnsTwoFiles()
        {
            // Arrange
            var mockFileSystem = CreateMockFileSystem();

            var ant = new AntDirectory(new Ant("/assets/**/*.txt"), mockFileSystem);

            // Act
            var files = ant.SearchRecursively(XFS.Path(@"C:\")).ToList();

            // Assert
            files.Should().HaveCount(2);
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file1.txt"));
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file2.txt"));
        }

        [Test]
        public void SearchRecursively_RecursiveFilesDoNotExist_ReturnsEmpty()
        {
            // Arrange
            var mockFileSystem = CreateMockFileSystem();

            var ant = new AntDirectory(new Ant("/assets/**/*.xml"), mockFileSystem);

            // Act
            var files = ant.SearchRecursively(XFS.Path(@"C:\"));

            // Assert
            files.Should().BeEmpty();
        }

        [Test]
        public void SearchRecursively_PatternWithQuestionMark_ReturnsTwoFiles()
        {
            // Arrange
            var mockFileSystem = CreateMockFileSystem();

            var ant = new AntDirectory(new Ant("/assets/dir1/*/file?.txt"), mockFileSystem);

            // Act
            var files = ant.SearchRecursively(XFS.Path(@"C:\")).ToList();

            // Assert
            files.Should().HaveCount(2);
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file1.txt"));
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file2.txt"));
        }

        [Test]
        public void SearchRecursively_PatternWithThreeQuestionMarks_ReturnsEmpty()
        {
            // Arrange
            var mockFileSystem = CreateMockFileSystem();

            var ant = new AntDirectory(new Ant("/assets/dir1/*/file???.txt"), mockFileSystem);

            // Act
            var files = ant.SearchRecursively(XFS.Path(@"C:\"));

            // Assert
            files.Should().BeEmpty();
        }

        [Test]
        public void SearchRecursively_PatternRecursiveWithWildcard_ReturnsThreeFiles()
        {
            // Arrange
            var mockFileSystem = CreateMockFileSystem();

            var ant = new AntDirectory(new Ant("/assets/**/*.*"), mockFileSystem);

            // Act
            var files = ant.SearchRecursively(XFS.Path(@"C:\")).ToList();

            // Assert
            files.Should().HaveCount(3);
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file1.txt"));
            files.Should().Contain(f => f == XFS.Path(@"assets\dir1\dir1_1\file2.txt"));
            files.Should().Contain(f => f == XFS.Path(@"assets\dir2\file2.zip"));
        }

        private static MockFileSystem CreateMockFileSystem()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(XFS.Path(@"C:\assets\dir1\dir1_1\file1.txt"), new MockFileData("data"));
            mockFileSystem.AddFile(XFS.Path(@"C:\assets\dir1\dir1_1\file2.txt"), new MockFileData("data"));
            mockFileSystem.AddFile(XFS.Path(@"C:\assets\dir2\file2.zip"), new MockFileData("data"));
            return mockFileSystem;
        }

        [TestCase(true), TestCase(false)]
        public void SearchRecursively_WithDirectoryPathSetting_ReturnsExpectedValue(
            bool includeDirectoryPath)
        {
            var ant = new AntDirectory(new Ant("/assets/dir1/*/file?.txt"), CreateMockFileSystem());
            var match = ant.SearchRecursively(XFS.Path(@"C:\"), includeDirectoryPath);

            if (includeDirectoryPath)
            {
                match.First().Should().StartWith(XFS.Path(@"C:\"));
            }
            else
            {
                match.First().Should().NotStartWith(XFS.Path(@"C:\"));
            }
        }

        [Test]
        public void SearchRecursively_WhenDirectoryNotFound_DoesNotThrow()
        {
            var ant = new AntDirectory(new Ant("*.txt"), new MockFileSystem());

            Assert.DoesNotThrow(() => ant.SearchRecursively(XFS.Path(@"C:\Octopus\Applications\production\Containers")).ToList());
        }
    }
}
