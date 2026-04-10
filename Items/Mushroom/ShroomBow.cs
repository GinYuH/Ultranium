using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Bow");
		// ((ModItem)this).Tooltip.SetDefault("Has a 50% chance to convert arrows into fast moving fungus arrows");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 12;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 46;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 7f;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.UseSound = SoundID.Item5;
		((ModItem)this).Item.shoot = 10;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shootSpeed = 5f;
		((ModItem)this).Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(2) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).Mod.Find<ModProjectile>("ShroomArrow").Type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(183, 10);
		val.AddTile(16);
		val.Register();
	}
}
