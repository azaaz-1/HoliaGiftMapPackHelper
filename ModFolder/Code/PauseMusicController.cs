using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celeste.Mod.Entities;

namespace Celeste.Mod.HoliaGiftMapPackHelper.Entities;
[CustomEntity("HoliaRhythmMapHelper/PauseMusicController"), Tracked]

public class PauseMusicController : Entity
{
    public PauseMusicController(EntityData e, Vector2 offset)
        :this(e.Position + offset)
    { 

    }

    public PauseMusicController(Vector2 position)
        :base(position)
    {
        base.Tag = Tags.PauseUpdate;
    }



    public override void Update()
    {
        base.Update();
        if (SceneAs<Level>()!= null && Audio.CurrentMusicEventInstance != null)
        {
            if (SceneAs<Level>().Paused)
            {
                Audio.CurrentMusicEventInstance.setPaused(true);
                //Logger.Log("music", "pause");
            }
            else
            {
                Audio.CurrentMusicEventInstance.setPaused(false);
                //Logger.Log("music", "resume");
            }
        }


    }
}
