using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Phantom : ModNPC
{
	public int ShootTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Phantom");
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[560];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1.2f;
		((ModNPC)this).npc.width = 34;
		((ModNPC)this).npc.height = 34;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 60;
		((ModNPC)this).npc.lifeMax = 2000;
		((ModNPC)this).npc.HitSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomHit");
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomDeath")?.WithVolume(1f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.knockBackResist = 0.9f;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.aiStyle = 108;
		base.animationType = 559;
		base.aiType = 560;
		((ModNPC)this).npc.buffImmune[24] = true;
		((ModNPC)this).npc.noTileCollide = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("PhantomBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 3200;
		((ModNPC)this).npc.damage = 115;
		((ModNPC)this).npc.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore3"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override void AI()
	{
		((ModNPC)this).npc.TargetClosest();
		Player player = Main.player[((ModNPC)this).npc.target];
		ShootTimer++;
		if (ShootTimer == 600)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).mod.ProjectileType("PhantomWave"), 40, 0f, 0, 0f, 0f);
			ShootTimer = 0;
		}
		if (Main.rand.Next(500) == 0)
		{
			Main.PlaySound(50, ((ModNPC)this).npc.position, ((ModNPC)this).mod.GetSoundSlot((SoundType)50, "Sounds/PhantomIdle"));
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(4) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), 1, false, 0, false, false);
		}
	}
}
