// The code in this file was ported to C# from the "is-css-color" library
// Licensed under the MIT License
// https://github.com/princejwesley/is-css-color
// 
// Original license:
// -----------------------------------------------------------------------------------
// The MIT License(MIT)
//   
// Copyright(c) 2015 Prince John Wesley (princejohnwesley@gmail.com)
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
// https://github.com/princejwesley/is-css-color/blob/f0614f76718866f4e93ee1443af4efe449200475/index.js
//
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GhBadgesSharp.Internal
{
    internal static class CssColor
    {
        private static readonly Regex s_HexPattern = new Regex("^#(?:[a-f0-9]{3})?(?:[a-f0-9]{3})$");

        private static readonly HashSet<string> s_CssColorNames = new HashSet<string>()
        {
            "aliceblue",
            "antiquewhite",
            "aqua",
            "aquamarine",
            "azure",
            "beige",
            "bisque",
            "black",
            "blanchedalmond",
            "blue",
            "blueviolet",
            "brown",
            "burlywood",
            "cadetblue",
            "chartreuse",
            "chocolate",
            "coral",
            "cornflowerblue",
            "cornsilk",
            "crimson",
            "currentColor",
            "cyan",
            "darkblue",
            "darkcyan",
            "darkgoldenrod",
            "darkgray",
            "darkgreen",
            "darkgrey",
            "darkkhaki",
            "darkmagenta",
            "darkolivegreen",
            "darkorange",
            "darkorchid",
            "darkred",
            "darksalmon",
            "darkseagreen",
            "darkslateblue",
            "darkslategray",
            "darkslategrey",
            "darkturquoise",
            "darkviolet",
            "deeppink",
            "deepskyblue",
            "dimgray",
            "dimgrey",
            "dodgerblue",
            "firebrick",
            "floralwhite",
            "forestgreen",
            "fuchsia",
            "gainsboro",
            "ghostwhite",
            "gold",
            "goldenrod",
            "gray",
            "green",
            "greenyellow",
            "grey",
            "honeydew",
            "hotpink",
            "indianred",
            "indigo",
            "inherit",
            "initial",
            "ivory",
            "khaki",
            "lavender",
            "lavenderblush",
            "lawngreen",
            "lemonchiffon",
            "lightblue",
            "lightcoral",
            "lightcyan",
            "lightgoldenrodyellow",
            "lightgray",
            "lightgreen",
            "lightgrey",
            "lightpink",
            "lightsalmon",
            "lightseagreen",
            "lightskyblue",
            "lightslategray",
            "lightslategrey",
            "lightsteelblue",
            "lightyellow",
            "lime",
            "limegreen",
            "linen",
            "magenta",
            "maroon",
            "mediumaquamarine",
            "mediumblue",
            "mediumorchid",
            "mediumpurple",
            "mediumseagreen",
            "mediumslateblue",
            "mediumspringgreen",
            "mediumturquoise",
            "mediumvioletred",
            "midnightblue",
            "mintcream",
            "mistyrose",
            "moccasin",
            "navajowhite",
            "navy",
            "oldlace",
            "olive",
            "olivedrab",
            "orange",
            "orangered",
            "orchid",
            "palegoldenrod",
            "palegreen",
            "paleturquoise",
            "palevioletred",
            "papayawhip",
            "peachpuff",
            "peru",
            "pink",
            "plum",
            "powderblue",
            "purple",
            "rebeccapurple",
            "red",
            "rosybrown",
            "royalblue",
            "saddlebrown",
            "salmon",
            "sandybrown",
            "seagreen",
            "seashell",
            "sienna",
            "silver",
            "skyblue",
            "slateblue",
            "slategray",
            "slategrey",
            "snow",
            "springgreen",
            "steelblue",
            "tan",
            "teal",
            "thistle",
            "tomato",
            "transparent",
            "turquoise",
            "violet",
            "wheat",
            "white",
            "whitesmoke",
            "yellow",
            "yellowgreen",
        };

        private const string s_RbgHslPrefix = @"^(rgb|hsl)(a?)\s*\(";
        private const string s_RbgHslValue = @"\s*([-+]?\d+%?)\s*";
        private const string s_RbgHslAlpha = @"(?:,\s*([-+]?(?:(?:\d+(?:\.\d+)?)|(?:\.\d+))\s*))?";
        private const string s_RbgHslSuffix = @"\)$";
        private static readonly Regex s_RgbHslPattern = new Regex(s_RbgHslPrefix + s_RbgHslValue + ',' + s_RbgHslValue + ',' + s_RbgHslValue + s_RbgHslAlpha + s_RbgHslSuffix);

        [Flags]
        private enum ColorType
        {
            Num = 0x01,
            Percentage = 0x01 << 1,
            Error = Num & Percentage
        }


        public static bool IsCssColor(string color)
        {
            if (String.IsNullOrEmpty(color))
                return false;

            color = color.Trim().ToLower();

            if (s_CssColorNames.Contains(color) || s_HexPattern.IsMatch(color))
            {
                return true;
            }

            var match = s_RgbHslPattern.Match(color);
            if (match.Success && match.Groups.Count == 7)
            {
                var flavor = match.Groups[1].Value;
                var alpha = match.Groups[2].Value;
                var rh = match.Groups[3].Value;
                var gs = match.Groups[4].Value;
                var bl = match.Groups[5].Value;
                var a = match.Groups[6].Value;

                // alpha test
                if ((alpha == "a" && String.IsNullOrEmpty(a)) || (!String.IsNullOrEmpty(a) && alpha == ""))
                {
                    return false;
                }

                // hsl
                if (flavor == "hsl")
                {
                    if (GetColorType(rh) != ColorType.Num)
                    {
                        return false;
                    }

                    return (GetColorType(gs) & GetColorType(bl)) == ColorType.Percentage;
                }

                // rgb                
                return (GetColorType(rh) & GetColorType(gs) & GetColorType(bl)) != ColorType.Error;
            }

            return false;
        }

        private static ColorType GetColorType(string token) =>
            token.IndexOf('%') >= 0 ? ColorType.Percentage : ColorType.Num;

    }
}
