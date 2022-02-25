using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
namespace StorybrewScripts
{
    public class Square : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var times = new Dictionary<int, int>();
            times[122953] = 145279;
            times[290395] = 335046;
            foreach(var time in times){
                Generates(time.Key, time.Value);
            }
        }
        public void Generates(int StartTime, int EndTime){
            var layer = GetLayer("");
            var sprite = layer.CreateSprite("sb/box_uwu.png",OsbOrigin.Centre);
            sprite = layer.CreateSprite("sb/box_uwu.png",OsbOrigin.Centre); // Spinny box1
            sprite.Scale(StartTime, 1.3);
            sprite.Rotate(OsbEasing.None, StartTime, EndTime, 0, 10);
            sprite.MoveY(OsbEasing.None, StartTime, StartTime, 90, 90);
            sprite = layer.CreateSprite("sb/box_uwu.png",OsbOrigin.Centre); // Spinny box2
            sprite.Scale(StartTime, 1.2);
            sprite.Rotate(OsbEasing.None, StartTime, EndTime, 0, 20);
            sprite.MoveY(OsbEasing.None, StartTime, StartTime, 90, 90);
        }
    }
}
