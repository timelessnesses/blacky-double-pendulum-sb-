
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Vig : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            int EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);
		    var vig = GetLayer("Vig").CreateSprite("sb/vig.png", OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap("sb/vig.png");
            vig.Fade(-500, 357372, 1, 1);
            vig.Scale(430379, 480.0f / bitmap.Height);
            vig.Fade(357372, 357372, 1,0);
    }
}}
