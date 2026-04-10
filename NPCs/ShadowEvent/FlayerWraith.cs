using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class FlayerWraith : ModNPC
{
	private int timer;

	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Flayer Wraith");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1.2f;
		((ModNPC)this).NPC.width = 78;
		((ModNPC)this).NPC.height = 112;
		((ModNPC)this).NPC.damage = 60;
		((ModNPC)this).NPC.lifeMax = 4000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit55;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath52;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.buffImmune[24] = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.aiStyle = 0;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("FlayerWraithBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 5000;
		((ModNPC)this).NPC.damage = 110;
		((ModNPC)this).NPC.defense = 65;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/FlayerWraithTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/FlayerWraithTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/FlayerWraithGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/FlayerWraithGore2"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.01f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		bool expertMode = Main.expertMode;
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
			((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.1f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -36f;
			}
		}
		timer++;
		if (timer == 360)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 5.5f;
			vector.Y *= 5.5f;
			int num = (expertMode ? 40 : 45);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("DarkMatterBoltGreen").Type, num, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
			timer = 0;
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
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(1, 2), false, 0, false, false);
		}
	}
}
