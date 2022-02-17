
using StorybrewCommon.Scripting;

namespace StorybrewScripts
{
    public class Credit : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var credit = GetLayer("").CreateSprite("sb/credit.png");
            credit.Scale(-4000, 480.0f / GetMapsetBitmap("sb/credit.png").Height);
            credit.Fade(-4000, -3000, 0, 1);
            credit.Fade(-3000, -1000, 1, 0);
        }
    }
}
