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
		// ((ModNPC)this).DisplayName.SetDefault("Darkmatter Mass");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 32;
		((ModNPC)this).NPC.damage = 80;
		((ModNPC)this).NPC.defense = 65;
		((ModNPC)this).NPC.lifeMax = 2200;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit13;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath19;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.aiStyle = 0;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.buffImmune[24] = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("ShadeMassBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 3700;
		((ModNPC)this).NPC.damage = 110;
		((ModNPC)this).NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore2"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.05f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		_ = Main.expertMode;
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		if (((ModNPC)this).NPC.localAI[0] == 0f)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<DarkmatterTentacle>(), ((ModNPC)this).NPC.damage, 0f, Main.myPlayer, 0f, 0f);
				if (num == 1000)
				{
					((Entity)((ModNPC)this).NPC).active = false;
					return;
				}
				DarkmatterTentacle darkmatterTentacle = Main.projectile[num].ModProjectile as DarkmatterTentacle;
				darkmatterTentacle.ShadeMass = ((ModNPC)this).NPC.whoAmI;
				darkmatterTentacle.width = 16f;
				darkmatterTentacle.length = 80f;
				darkmatterTentacle.minAngle = ((float)i - 0.5f) * (float)Math.PI / 1.5f;
				darkmatterTentacle.maxAngle = ((float)i + 0.5f) * (float)Math.PI / 1.5f;
				Main.projectile[num].rotation = (darkmatterTentacle.minAngle + darkmatterTentacle.maxAngle) / 2f;
				Main.projectile[num].netUpdate = true;
			}
			((ModNPC)this).NPC.localAI[0] = 1f;
		}
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && MoveSpeed >= -45)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && MoveSpeed <= 45)
			{
				MoveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.07f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -45)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 45)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 10.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(1, 1), false, 0, false, false);
		}
	}
}
