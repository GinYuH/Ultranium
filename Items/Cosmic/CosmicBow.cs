using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
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
		((ModItem)this).DisplayName.SetDefault("Cosmic Annahilation");
		((ModItem)this).Tooltip.SetDefault("Fires a tight spread of homing cosmic bolts\nDoes not require ammo to use");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 400;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 48;
		((Entity)(object)((ModItem)this).item).height = 74;
		((ModItem)this).item.useTime = 23;
		((ModItem)this).item.useAnimation = 23;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(2);
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("CosmicBowBolt");
		((ModItem)this).item.shootSpeed = 13.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.overrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(5f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("CosmicBowBolt"), ((ModItem)this).item.damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, 0f);
	}
}
