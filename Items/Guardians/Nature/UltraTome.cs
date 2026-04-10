using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraTome : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultranium Grimoire");
		((ModItem)this).Tooltip.SetDefault("Creates a circle of nature blasts that close in on the cursor");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 210;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 18;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 60;
		((ModItem)this).item.useAnimation = 60;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.scale = 0.8f;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("NatureBlastBase");
		((ModItem)this).item.shootSpeed = 0f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
		Projectile.NewProjectile(vector.X, vector.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
