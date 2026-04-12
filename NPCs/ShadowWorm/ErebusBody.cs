using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowWorm.Projectiles;

namespace Ultranium.NPCs.ShadowWorm;

[AutoloadBossHead]
public class ErebusBody : ModNPC
{
	public int players;

	private int dpsCap = 120;

	private int damageDealt;

	private int dpsTime;

	private int noDamageTime;

	public float rotate;

	public float SpinX;

	public float SpinY;

	public int Spin;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		NPC.width = 130;
		NPC.height = 130;
		NPC.damage = 90;
		NPC.defense = 120;
		NPC.lifeMax = 385000;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit52;
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/ErebusRoar") with { PitchVariance = 0.5f };
		NPC.behindTiles = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.noGravity = true;
		NPC.dontCountMe = true;
		NPC.npcSlots = 1f;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
		players = 1;
		for (int j = 0; j < 206; j++)
		{
			NPC.buffImmune[j] = true;
		}
		for (int k = 0; k < 206; k++)
		{
			NPC.buffImmune[k] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 450000 + numPlayers * 45000;
		NPC.damage = 155;
		NPC.defense = 90;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = NPC.rotation;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Glow/ErebusBodyGlow").Value;
		Rectangle rectangle = new Rectangle(0, texture.Height * NPC.frame.Y, texture.Width, texture.Height);
		Vector2 origin = rectangle.Size() * 0.5f;
		spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), rectangle, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture2D = TextureAssets.Npc[NPC.type].Value;
		Vector2 origin = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
		Main.spriteBatch.Draw(texture2D, NPC.Center - Main.screenPosition, null, drawColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0f);
		return false;
	}

    public override bool? CanBeHitByItem(Player player, Item item)
    {
		if (noDamageTime > 0)
		{
			return false;
		}
		return null;
    }

    public override bool? CanBeHitByProjectile(Projectile projectile)
    {
        if (noDamageTime > 0)
        {
            return false;
        }
        return null;
    }

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == 634 || projectile.type == 617 || projectile.type == 620 || projectile.type == 632 || projectile.type == 631 || projectile.type == 639 || projectile.type == 616 || projectile.type == 502 || projectile.type == 503 || projectile.type == 636)
		{
			modifiers.SourceDamage /= 4;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (!NPC.immortal)
		{
			damageDealt += (int)hit.Damage;
		}
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ErebusBodyGore1").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ErebusBodyGore2").Type);
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override bool PreAI()
	{
		dpsTime++;
		if (noDamageTime >= 1)
		{
			noDamageTime--;
		}
		if (dpsTime >= 60)
		{
			dpsTime = 0;
			damageDealt = 0;
		}
		if (damageDealt >= dpsCap)
		{
			dpsTime = 0;
			damageDealt = 0;
			noDamageTime = 60;
		}
		if (noDamageTime != 0)
		{
			NPC.defense = 100000;
		}
		else
		{
			NPC.defense = 90;
		}
		Player player = Main.player[NPC.target];
		int num = (Main.expertMode ? 48 : 65);
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("RestlessSoul").Type) || ErebusHead.TeleportVortex)
		{
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
		}
		else
		{
			NPC.immortal = false;
			NPC.dontTakeDamage = false;
		}
		if (ErebusHead.TeleportVortex)
		{
			if (ExpandingVortex.Timer < 500)
			{
				NPC.scale = 0.0001f;
			}
			else
			{
				NPC.scale = 1f;
			}
		}
		else
		{
			NPC.scale = 1f;
		}
		if (ErebusHead.Timer1 == 570)
		{
			NPC.TargetClosest();
			if (Main.rand.Next(6) == 0)
			{
				SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
				float num2 = 20f;
				float num3 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Vector2 spinninpoint = new Vector2((float)(Math.Cos(num3) * (double)num2 * -1.0), (float)(Math.Sin(num3) * (double)num2 * -1.0));
				spinninpoint = spinninpoint.RotatedByRandom(MathHelper.ToRadians(30f));
				Main.projectile[Projectile.NewProjectile(null, NPC.Center, spinninpoint, Mod.Find<ModProjectile>("ErebusTentacle").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
			}
		}
		if (ErebusHead.CircleTimer < 580 && ErebusHead.Circling)
		{
			Vector2 center = Main.npc[(int)NPC.ai[3]].Center;
			center += Vector2.Normalize(Main.npc[(int)NPC.ai[3]].velocity.RotatedBy(Math.PI / 2.0)) * 900f;
			if (NPC.Distance(center) < 900f)
			{
				NPC.Center = center + NPC.DirectionFrom(center) * 900f;
			}
		}
		if (Main.netMode != 1 && !((Entity)Main.npc[(int)NPC.ai[1]]).active)
		{
			NPC.life = 0;
			NPC.HitEffect();
			((Entity)NPC).active = false;
		}
		if ((double)NPC.ai[1] < (double)Main.npc.Length)
		{
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num4 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector.X;
			float num5 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector.Y;
			NPC.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
			float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
			float num7 = (num6 - (float)NPC.width) / num6;
			float num8 = num4 * num7;
			float num9 = num5 * num7;
			NPC.velocity = Vector2.Zero;
			NPC.position.X = NPC.position.X + num8;
			NPC.position.Y = NPC.position.Y + num9;
		}
		if (NPC.CountNPCS(Mod.Find<ModNPC>("ErebusHead").Type) == 0)
		{
			((Entity)NPC).active = false;
		}
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreKill()
	{
		return false;
	}
}
