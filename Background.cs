using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;
using System.Collections.Generic;
namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate()
        {
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 480.0f / bitmap.Height);
            bg.Fade(StartTime - 500, StartTime, 0, Opacity);
            var times = new List<int>();
            times.Add(78302);
            times.Add(103418);
            times.Add(122953);
            times.Add(134116);
            times.Add(145279);
            times.Add(156442);
            times.Add(167604);
            times.Add(178767);
            times.Add(189930);
            times.Add(245744);
            times.Add(270860);
            times.Add(290395);
            times.Add(301558);
            times.Add(312721);
            times.Add(323883);
            times.Add(329814);
            times.Add(335046);
            times.Add(357372);
            foreach(var time in times){
                FlashBang(time, bg);
            }
            bg.Fade(357372, 357372, 1,0);
        }
        public void FlashBang(int StartTime, OsbSprite bg){
            var flash = GetLayer("white").CreateSprite("sb/white.png", OsbOrigin.Centre);
            bg.Scale(StartTime, 480.0f / GetMapsetBitmap("sb/white.png").Height);
            flash.Color(StartTime, 0, 255, 242);
            flash.Fade(StartTime, StartTime, 0, 1);
            flash.Fade(StartTime+1, StartTime + 500, 1, 0);
            bg.Rotate(StartTime, StartTime, 0, 0);
            bg.Rotate(OsbEasing.Out, StartTime, StartTime + 2000, 0.523598775, 0);
            bg.Scale(StartTime, StartTime + 2000, 0.5, 480.0f / GetMapsetBitmap(BackgroundPath).Height);
        }
    }
}
