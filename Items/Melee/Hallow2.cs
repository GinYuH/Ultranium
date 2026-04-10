using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Hallow2 : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("True Chaos Blade");
		((ModItem)this).Tooltip.SetDefault("Fires a Chaos star\nHas a 20% chance to fire out a large chaos blast\nThe chaos blast will home and deal double the swords base damage");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 80;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 84;
		((Entity)(object)((ModItem)this).item).height = 84;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.UseSound = SoundID.Item60;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("BlueStar");
		((ModItem)this).item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("HallowBlast"), 160, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "Hallow", 1);
		val.AddIngredient(549, 5);
		val.AddIngredient(548, 5);
		val.AddIngredient(547, 5);
		val.AddIngredient(502, 25);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
