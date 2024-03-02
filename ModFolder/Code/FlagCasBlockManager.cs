using FMOD.Studio;
using Monocle;
using Celeste.Mod.Entities;

namespace Celeste.Mod.HoliaGiftMapPackHelper.Entities;
[CustomEntity("HoliaGiftMapPackHelper/FlagCasBlockManager"), Tracked]
public class CassetteBlockManager : Entity
{
	private int currentIndex;

	private float beatTimer;

	private int beatIndex;

	private float tempoMult;

	private int leadBeats;

	private int maxBeat;

	private bool isLevelMusic;

	private int beatIndexOffset;

	private EventInstance sfx;

	private EventInstance snapshot;

	public CassetteBlockManager()
	{
		//to be implemented...
	}

	public override void Awake(Scene scene)
	{
		//needed?
	}

	public override void Removed(Scene scene)
	{
		//needed?
	}

	private int id = 0;
	private int ide = 0;

	public override void Update()
	{
		base.Update();
		foreach (FlagCasBlock entity in base.Scene.Tracker.GetEntities<FlagCasBlock>())
		{
			entity.Activated = base.SceneAs<Level>().Session.GetFlag(entity.flag);
		}
	}



	public int GetSixteenthNote()
	{
		return (beatIndex + beatIndexOffset) % 256 + 1;
	}



	public void Finish()
	{
		if (!isLevelMusic)
		{
			Audio.Stop(snapshot);
		}
		RemoveSelf();
	}


	public void SetActiveIndex(int index)
	{

	}


}
