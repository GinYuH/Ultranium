using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Cosmic;

public class CosmicBow : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(93, 215, 195),
		new Color(72, 37, 169)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Annahilation");
		//Tooltip.SetDefault("Fires a tight spread of homing cosmic bolts\nDoes not require ammo to use");
	}

	public override void SetDefaults()
	{
		Item.damage = 400;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 48;
		Item.height = 74;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 1f;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.buyPrice(2);
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("CosmicBowBolt").Type;
		Item.shootSpeed = 13.5f;
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

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(5f);
		position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 10f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("CosmicBowBolt").Type, Item.damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, 0f);
	}
}
