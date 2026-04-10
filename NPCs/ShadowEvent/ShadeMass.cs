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
		((ModNPC)this).DisplayName.SetDefault("Darkmatter Mass");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 32;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 65;
		((ModNPC)this).npc.lifeMax = 2200;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit13;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath19;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.aiStyle = 0;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.buffImmune[24] = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("ShadeMassBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 3700;
		((ModNPC)this).npc.damage = 110;
		((ModNPC)this).npc.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/ShadeMassGore2"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.05f;
		Player player = Main.player[((ModNPC)this).npc.target];
		_ = Main.expertMode;
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		if (((ModNPC)this).npc.localAI[0] == 0f)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ModContent.ProjectileType<DarkmatterTentacle>(), ((ModNPC)this).npc.damage, 0f, Main.myPlayer, 0f, 0f);
				if (num == 1000)
				{
					((Entity)((ModNPC)this).npc).active = false;
					return;
				}
				DarkmatterTentacle darkmatterTentacle = Main.projectile[num].modProjectile as DarkmatterTentacle;
				darkmatterTentacle.ShadeMass = ((ModNPC)this).npc.whoAmI;
				darkmatterTentacle.width = 16f;
				darkmatterTentacle.length = 80f;
				darkmatterTentacle.minAngle = ((float)i - 0.5f) * (float)Math.PI / 1.5f;
				darkmatterTentacle.maxAngle = ((float)i + 0.5f) * (float)Math.PI / 1.5f;
				Main.projectile[num].rotation = (darkmatterTentacle.minAngle + darkmatterTentacle.maxAngle) / 2f;
				Main.projectile[num].netUpdate = true;
			}
			((ModNPC)this).npc.localAI[0] = 1f;
		}
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && MoveSpeed >= -45)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && MoveSpeed <= 45)
			{
				MoveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.07f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -45)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 45)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.07f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 10.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(1, 1), false, 0, false, false);
		}
	}
}
