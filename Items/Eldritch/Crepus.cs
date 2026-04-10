using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Crepus : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Crepus");
		// Tooltip.SetDefault("Shoots a spread of green and purple shade arrows\nGreen shade arrows will accelerate quickly\nPurple arrows move slowly but deal rapid damage");
	}

	public override void SetDefaults()
	{
		Item.damage = 270;
		Item.width = 20;
		Item.height = 40;
		Item.useTime = 18;
		Item.useAnimation = 18;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Ranged;
		Item.autoReuse = true;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1, 50);
		Item.useAmmo = AmmoID.Arrow;
		Item.shoot = 11;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shootSpeed = 8f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 2f;
		float num2 = MathHelper.ToRadians(3f);
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.4f;
			Projectile.NewProjectile(null, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("ShadeArrowGreen").Type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		float num3 = MathHelper.ToRadians(1f);
		for (int j = 0; (float)j < num; j++)
		{
			Vector2 vector2 = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num3, num3, (float)j / (num - 1f))) * 0.4f;
			Projectile.NewProjectile(null, position.X, position.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("ShadeArrow").Type, damage, knockBack, player.whoAmI, 0f, 0f);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.Register();
	}
}
