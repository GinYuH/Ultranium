using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.ShadowEvent;

public class AbyssalWraith : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Wraith");
		Main.npcFrameCount[NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 64;
		NPC.height = 84;
		NPC.damage = 80;
		NPC.defense = 65;
		NPC.lifeMax = 1500;
		NPC.HitSound = SoundID.NPCHit54;
		NPC.DeathSound = SoundID.NPCDeath6;
		NPC.knockBackResist = 0f;
		NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.buffImmune[24] = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("AbyssalWraithBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 3200;
		NPC.damage = 110;
		NPC.defense = 70;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalWraithGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalWraithGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalWraithGore3").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalWraithGore4").Type);
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		NPC.rotation = NPC.velocity.X * 0.05f;
		NPC.spriteDirection = NPC.direction;
		Player player = Main.player[NPC.target];
		NPC.netUpdate = true;
		NPC.TargetClosest();
		NPC.velocity.Y = -100f;
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && MoveSpeed >= -53)
			{
				MoveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && MoveSpeed <= 53)
			{
				MoveSpeed++;
			}
			NPC.velocity.X = (float)MoveSpeed * 0.07f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
			{
				MoveSpeedY++;
			}
			NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -34f;
			}
		}
		NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
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

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 5));
    }
}
