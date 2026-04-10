using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Sword");
		((ModItem)this).Tooltip.SetDefault("Has a chance to fire out a mushroom bolt");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 10;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 54;
		((Entity)(object)((ModItem)this).item).height = 54;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shootSpeed = 2f;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("MushroomBolt");
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(3) == 0)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("MushroomBolt"), damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(183, 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
