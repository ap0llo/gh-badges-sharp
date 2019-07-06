// The code in this file was ported to C# from the "anafanafo" library
// Licensed under the MIT License
// https://github.com/metabolize/anafanafo
// 
// Original license:
// -----------------------------------------------------------------------------------
// MIT License
// 
// Copyright (c) 2018 Metabolize LLC
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
// https://github.com/metabolize/anafanafo/blob/989fa7f0dc709b726d78f3ea0ecfe7c6546b1aa6/packages/anafanafo/test.js
//
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GhBadgesSharp.Test
{
    public class TextWidthHelperTest
    {
        [Theory]
        [InlineData("m", 106.99)]
        [InlineData("v1.2.511", 494.77)]
        public void BasicTest_1(string text, double expectedWidth)
        {
            var actualWidth = TextWidthHelper.GetWidth(text);
            Assert.InRange(actualWidth, expectedWidth - 0.1, expectedWidth + 0.1);
        }

        [Theory]
        [InlineData(588, 73.37)]
        [InlineData(1013, 44.53)]
        [InlineData(1014, 44.53)]
        public void BasicTest_2(int character, double expectedWidth)
        {
            var actualWidth = TextWidthHelper.GetWidth($"{(char)character}");
            Assert.Equal(actualWidth, expectedWidth);
        }
    }
}
