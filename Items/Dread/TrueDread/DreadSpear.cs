using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadSpear : ModItem
{
	private int currentHit;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Inquietude Impaler");
		// ((ModItem)this).Tooltip.SetDefault("Fires slightly inaccurate dread scythes");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 200;
		((Entity)(object)((ModItem)this).Item).width = 64;
		((Entity)(object)((ModItem)this).Item).height = 64;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.knockBack = 9f;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.useTime = 17;
		((ModItem)this).Item.useAnimation = 17;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DreadSpear").Type;
		((ModItem)this).Item.shootSpeed = 15f;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		currentHit = 0;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[((ModItem)this).Item.shoot] > 0)
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 spinningpoint = new Vector2(speedX, speedY);
		Vector2 zero = Vector2.Zero;
		zero = ((Main.rand.Next(2) != 1) ? spinningpoint.RotatedBy(-Math.PI / (double)(Main.rand.Next(82, 1800) / 10)) : spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)));
		speedX = zero.X;
		speedY = zero.Y;
		currentHit++;
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
