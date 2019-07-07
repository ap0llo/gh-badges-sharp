// The code in this file was ported to C# from the "gh-badges" library 
// Licensed under the Creative Commons Zero v1.0 Universal License
// https://github.com/badges/shields/blob/master/gh-badges/LICENSE
// 
// Original license:
// -----------------------------------------------------------------------------------
// CC0 1.0 Universal
// 
// Statement of Purpose
// 
// The laws of most jurisdictions throughout the world automatically confer
// exclusive Copyright and Related Rights (defined below) upon the creator and
// subsequent owner(s) (each and all, an "owner") of an original work of
// authorship and/or a database (each, a "Work").
// 
// Certain owners wish to permanently relinquish those rights to a Work for the
// purpose of contributing to a commons of creative, cultural and scientific
// works ("Commons") that the public can reliably and without fear of later
// claims of infringement build upon, modify, incorporate in other works, reuse
// and redistribute as freely as possible in any form whatsoever and for any
// purposes, including without limitation commercial purposes. These owners may
// contribute to the Commons to promote the ideal of a free culture and the
// further production of creative, cultural and scientific works, or to gain
// reputation or greater distribution for their Work in part through the use and
// efforts of others.
// 
// For these and/or other purposes and motivations, and without any expectation
// of additional consideration or compensation, the person associating CC0 with a
// Work (the "Affirmer"), to the extent that he or she is an owner of Copyright
// and Related Rights in the Work, voluntarily elects to apply CC0 to the Work
// and publicly distribute the Work under its terms, with knowledge of his or her
// Copyright and Related Rights in the Work and the meaning and intended legal
// effect of CC0 on those rights.
// 
// 1. Copyright and Related Rights. A Work made available under CC0 may be
// protected by copyright and related or neighboring rights ("Copyright and
// Related Rights"). Copyright and Related Rights include, but are not limited
// to, the following:
// 
//   i. the right to reproduce, adapt, distribute, perform, display, communicate,
//   and translate a Work;
// 
//   ii. moral rights retained by the original author(s) and/or performer(s);
// 
//   iii. publicity and privacy rights pertaining to a person's image or likeness
//   depicted in a Work;
// 
//   iv. rights protecting against unfair competition in regards to a Work,
//   subject to the limitations in paragraph 4(a), below;
// 
//   v. rights protecting the extraction, dissemination, use and reuse of data in
//   a Work;
// 
//   vi. database rights (such as those arising under Directive 96/9/EC of the
//   European Parliament and of the Council of 11 March 1996 on the legal
//   protection of databases, and under any national implementation thereof,
//   including any amended or successor version of such directive); and
// 
//   vii. other similar, equivalent or corresponding rights throughout the world
//   based on applicable law or treaty, and any national implementations thereof.
// 
// 2. Waiver. To the greatest extent permitted by, but not in contravention of,
// applicable law, Affirmer hereby overtly, fully, permanently, irrevocably and
// unconditionally waives, abandons, and surrenders all of Affirmer's Copyright
// and Related Rights and associated claims and causes of action, whether now
// known or unknown (including existing as well as future claims and causes of
// action), in the Work (i) in all territories worldwide, (ii) for the maximum
// duration provided by applicable law or treaty (including future time
// extensions), (iii) in any current or future medium and for any number of
// copies, and (iv) for any purpose whatsoever, including without limitation
// commercial, advertising or promotional purposes (the "Waiver"). Affirmer makes
// the Waiver for the benefit of each member of the public at large and to the
// detriment of Affirmer's heirs and successors, fully intending that such Waiver
// shall not be subject to revocation, rescission, cancellation, termination, or
// any other legal or equitable action to disrupt the quiet enjoyment of the Work
// by the public as contemplated by Affirmer's express Statement of Purpose.
// 
// 3. Public License Fallback. Should any part of the Waiver for any reason be
// judged legally invalid or ineffective under applicable law, then the Waiver
// shall be preserved to the maximum extent permitted taking into account
// Affirmer's express Statement of Purpose. In addition, to the extent the Waiver
// is so judged Affirmer hereby grants to each affected person a royalty-free,
// non transferable, non sublicensable, non exclusive, irrevocable and
// unconditional license to exercise Affirmer's Copyright and Related Rights in
// the Work (i) in all territories worldwide, (ii) for the maximum duration
// provided by applicable law or treaty (including future time extensions), (iii)
// in any current or future medium and for any number of copies, and (iv) for any
// purpose whatsoever, including without limitation commercial, advertising or
// promotional purposes (the "License"). The License shall be deemed effective as
// of the date CC0 was applied by Affirmer to the Work. Should any part of the
// License for any reason be judged legally invalid or ineffective under
// applicable law, such partial invalidity or ineffectiveness shall not
// invalidate the remainder of the License, and in such case Affirmer hereby
// affirms that he or she will not (i) exercise any of his or her remaining
// Copyright and Related Rights in the Work or (ii) assert any associated claims
// and causes of action with respect to the Work, in either case contrary to
// Affirmer's express Statement of Purpose.
// 
// 4. Limitations and Disclaimers.
// 
//   a. No trademark or patent rights held by Affirmer are waived, abandoned,
//   surrendered, licensed or otherwise affected by this document.
// 
//   b. Affirmer offers the Work as-is and makes no representations or warranties
//   of any kind concerning the Work, express, implied, statutory or otherwise,
//   including without limitation warranties of title, merchantability, fitness
//   for a particular purpose, non infringement, or the absence of latent or
//   other defects, accuracy, or the present or absence of errors, whether or not
//   discoverable, all to the greatest extent permissible under applicable law.
// 
//   c. Affirmer disclaims responsibility for clearing rights of other persons
//   that may apply to the Work or any use thereof, including without limitation
//   any person's Copyright and Related Rights in the Work. Further, Affirmer
//   disclaims responsibility for obtaining any necessary consents, permissions
//   or other rights required for any use of the Work.
// 
//   d. Affirmer understands and acknowledges that Creative Commons is not a
//   party to this document and has no duty or obligation with respect to this
//   CC0 or use of the Work.
// 
// For more information, please see
// <http://creativecommons.org/publicdomain/zero/1.0/>
//
// -----------------------------------------------------------------------------------
// Link to original source code:
// https://github.com/badges/shields/blob/c6ef885b7508d342963d0600d27282950d1e646b/gh-badges/lib/make-badge.js
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Fluid;

namespace GhBadgesSharp
{
    public static class Badge
    {
        private static readonly Dictionary<string, FluidTemplate> s_Templates = new Dictionary<string, FluidTemplate>();


        static Badge()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var templateResourceNames = assembly
                .GetManifestResourceNames()
                .Where(name => name.StartsWith("GhBadgesSharp.Templates.") && name.EndsWith("-template.liquid"));
            
            foreach (var resourceName in templateResourceNames)
            {
                var templateName = resourceName
                    .Replace("GhBadgesSharp.Templates.", "")
                    .Replace("-template.liquid", "");

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var streamReader = new StreamReader(stream))
                {
                    var templateSource = streamReader.ReadToEnd();
                    var template = FluidTemplate.Parse(templateSource);
                    s_Templates.Add(templateName, template);
                }
            }

        }

        public static XElement MakeBadge(
            string template,
            string leftText,
            string rightText,
            string color = null,
            string labelColor = null,
            string logo = null,
            int? logoPosition = null,
            int? logoWidth = null,
            IEnumerable<string> links = null)
        {
            var badgeData = GetBadgeData(template, leftText, rightText, color, labelColor, logo, logoPosition, logoWidth, links);
            return RenderBadge(badgeData);
        }


        internal static BadgeData GetBadgeData(
                string template,
                string leftText,
                string rightText,
                string color = null,
                string labelColor = null,
                string logo = null,
                int? logoPosition = null,
                int? logoWidth = null,
                IEnumerable<string> links = null)
        {

            links = links ?? Enumerable.Empty<string>();

            leftText = leftText.Trim();
            rightText = rightText.Trim();

            color = Colors.NormalizeColor(color);
            labelColor = Colors.NormalizeColor(labelColor);

            if (!s_Templates.ContainsKey(template))
            {
                template = "flat";
            }

            if (template.StartsWith("popout"))
            {
                if (!String.IsNullOrEmpty(logo))
                {
                    logoPosition = logoPosition <= 10 && logoPosition >= -10 ? logoPosition : 0;
                    logoWidth = logoWidth ?? 32;
                }
                else
                {
                    template = template.Replace("popout", "flat");
                }
            }

            if (template == "social")
            {
                leftText = Capitalize(leftText);
            }
            else if (template == "for-the-badge")
            {
                leftText = leftText.ToUpper();
                leftText = leftText.ToUpper();
            }

            var leftWidth = TextWidthHelper.GetWidth(leftText) / 10;

            // Increase chances of pixel grid alignment.
            if (leftWidth % 2 == 0)
            {
                leftWidth++;
            }

            var rightWidth = TextWidthHelper.GetWidth(rightText) / 10;

            // Increase chances of pixel grid alignment.
            if (rightWidth % 2 == 0)
            {
                rightWidth++;
            }

            logoWidth = logoWidth ?? (logo != null ? 14 : 0);

            int logoPadding;
            if (leftText.Length == 0)
            {
                logoPadding = 0;
            }
            else
            {
                logoPadding = logo != null ? 3 : 0;
            }

            var context = new BadgeData()
            {
                TemplateName = template,
                LeftText = leftText,
                RightText = rightText,
                EscapedLeftText = EscapeXml(leftText),
                EscapedRightText = EscapeXml(rightText),
                Widths = new[] { leftWidth + 10 + logoWidth ?? 0 + logoPadding, rightWidth + 10 },
                Links = links.Select(EscapeXml),
                Logo = EscapeXml(logo),
                LogoPosition = logoPosition,
                LogoWidth = logoWidth,
                LogoPadding = logoPadding,
                ColorA = Colors.ToSvgColor(labelColor),
                ColorB = Colors.ToSvgColor(color),
                // escapeXml
            };

            return context;
        }

        private static string Capitalize(string value)
        {
            if (value == null)
                return null;

            if (value.Length == 0)
                return value;

            if (value.Length == 1)
                return value.ToUpper();

            return value.Substring(0, 1).ToUpper() + value.Substring(1);
        }

        private static string EscapeXml(string value)
        {
            if (value == null)
                return null;

            return new XText(value).ToString();
        }

        private static XElement RenderBadge(BadgeData data)
        {
            var template = s_Templates[data.TemplateName];

            var context = new TemplateContext();
            context.MemberAccessStrategy.Register(data.GetType());
            context.SetValue("it", data);

            var rendered = template.Render(context).Trim();

            var svg = XElement.Parse(rendered);
            return svg;

        }
    }
}
