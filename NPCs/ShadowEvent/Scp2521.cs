using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Scp2521 : ModNPC
{
	public int JumpTimer;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Abyss Strider");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.width = 100;
		((ModNPC)this).NPC.height = 50;
		((ModNPC)this).NPC.damage = 80;
		((ModNPC)this).NPC.defense = 70;
		((ModNPC)this).NPC.lifeMax = 1800;
		((ModNPC)this).NPC.knockBackResist = 0.1f;
		((ModNPC)this).NPC.aiStyle = 3;
		base.AIType = 257;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit49;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath55;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("Scp2521Banner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 3500;
		((ModNPC)this).NPC.damage = 130;
		((ModNPC)this).NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore3"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override void AI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		Player player = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.TargetClosest();
		new Vector2(((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2), ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2));
		if (Vector2.Distance(((ModNPC)this).NPC.Center, player.Center) >= 650f && Main.rand.Next(350) == 0)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			int num = Main.rand.Next(1, 1);
			for (int i = 0; i < num; i++)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("DarkMatterBolt").Type, 40, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		JumpTimer--;
		float num2 = 10.5f;
		if (Math.Abs(((ModNPC)this).NPC.Center.X - player.Center.X) <= 100f && ((ModNPC)this).NPC.Bottom.Y > player.Bottom.Y && ((ModNPC)this).NPC.velocity.Y == 0f && JumpTimer <= 0)
		{
			((ModNPC)this).NPC.velocity.Y -= num2;
			JumpTimer = 15;
		}
		if (Main.rand.Next(500) == 0)
		{
			int num3 = 0;
			switch (Main.rand.Next(2))
			{
			case 0:
				num3 = 41;
				break;
			case 1:
				num3 = 42;
				break;
			case 2:
				num3 = 43;
				break;
			}
			SoundEngine.PlaySound(29, (int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, num3, 1f, 0f);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 11.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
