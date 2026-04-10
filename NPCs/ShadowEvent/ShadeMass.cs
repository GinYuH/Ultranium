using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowEvent.Projectiles;

namespace Ultranium.NPCs.ShadowEvent;

public class ShadeMass : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Darkmatter Mass");
	}

	public override void SetDefaults()
	{
		NPC.scale = 1f;
		NPC.width = 30;
		NPC.height = 32;
		NPC.damage = 80;
		NPC.defense = 65;
		NPC.lifeMax = 2200;
		NPC.HitSound = SoundID.NPCHit13;
		NPC.DeathSound = SoundID.NPCDeath19;
		NPC.knockBackResist = 0f;
		NPC.aiStyle = 0;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.buffImmune[24] = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("ShadeMassBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 3700;
		NPC.damage = 110;
		NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore1"));
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore2"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		NPC.rotation = NPC.velocity.X * 0.05f;
		Player player = Main.player[NPC.target];
		_ = Main.expertMode;
		NPC.netUpdate = true;
		NPC.TargetClosest();
		if (NPC.localAI[0] == 0f)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<DarkmatterTentacle>(), NPC.damage, 0f, Main.myPlayer, 0f, 0f);
				if (num == 1000)
				{
					((Entity)NPC).active = false;
					return;
				}
				DarkmatterTentacle darkmatterTentacle = Main.projectile[num].ModProjectile as DarkmatterTentacle;
				darkmatterTentacle.ShadeMass = NPC.whoAmI;
				darkmatterTentacle.width = 16f;
				darkmatterTentacle.length = 80f;
				darkmatterTentacle.minAngle = ((float)i - 0.5f) * (float)Math.PI / 1.5f;
				darkmatterTentacle.maxAngle = ((float)i + 0.5f) * (float)Math.PI / 1.5f;
				Main.projectile[num].rotation = (darkmatterTentacle.minAngle + darkmatterTentacle.maxAngle) / 2f;
				Main.projectile[num].netUpdate = true;
			}
			NPC.localAI[0] = 1f;
		}
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && MoveSpeed >= -45)
			{
				MoveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && MoveSpeed <= 45)
			{
				MoveSpeed++;
			}
			NPC.velocity.X = (float)MoveSpeed * 0.07f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -45)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 45)
			{
				MoveSpeedY++;
			}
			NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 10.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(1, 1), false, 0, false, false);
		}
	}
}
