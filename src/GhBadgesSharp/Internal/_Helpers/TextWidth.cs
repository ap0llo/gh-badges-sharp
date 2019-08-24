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
// https://github.com/metabolize/anafanafo/blob/989fa7f0dc709b726d78f3ea0ecfe7c6546b1aa6/packages/anafanafo/index.js
// https://github.com/metabolize/anafanafo/blob/989fa7f0dc709b726d78f3ea0ecfe7c6546b1aa6/packages/char-width-table-consumer/src/consumer.js
//
using System;
using System.Collections;
using Newtonsoft.Json;

namespace GhBadgesSharp.Internal
{
    internal static class TextWidth
    {
        private const string s_ResourceName = "GhBadgesSharp.Resources.widths.json";

        private static readonly object[][] s_Widths;
        private static readonly double s_DefaultWidth;

        static TextWidth()
        {            
            s_Widths = JsonConvert.DeserializeObject<object[][]>(EmbeddedResource.Load(s_ResourceName));
            s_DefaultWidth = Get("m");
        }

        private class BinarySearchComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var entry = (object[])x;
                var c = Convert.ToInt32(y);

                var start = Convert.ToInt32(entry[0]);
                var end = Convert.ToInt32(entry[1]);

                if (c >= start && c <= end)
                {
                    return 0;
                }
                else if (c < start)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static double Get(string text, bool guess = true)
        {
            var width = 0.0;
            foreach (var c in text)
            {
                var charWidth = GetCharWidth(c);

                if (!charWidth.HasValue)
                {
                    if (guess)
                    {
                        width += s_DefaultWidth;
                    }
                    else
                    {
                        throw new Exception($"No width available for character code {(int)c}");
                    }
                }
                else
                {
                    width += charWidth.Value;
                }
            }

            return width;
        }

        private static double? GetCharWidth(char c)
        {
            if (Char.IsControl(c))
                return 0.0;

            var index = Array.BinarySearch(s_Widths, c, new BinarySearchComparer());

            return index >= 0 ? (double)s_Widths[index][2] : default;
        }
    }
}

