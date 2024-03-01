using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celeste.Mod.Entities;

namespace Celeste.Mod.HoliaGiftMapPackHelper.Entities;
[CustomEntity("HoliaRhythmMapHelper/CustomSpriteFlagSwitch"), Tracked]
public class CustomSpriteFlagSwitch : Entity
{
	private const float Cooldown = 1f;

	private float cooldownTimer;

	private bool onlyFire;

	private bool onlyIce;

	private bool playSounds;

	private Sprite sprite;

	private string flag;

	private bool lastf;

	private bool nowf;

	private bool Usable
	{
		get
		{
			if (!onlyFire || !nowf)
			{
				if (onlyIce)
				{
					return nowf;
				}
				return true;
			}
			return false;
		}
	}

	public CustomSpriteFlagSwitch(Vector2 position, bool onlyFire, bool onlyIce)
		: base(position)
	{
		this.onlyFire = onlyFire;
		this.onlyIce = onlyIce;
		base.Collider = new Hitbox(16f, 24f, -8f, -12f);
		Add(new PlayerCollider(OnPlayer));

		Add(sprite = GFX.SpriteBank.Create("CustomSpriteFlagSwitch"));
		base.Depth = 2000;

	}

	public CustomSpriteFlagSwitch(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Bool("setTrueOnly"), data.Bool("setFalseOnly"))
	{
		flag = data.Attr("flag");
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		nowf = SceneAs<Level>().Session.GetFlag(flag);
		SetSprite(animate: false);
	}

	private void OnChangeMode(bool newflag)
	{
		SetSprite(animate: true);
	}

	private void SetSprite(bool animate)
	{
		if (animate)
		{
			if (playSounds)
			{
				Audio.Play(nowf ? "event:/game/09_core/switch_to_hot" : "event:/game/09_core/switch_to_cold", Position);
			}
			if (Usable)
			{
				sprite.Play(nowf ? "hot" : "ice");
			}
			else
			{
				if (playSounds)
				{
					Audio.Play("event:/game/09_core/switch_dies", Position);
				}
				sprite.Play(!nowf ? "iceOff" : "hotOff");
			}
		}
		else if (Usable)
		{
			sprite.Play(!nowf ? "iceLoop" : "hotLoop");
		}
		else
		{
			sprite.Play(!nowf ? "iceOffLoop" : "hotOffLoop");
		}
		playSounds = false;
	}

	private void OnPlayer(Player player)
	{
		if (Usable && cooldownTimer <= 0f)
		{
			playSounds = true;
			Level level = SceneAs<Level>();
			if (level.Session.GetFlag(flag) == false)
			{
				level.Session.SetFlag(flag, true);
			}
			else
			{
				level.Session.SetFlag(flag, false);
			}
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
			level.Flash(Color.White * 0.15f, drawPlayerOver: true);
			Celeste.Freeze(0.05f);
			cooldownTimer = 1f;
		}
	}

	public override void Update()
	{
		base.Update();
		nowf = base.SceneAs<Level>().Session.GetFlag(flag);
		//Logger.Log(flag, nowf ? "True" : "False");
		if (nowf != lastf)
        {
			OnChangeMode(!lastf);
			cooldownTimer = 1f;
		}
		if (cooldownTimer > 0f)
		{
			cooldownTimer -= Engine.DeltaTime;
		}
		lastf = nowf;
	}
}
