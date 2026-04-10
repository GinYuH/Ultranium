using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class AbyssalWraith : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Abyssal Wraith");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 64;
		((ModNPC)this).NPC.height = 84;
		((ModNPC)this).NPC.damage = 80;
		((ModNPC)this).NPC.defense = 65;
		((ModNPC)this).NPC.lifeMax = 1500;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit54;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.aiStyle = 0;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.buffImmune[24] = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("AbyssalWraithBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 3200;
		((ModNPC)this).NPC.damage = 110;
		((ModNPC)this).NPC.defense = 70;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore4"));
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
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		Player player = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		((ModNPC)this).NPC.velocity.Y = -100f;
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && MoveSpeed >= -53)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && MoveSpeed <= 53)
			{
				MoveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.07f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -34f;
			}
		}
		((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
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
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(1, 1), false, 0, false, false);
		}
	}
}
