using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
		//DisplayName.SetDefault("Erebus");
	}

	public override void SetDefaults()
	{
		NPC.width = 100;
		NPC.height = 100;
		NPC.damage = 60;
		NPC.defense = 75;
		NPC.lifeMax = 385000;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit52;
		NPC.behindTiles = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.noGravity = true;
		NPC.dontCountMe = true;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
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
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Glow/ErebusTailGlow").Value;
		Rectangle rectangle = new Rectangle(0, texture.Height * NPC.frame.Y, texture.Width, texture.Height);
		Vector2 origin = rectangle.Size() * 0.5f;
		SpriteEffects effects = SpriteEffects.None;
		spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), rectangle, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, effects, 0f);
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
		if (projectile.type == ProjectileID.NebulaBlaze1 || projectile.type == ProjectileID.NebulaArcanum || projectile.type == ProjectileID.NebulaArcanumExplosionShotShard || projectile.type == ProjectileID.LastPrismLaser || projectile.type == ProjectileID.PhantasmArrow || projectile.type == ProjectileID.MoonlordArrow || projectile.type == ProjectileID.VortexBeaterRocket || projectile.type == ProjectileID.Meowmere || projectile.type == ProjectileID.StarWrath || projectile.type == ProjectileID.Daybreak)
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
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ErebusTailGore").Type);
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
		_ = Main.player[NPC.target];
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
		if (ErebusHead.CircleTimer < 580 && ErebusHead.Circling)
		{
			Vector2 center = Main.npc[(int)NPC.ai[3]].Center;
			center += Vector2.Normalize(Main.npc[(int)NPC.ai[3]].velocity.RotatedBy(Math.PI / 2.0)) * 900f;
			if (NPC.Distance(center) < 900f)
			{
				NPC.Center = center + NPC.DirectionFrom(center) * 900f;
			}
		}
		if ((double)NPC.ai[1] < (double)Main.npc.Length)
		{
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector.X;
			float num2 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector.Y;
			NPC.rotation = (float)Math.Atan2(num2, num) + 1.57f;
			float num3 = (float)Math.Sqrt(num * num + num2 * num2);
			float num4 = (num3 - (float)NPC.width) / num3;
			float num5 = num * num4;
			float num6 = num2 * num4;
			NPC.velocity = Vector2.Zero;
			NPC.position.X = NPC.position.X + num5;
			NPC.position.Y = NPC.position.Y + num6;
		}
		if (NPC.CountNPCS(Mod.Find<ModNPC>("ErebusHead").Type) == 0)
		{
			NPC.active = false;
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
