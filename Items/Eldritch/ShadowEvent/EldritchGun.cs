using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchGun : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Fires out eldritch tentacles\nDoes not require ammo to use");
		((ModItem)this).DisplayName.SetDefault("Death's Raze");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.damage = 280;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 6;
		((ModItem)this).item.useAnimation = 6;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item34;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ShadeTentacle");
		((ModItem)this).item.shootSpeed = 32f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(speedX, speedY).SafeNormalize(-Vector2.UnitY);
		Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
		vector = (vector * 4f + vector2).SafeNormalize(-Vector2.UnitY) * ((ModItem)this).item.shootSpeed;
		float num = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num *= -1f;
		}
		float num2 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num2 *= -1f;
		}
		Projectile.NewProjectile(position, vector, type, damage, knockBack, player.whoAmI, num, num2);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
