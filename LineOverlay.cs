using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
namespace StorybrewScripts
{
    public class LineOverlay : StoryboardObjectGenerator
    {

        [Configurable]
        public int BeatDivisor = 1;

        [Configurable]
        public int FadeDuration = 200;

        [Configurable]
        public string SpritePath = "sb/bar.png";

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
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var hSprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                hSprite.ScaleVec(OsbEasing.InBounce,hitobject.StartTime-200,hitobject.StartTime+FadeDuration, new OpenTK.Vector2((int)SpriteScale, 600000000), new OpenTK.Vector2((int)SpriteScale, 600000000));
                hSprite.Fade(OsbEasing.InBounce, hitobject.StartTime-100, hitobject.StartTime+FadeDuration, 0, 1);
                hSprite.ScaleVec(OsbEasing.OutBounce, hitobject.EndTime, hitobject.EndTime + FadeDuration, new OpenTK.Vector2((int)SpriteScale, 600000000), new OpenTK.Vector2(0, 600000000));
                hSprite.Fade(OsbEasing.OutBounce, hitobject.EndTime, hitobject.EndTime + FadeDuration, 1, 0);
                hSprite.Additive(hitobject.StartTime, hitobject.EndTime + FadeDuration);
                hSprite.Color(hitobject.StartTime, hitobject.Color);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 1024;
                    var startTime = hitobject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hSprite.PositionAt(startTime);
                        hSprite.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));

                        if (complete) break;
                        startTime += timestep;
                    }
                }
            }
        }
    }
}