using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class FlayerBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Life's Diminish");
		((ModItem)this).Tooltip.SetDefault("Converts all arrows into dark matter arrow bolts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 170;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 46;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.useTime = 14;
		((ModItem)this).item.useAnimation = 14;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 4f;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DarkMatterArrowBolt");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
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
		for (int i = 0; i < 1; i++)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("DarkMatterArrowBolt"), ((ModItem)this).item.damage, knockBack, ((ModItem)this).item.owner, 0f, 0f);
		}
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
