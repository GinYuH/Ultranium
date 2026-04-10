using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Phantom : ModNPC
{
	public int ShootTimer;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Phantom");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[560];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1.2f;
		((ModNPC)this).NPC.width = 34;
		((ModNPC)this).NPC.height = 34;
		((ModNPC)this).NPC.damage = 80;
		((ModNPC)this).NPC.defense = 60;
		((ModNPC)this).NPC.lifeMax = 2000;
		((ModNPC)this).NPC.HitSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomHit");
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomDeath")?.WithVolume(1f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.knockBackResist = 0.9f;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.aiStyle = 108;
		base.AnimationType = 559;
		base.AIType = 560;
		((ModNPC)this).NPC.buffImmune[24] = true;
		((ModNPC)this).NPC.noTileCollide = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("PhantomBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 3200;
		((ModNPC)this).NPC.damage = 115;
		((ModNPC)this).NPC.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/PhantomGore3"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override void AI()
	{
		((ModNPC)this).NPC.TargetClosest();
		Player player = Main.player[((ModNPC)this).NPC.target];
		ShootTimer++;
		if (ShootTimer == 600)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("PhantomWave").Type, 40, 0f, 0, 0f, 0f);
			ShootTimer = 0;
		}
		if (Main.rand.Next(500) == 0)
		{
			SoundEngine.PlaySound(50, ((ModNPC)this).NPC.position, ((ModNPC)this).Mod.GetSoundSlot((SoundType)50, "Sounds/PhantomIdle"));
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(4) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
