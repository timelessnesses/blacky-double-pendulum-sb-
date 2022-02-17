
using StorybrewCommon.Scripting;

namespace StorybrewScripts
{
    public class Credit : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var credit = GetLayer("").CreateSprite("sb/credit.png");
            credit.Scale(-2000, 480.0f / GetMapsetBitmap("sb/credit.png").Height);
            credit.Fade(-2000, -1500, 0, 1);
            credit.Fade(-1500, -1000, 1, 0);
        }
    }
}
