// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Xunit;

namespace Microsoft.Extensions.Configuration.Json
{
    public class JsonConfigurationExtensionsTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddJsonFile_ThrowsIfFilePathIsNullOrEmpty(string path)
        {
            // Arrange
            var configurationBuilder = new ConfigurationBuilder();

            // Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => JsonConfigurationExtensions.AddJsonFile(configurationBuilder, path));
            Assert.Equal("path", ex.ParamName);
            Assert.StartsWith("File path must be a non-empty string.", ex.Message);
        }

        [Fact]
        public void AddJsonFile_ThrowsIfFileDoesNotExistAtPath()
        {
            // Arrange
            var path = "file-does-not-exist.json";

            // Act and Assert
            var ex = Assert.Throws<FileNotFoundException>(() => new ConfigurationBuilder().AddJsonFile(path).Build());
            Assert.True(ex.Message.StartsWith($"The configuration file '{path}' was not found and is not optional. The physical path is '"));
        }
    }
}
