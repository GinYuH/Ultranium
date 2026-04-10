using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Crepus : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Crepus");
		((ModItem)this).Tooltip.SetDefault("Shoots a spread of green and purple shade arrows\nGreen shade arrows will accelerate quickly\nPurple arrows move slowly but deal rapid damage");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 270;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 18;
		((ModItem)this).item.useAnimation = 18;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.ranged = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
		((ModItem)this).item.shoot = 11;
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shootSpeed = 8f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 2f;
		float num2 = MathHelper.ToRadians(3f);
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.4f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("ShadeArrowGreen"), damage, knockBack, player.whoAmI, 0f, 0f);
		}
		float num3 = MathHelper.ToRadians(1f);
		for (int j = 0; (float)j < num; j++)
		{
			Vector2 vector2 = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num3, num3, (float)j / (num - 1f))) * 0.4f;
			Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, ((ModItem)this).mod.ProjectileType("ShadeArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
