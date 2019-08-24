// The code in this file was ported to C# from the "is-css-color" library
// Licensed under the MIT License
// https://github.com/princejwesley/is-css-color
// 
// Original license:
// -----------------------------------------------------------------------------------
// The MIT License(MIT)
//   
// Copyright(c) 2015 Prince John Wesley(princejohnwesley @gmail.com)
//   
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//   
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// -----------------------------------------------------------------------------------
// Link to original source code:
// https://github.com/princejwesley/is-css-color/blob/f0614f76718866f4e93ee1443af4efe449200475/test.js
//
using GhBadgesSharp.Internal;
using Xunit;

namespace GhBadgesSharp.Test.Internal
{
    public class CssColorHelperTest
    {
        [Theory]
        // Invalid values
        [InlineData("", false)]
        [InlineData(null, false)]
        // Hex code values
        [InlineData("fff", false)]
        [InlineData("#fff", true)]
        [InlineData("#fffe", false)]
        [InlineData("#FFFFFF", true)]
        [InlineData("#ABCFGH", false)]
        [InlineData("#123456", true)]
        // rgb & rgba values
        [InlineData(" rgb(122, 200, 222, 1)", false)]
        [InlineData(" rgb(122, 200, 222)", true)]
        [InlineData(" rgb(100%, 200, 222)", false)]
        [InlineData(" rgb(100%, 200%, 222%)", true)]
        [InlineData("rgb(-100, 20, 111)", true)]
        [InlineData("rgba(-100, 20, 111)", false)]
        [InlineData("rgba(-100, 20, 111, .)", false)]
        [InlineData("rgba(-100, 20, 111, .1)", true)]
        [InlineData("rgba(-100, 20, 111, 1.1)", true)]
        [InlineData("rgba(-100, 20, 111, 1.)", false)]
        [InlineData("rgba(-100, 20, 111, -22.4)", true)]
        // hsl & hsla values
        [InlineData(" hsl(122, 200, 222, 1)", false)]
        [InlineData(" hsl(122, 200, 222)", false)]
        [InlineData(" hsl(122, 200, 222%)", false)]
        [InlineData(" hsl(122, 200%, 222%)", true)]
        [InlineData("hsla(122, 200%, 222%, 1)", true)]
        public void IsCssColor_returns_expected_result(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, CssColor.IsCssColor(value));
        }
    }
}
