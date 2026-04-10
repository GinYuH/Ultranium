using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowWorm.Projectiles;

namespace Ultranium.NPCs.ShadowWorm;

[AutoloadBossHead]
public class ErebusTail : ModNPC
{
	public int players;

	private int dpsCap = 250;

	private int damageDealt;

	private int dpsTime;

	private int noDamageTime;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 100;
		((ModNPC)this).npc.height = 100;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 75;
		((ModNPC)this).npc.lifeMax = 385000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit52?.WithVolume(5f);
		((ModNPC)this).npc.behindTiles = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.dontCountMe = true;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 450000 + numPlayers * 45000;
		((ModNPC)this).npc.damage = 155;
		((ModNPC)this).npc.defense = 90;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).npc.rotation;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		Texture2D texture = ((ModNPC)this).mod.GetTexture("NPCs/ShadowWorm/Glow/ErebusTailGlow");
		Rectangle rectangle = new Rectangle(0, texture.Height * ((ModNPC)this).npc.frame.Y, texture.Width, texture.Height);
		Vector2 origin = rectangle.Size() * 0.5f;
		SpriteEffects effects = SpriteEffects.None;
		spriteBatch.Draw(texture, ((ModNPC)this).npc.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).npc.gfxOffY), rectangle, new Color(255, 255, 255, 0), ((ModNPC)this).npc.rotation, origin, ((ModNPC)this).npc.scale, effects, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		Texture2D texture2D = Main.npcTexture[((ModNPC)this).npc.type];
		Vector2 origin = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
		Main.spriteBatch.Draw(texture2D, ((ModNPC)this).npc.Center - Main.screenPosition, null, drawColor, ((ModNPC)this).npc.rotation, origin, ((ModNPC)this).npc.scale, SpriteEffects.None, 0f);
		return false;
	}

	public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
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

	public override void HitEffect(int hitDirection, double damage)
	{
		if (!((ModNPC)this).npc.immortal)
		{
			damageDealt += (int)damage;
		}
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ErebusTailGore"));
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
			((ModNPC)this).npc.defense = 100000;
		}
		else
		{
			((ModNPC)this).npc.defense = 90;
		}
		_ = Main.player[((ModNPC)this).npc.target];
		if (NPC.AnyNPCs(((ModNPC)this).mod.NPCType("RestlessSoul")) || ErebusHead.TeleportVortex)
		{
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
		}
		else
		{
			((ModNPC)this).npc.immortal = false;
			((ModNPC)this).npc.dontTakeDamage = false;
		}
		if (ErebusHead.TeleportVortex)
		{
			if (ExpandingVortex.Timer < 500)
			{
				((ModNPC)this).npc.scale = 0.0001f;
			}
			else
			{
				((ModNPC)this).npc.scale = 1f;
			}
		}
		else
		{
			((ModNPC)this).npc.scale = 1f;
		}
		if (ErebusHead.CircleTimer < 580 && ErebusHead.Circling)
		{
			Vector2 center = Main.npc[(int)((ModNPC)this).npc.ai[3]].Center;
			center += Vector2.Normalize(Main.npc[(int)((ModNPC)this).npc.ai[3]].velocity.RotatedBy(Math.PI / 2.0)) * 900f;
			if (((ModNPC)this).npc.Distance(center) < 900f)
			{
				((ModNPC)this).npc.Center = center + ((ModNPC)this).npc.DirectionFrom(center) * 900f;
			}
		}
		if ((double)((ModNPC)this).npc.ai[1] < (double)Main.npc.Length)
		{
			Vector2 vector = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
			float num = Main.npc[(int)((ModNPC)this).npc.ai[1]].position.X + (float)(Main.npc[(int)((ModNPC)this).npc.ai[1]].width / 2) - vector.X;
			float num2 = Main.npc[(int)((ModNPC)this).npc.ai[1]].position.Y + (float)(Main.npc[(int)((ModNPC)this).npc.ai[1]].height / 2) - vector.Y;
			((ModNPC)this).npc.rotation = (float)Math.Atan2(num2, num) + 1.57f;
			float num3 = (float)Math.Sqrt(num * num + num2 * num2);
			float num4 = (num3 - (float)((ModNPC)this).npc.width) / num3;
			float num5 = num * num4;
			float num6 = num2 * num4;
			((ModNPC)this).npc.velocity = Vector2.Zero;
			((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + num5;
			((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + num6;
		}
		if (NPC.CountNPCS(((ModNPC)this).mod.NPCType("ErebusHead")) == 0)
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreNPCLoot()
	{
		return false;
	}
}
