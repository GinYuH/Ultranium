using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class DevotedKatana : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(255, 65, 176),
		new Color(132, 22, 199)
	};

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Devoted Katana");
		// Tooltip.SetDefault("Shoots spreads of homing shadow stars\n~Dedicated Item~");
	}

	public override void SetDefaults()
	{
		Item.damage = 200;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 60;
		Item.height = 78;
		Item.useTime = 16;
		Item.useAnimation = 16;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.value = Item.buyPrice(2);
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DStar").Type;
		Item.shootSpeed = 10f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 100f;
		if (Collision.CanHit(position, 0, 0, position + vector, 0, 0))
		{
			position += vector;
		}
		int num = Main.rand.Next(3, 4);
		for (int i = 0; i < num; i++)
		{
			Projectile.NewProjectile(null, position, new Vector2(velocity.X, velocity.Y).RotatedByRandom(0.19634954631328583), type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
