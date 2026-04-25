using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;

namespace Ultranium.NPCs.ShadowEvent;

public class Warden : ModNPC
{
	public int JumpTimer;

	public int Timer;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Brute");
		Main.npcFrameCount[NPC.type] = 8;
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 138;
		NPC.height = 144;
		NPC.damage = 80;
		NPC.defense = 70;
		NPC.lifeMax = 12000;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit49;
		NPC.DeathSound = SoundID.NPCDeath55;
		Banner = NPC.type;
		BannerItem = Mod.Find<ModItem>("AbyssBruteBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 24000;
		NPC.damage = 130;
		NPC.defense = 70;
	}

	public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0 || Main.dedServ)
            return;
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WardenGore1").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WardenGore2").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WardenGore3").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WardenGore4").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WardenGore5").Type);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180);
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
		_ = Main.player[NPC.target];
		NPC.TargetClosest();
		Timer++;
		if (Timer == 500)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 25f;
			vector.Y *= 25f;
			NPC.velocity.X = vector.X;
			NPC.velocity.Y = vector.Y;
			Vector2 vector2 = Main.player[NPC.target].Center - NPC.Center;
			vector2.Normalize();
			vector2.X *= 25f;
			vector2.Y *= 25f;
		}
		if (Timer > 500 && Timer < 570)
		{
			NPC.rotation += 0.5f * (float)NPC.direction;
			Vector2 position = NPC.Center + Vector2.Normalize(NPC.velocity) * 10f;
			Dust obj = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald)];
			obj.position = position;
			obj.velocity = NPC.velocity.RotatedBy(Math.PI / 2.0) * 0.05f + NPC.velocity / 2f;
			obj.position += NPC.velocity.RotatedBy(Math.PI / 2.0);
			obj.fadeIn = 0.5f;
			obj.noGravity = true;
			Dust obj2 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald)];
			obj2.position = position;
			obj2.velocity = NPC.velocity.RotatedBy(-Math.PI / 2.0) * 0.05f + NPC.velocity / 2f;
			obj2.position += NPC.velocity.RotatedBy(-Math.PI / 2.0);
			obj2.fadeIn = 0.5f;
			obj2.noGravity = true;
		}
		else
		{
			NPC.rotation = 0f;
		}
		if (Timer < 840)
		{
			NPC.aiStyle = NPCAIStyleID.Fighter;
			AIType = NPCID.GiantWalkingAntlion;
		}
		if (Timer > 840)
		{
			NPC.velocity.X *= 0f;
			if (Timer == 880 || Timer == 920 || Timer == 960)
			{
				Vector2 vector3 = Main.player[NPC.target].Center - NPC.Center;
				vector3.Normalize();
				vector3.X *= 6f;
				vector3.Y *= 6f;
				int num = Main.rand.Next(3, 5);
				for (int i = 0; i < num; i++)
				{
					float num2 = (float)Main.rand.Next(-300, 300) * 0.01f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector3.X + num2, vector3.Y + num2, Mod.Find<ModProjectile>("WardenBolt").Type, 60, 1f, Main.myPlayer, 0f, 0f);
				}
			}
		}
		if (Timer == 1020)
		{
			Timer = 0;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter > 6.0)
		{
			NPC.frame.Y = NPC.frame.Y + frameHeight;
			NPC.frameCounter = 0.0;
		}
		if (NPC.frame.Y >= frameHeight * 6)
		{
			NPC.frame.Y = 0;
		}
		if (Timer > 500 && Timer < 570)
		{
			NPC.frame.Y = 7 * frameHeight;
		}
		if (Timer > 840)
		{
			NPC.frame.Y = 6 * frameHeight;
		}
	}


    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 5));
    }
}
