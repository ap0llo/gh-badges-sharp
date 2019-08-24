using Fluid;

namespace GhBadgesSharp.Internal.ViewModels
{
    /// <summary>
    /// View model for the "flat-square" template
    /// </summary>
    internal class FlatSquareViewModel : FlatViewModel
    {
        public FlatSquareViewModel(BadgeData badgeData) : base(badgeData)
        { }


        internal override FluidTemplate GetTemplate() => Templates.GetTemplate("flat-square");
    }
}
