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
		// ((ModNPC)this).DisplayName.SetDefault("Shade Spirit");
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 7;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 28;
		((ModNPC)this).NPC.damage = 70;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.lifeMax = 1100;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit36;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).NPC.knockBackResist = 0.9f;
		((ModNPC)this).NPC.aiStyle = 91;
		((ModNPC)this).NPC.buffImmune[24] = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("ShadeSpiritBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
		for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
		{
			Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
			Color color = ((ModNPC)this).NPC.GetAlpha(lightColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail"), position, null, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 2400;
		((ModNPC)this).NPC.damage = 110;
		((ModNPC)this).NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/ShadeSpiritGore"));
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
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.06f;
		bool expertMode = Main.expertMode;
		if (((ModNPC)this).NPC.localAI[1] == 0f)
		{
			int num = 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = 360 / num;
				int num3 = (expertMode ? 45 : 50);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("SoulOrbiter").Type, num3, 0f, Main.myPlayer, (float)(i * num2), (float)((ModNPC)this).NPC.whoAmI);
			}
			((ModNPC)this).NPC.localAI[1] += 1f;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(6) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
