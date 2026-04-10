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
		// ((ModNPC)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 130;
		((ModNPC)this).NPC.height = 130;
		((ModNPC)this).NPC.damage = 90;
		((ModNPC)this).NPC.defense = 120;
		((ModNPC)this).NPC.lifeMax = 385000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit52?.WithVolume(5f);
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.behindTiles = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.dontCountMe = true;
		((ModNPC)this).NPC.npcSlots = 1f;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
		players = 1;
		for (int j = 0; j < 206; j++)
		{
			((ModNPC)this).NPC.buffImmune[j] = true;
		}
		for (int k = 0; k < 206; k++)
		{
			((ModNPC)this).NPC.buffImmune[k] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 450000 + numPlayers * 45000;
		((ModNPC)this).NPC.damage = 155;
		((ModNPC)this).NPC.defense = 90;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).NPC.rotation;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture = ((ModNPC)this).Mod.GetTexture("NPCs/ShadowWorm/Glow/ErebusBodyGlow");
		Rectangle rectangle = new Rectangle(0, texture.Height * ((ModNPC)this).NPC.frame.Y, texture.Width, texture.Height);
		Vector2 origin = rectangle.Size() * 0.5f;
		spriteBatch.Draw(texture, ((ModNPC)this).NPC.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY), rectangle, new Color(255, 255, 255, 0), ((ModNPC)this).NPC.rotation, origin, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture2D = TextureAssets.Npc[((ModNPC)this).NPC.type].Value;
		Vector2 origin = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
		Main.spriteBatch.Draw(texture2D, ((ModNPC)this).NPC.Center - Main.screenPosition, null, drawColor, ((ModNPC)this).NPC.rotation, origin, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
		return false;
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
		if (projectile.type == 634 || projectile.type == 617 || projectile.type == 620 || projectile.type == 632 || projectile.type == 631 || projectile.type == 639 || projectile.type == 616 || projectile.type == 502 || projectile.type == 503 || projectile.type == 636)
		{
			damage /= 4;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (!((ModNPC)this).NPC.immortal)
		{
			damageDealt += (int)damage;
		}
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ErebusBodyGore1"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ErebusBodyGore2"));
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
			((ModNPC)this).NPC.defense = 100000;
		}
		else
		{
			((ModNPC)this).NPC.defense = 90;
		}
		Player player = Main.player[((ModNPC)this).NPC.target];
		int num = (Main.expertMode ? 48 : 65);
		if (NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("RestlessSoul").Type) || ErebusHead.TeleportVortex)
		{
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
		}
		else
		{
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
		}
		if (ErebusHead.TeleportVortex)
		{
			if (ExpandingVortex.Timer < 500)
			{
				((ModNPC)this).NPC.scale = 0.0001f;
			}
			else
			{
				((ModNPC)this).NPC.scale = 1f;
			}
		}
		else
		{
			((ModNPC)this).NPC.scale = 1f;
		}
		if (ErebusHead.Timer1 == 570)
		{
			((ModNPC)this).NPC.TargetClosest();
			if (Main.rand.Next(6) == 0)
			{
				SoundEngine.PlaySound(SoundID.Item78, ((ModNPC)this).NPC.Center);
				float num2 = 20f;
				float num3 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Vector2 spinninpoint = new Vector2((float)(Math.Cos(num3) * (double)num2 * -1.0), (float)(Math.Sin(num3) * (double)num2 * -1.0));
				spinninpoint = spinninpoint.RotatedByRandom(MathHelper.ToRadians(30f));
				Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center, spinninpoint, ((ModNPC)this).Mod.Find<ModProjectile>("ErebusTentacle").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
			}
		}
		if (ErebusHead.CircleTimer < 580 && ErebusHead.Circling)
		{
			Vector2 center = Main.npc[(int)((ModNPC)this).NPC.ai[3]].Center;
			center += Vector2.Normalize(Main.npc[(int)((ModNPC)this).NPC.ai[3]].velocity.RotatedBy(Math.PI / 2.0)) * 900f;
			if (((ModNPC)this).NPC.Distance(center) < 900f)
			{
				((ModNPC)this).NPC.Center = center + ((ModNPC)this).NPC.DirectionFrom(center) * 900f;
			}
		}
		if (Main.netMode != 1 && !((Entity)Main.npc[(int)((ModNPC)this).NPC.ai[1]]).active)
		{
			((ModNPC)this).NPC.life = 0;
			((ModNPC)this).NPC.HitEffect();
			((Entity)((ModNPC)this).NPC).active = false;
		}
		if ((double)((ModNPC)this).NPC.ai[1] < (double)Main.npc.Length)
		{
			Vector2 vector = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
			float num4 = Main.npc[(int)((ModNPC)this).NPC.ai[1]].position.X + (float)(Main.npc[(int)((ModNPC)this).NPC.ai[1]].width / 2) - vector.X;
			float num5 = Main.npc[(int)((ModNPC)this).NPC.ai[1]].position.Y + (float)(Main.npc[(int)((ModNPC)this).NPC.ai[1]].height / 2) - vector.Y;
			((ModNPC)this).NPC.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
			float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
			float num7 = (num6 - (float)((ModNPC)this).NPC.width) / num6;
			float num8 = num4 * num7;
			float num9 = num5 * num7;
			((ModNPC)this).NPC.velocity = Vector2.Zero;
			((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + num8;
			((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + num9;
		}
		if (NPC.CountNPCS(((ModNPC)this).Mod.Find<ModNPC>("ErebusHead").Type) == 0)
		{
			((Entity)((ModNPC)this).NPC).active = false;
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
