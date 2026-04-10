using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glowing Mushroom Sword");
		// ((ModItem)this).Tooltip.SetDefault("Has a chance to fire out a mushroom bolt");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 10;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 54;
		((Entity)(object)((ModItem)this).Item).height = 54;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shootSpeed = 2f;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("MushroomBolt").Type;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(3) == 0)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).Mod.Find<ModProjectile>("MushroomBolt").Type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
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
