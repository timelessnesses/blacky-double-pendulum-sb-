using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System;
namespace StorybrewScripts
{
    public class Particles : StoryboardObjectGenerator
    {
        [Configurable]
        public int particles_count = 69;

        public override void Generate(){
            var times = new Dictionary<int, int>();
            times[122953] = 145279;
            times[290395] = 335046;
            foreach(var time in times){
                Generates(time.Key, time.Value);
            }
        }
        public void Generates(int StartTime, int EndTime){
            var particles = new List<OsbSprite>();
            var layer = GetLayer("");
            for(var i = 0; i < particles_count; i++){
                var sprite = layer.CreateSprite("sb/box_uwu.png",OsbOrigin.Centre);
                sprite.Scale(StartTime, (int) new Random().NextDouble() * 0.5 + 0.5);
                particles.Add(sprite);
            }
            Log("Generated " + particles.Count + " particles");
            foreach(var particle in particles){
                for(var time = StartTime; time < EndTime; time += (21093-20744)){
                    particle.MoveY(OsbEasing.None, time, time + (21093-20744), (int) new Random().NextDouble() * 90, (int) new Random().NextDouble() * 90);
                }
            }
        }
    }
}
