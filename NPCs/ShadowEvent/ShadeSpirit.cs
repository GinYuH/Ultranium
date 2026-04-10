using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class ShadeSpirit : ModNPC
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Shade Spirit");
		NPCID.Sets.TrailCacheLength[NPC.type] = 7;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 30;
		NPC.height = 28;
		NPC.damage = 70;
		NPC.defense = 50;
		NPC.lifeMax = 1100;
		NPC.HitSound = SoundID.NPCHit36;
		NPC.DeathSound = SoundID.NPCDeath6;
		NPC.knockBackResist = 0.9f;
		NPC.aiStyle = 91;
		NPC.buffImmune[24] = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("ShadeSpiritBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail").Width() * 0.5f, (float)NPC.height * 0.5f);
		for (int i = 0; i < NPC.oldPos.Length; i++)
		{
			Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
			Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length);
			spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail").Value, position, null, color, NPC.rotation, vector, NPC.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 2400;
		NPC.damage = 110;
		NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/ShadeSpiritGore"));
		return true;
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
		NPC.rotation = NPC.velocity.X * 0.06f;
		bool expertMode = Main.expertMode;
		if (NPC.localAI[1] == 0f)
		{
			int num = 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = 360 / num;
				int num3 = (expertMode ? 45 : 50);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("SoulOrbiter").Type, num3, 0f, Main.myPlayer, (float)(i * num2), (float)NPC.whoAmI);
			}
			NPC.localAI[1] += 1f;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(6) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
