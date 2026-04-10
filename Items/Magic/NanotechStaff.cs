using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class NanotechStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Conjures a small lingering nanite\nHas a chance to shoot a bigger nanite that will not stop moving");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 60;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 10;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.value = Item.buyPrice(0, 45, 50);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("Nanite");
		((ModItem)this).item.shootSpeed = 20f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("NaniteBig"), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1346, 15);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
