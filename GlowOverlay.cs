using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Numerics;
using System.Collections.Generic;
namespace StorybrewScripts
{
    public class GlowOverlay : StoryboardObjectGenerator
    {

        [Configurable]
        public int BeatDivisor = 64;

        [Configurable]
        public int FadeDuration = 200;

        [Configurable]
        public string SpritePath = "sb/glow.png";

        [Configurable]
        public double SpriteScale = 1;

        public override void Generate()
        {
            var times = new Dictionary<int,int>();
            times[78302] = 189930;
            times[245744] = 335046;
            foreach(var time in times){
                Generates(time.Key, time.Value);
            }
        }
        public void Generates(int StartTime, int EndTime)
        {
            var blur = GetMapsetBitmap(SpritePath);
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                var the_blur = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre);
                the_blur.Scale(OsbEasing.OutBounce, hitobject.StartTime, hitobject.EndTime + FadeDuration, SpriteScale, SpriteScale * 1);
                the_blur.Fade(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + FadeDuration, 1, 0);
                the_blur.Additive(hitobject.StartTime, hitobject.EndTime + FadeDuration);
                the_blur.Color(hitobject.StartTime, hitobject.Color);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        if (complete) break;
                        startTime += timestep;
                    }
                }
            }
        }
    }
}
