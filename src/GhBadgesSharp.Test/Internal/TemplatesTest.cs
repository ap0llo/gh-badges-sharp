﻿using Grynwald.GhBadgesSharp.Internal;
using Xunit;

namespace Grynwald.GhBadgesSharp.Test.Internal
{
    public class TemplatesTest
    {

        [Theory]
        [InlineData("flat-square")]
        [InlineData("flat")]
        [InlineData("for-the-badge")]
        [InlineData("plastic")]
        [InlineData("social")]
        public void GetTemplate_returns_valid_template_for_known_template_names(string name)
        {
            Assert.NotNull(Templates.GetTemplate(name));
        }
    }
}
