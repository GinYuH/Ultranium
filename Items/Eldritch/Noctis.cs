using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Noctis : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Noctis");
		((ModItem)this).Tooltip.SetDefault("Fires a spread of eldritch knives");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.scale = 1f;
		((ModItem)this).item.damage = 210;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchKnife");
		((ModItem)this).item.shootSpeed = 22f;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(25f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
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
