using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Bow");
		((ModItem)this).Tooltip.SetDefault("Has a 50% chance to shoot out Ethereal Bolts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 65;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 46;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 7f;
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.shoot = 10;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shootSpeed = 10f;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(2) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("EtherealBolt"), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, -4f);
	}
}
