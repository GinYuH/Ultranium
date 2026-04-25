using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.ShadowEvent;

public class Scp2521 : ModNPC
{
	public int JumpTimer;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyss Strider");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 100;
		NPC.height = 50;
		NPC.damage = 80;
		NPC.defense = 70;
		NPC.lifeMax = 1800;
		NPC.knockBackResist = 0.1f;
		NPC.aiStyle = NPCAIStyleID.Fighter;
		AIType = NPCID.AnomuraFungus;
		NPC.HitSound = SoundID.NPCHit49;
		NPC.DeathSound = SoundID.NPCDeath55;
		Banner = NPC.type;
		BannerItem = Mod.Find<ModItem>("Scp2521Banner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 3500;
		NPC.damage = 130;
		NPC.defense = 70;
	}

	public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0 || Main.dedServ)
            return;
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Scp2521Gore1").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Scp2521Gore2").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Scp2521Gore3").Type);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
		Player player = Main.player[NPC.target];
		NPC.TargetClosest();
		new Vector2(NPC.position.X + (float)(NPC.width / 2), NPC.position.Y + (float)(NPC.height / 2));
		if (Vector2.Distance(NPC.Center, player.Center) >= 650f && Main.rand.Next(350) == 0)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			int num = Main.rand.Next(1, 1);
			for (int i = 0; i < num; i++)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("DarkMatterBolt").Type, 40, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		JumpTimer--;
		float num2 = 10.5f;
		if (Math.Abs(NPC.Center.X - player.Center.X) <= 100f && NPC.Bottom.Y > player.Bottom.Y && NPC.velocity.Y == 0f && JumpTimer <= 0)
		{
			NPC.velocity.Y -= num2;
			JumpTimer = 15;
		}
		if (Main.rand.Next(500) == 0)
		{
			SoundStyle num3 = SoundID.Zombie41;
			switch (Main.rand.Next(2))
			{
			case 0:
				num3 = SoundID.Zombie41;
				break;
			case 1:
				num3 = SoundID.Zombie42;
				break;
			case 2:
				num3 = SoundID.Zombie43;
				break;
			}
			SoundEngine.PlaySound(num3, NPC.position);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 11.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 5));
    }
}
