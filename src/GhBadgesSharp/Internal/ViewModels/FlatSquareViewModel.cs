﻿using Fluid;

namespace Grynwald.GhBadgesSharp.Internal.ViewModels
{
    /// <summary>
    /// View model for the "flat-square" template
    /// </summary>
    internal class FlatSquareViewModel : FlatViewModel
    {
        public FlatSquareViewModel(BadgeData badgeData) : base(badgeData)
        { }


        internal override IFluidTemplate GetTemplate() => Templates.GetTemplate("flat-square");
    }
}
