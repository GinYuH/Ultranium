using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Bow of Distress");
		((ModItem)this).Tooltip.SetDefault("Converts arrows into dread bolts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 45;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 46;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 4f;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(0, 12);
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.shoot = 10;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shootSpeed = 9f;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
		Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("DreadBowBolt"), damage, knockBack, player.whoAmI, 0f, 0f);
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
		val.AddIngredient((Mod)null, "DreadFlame", 10);
		val.AddIngredient((Mod)null, "DreadScale", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, -4f);
	}
}
