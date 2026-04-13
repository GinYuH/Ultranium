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
		//DisplayName.SetDefault("Glowing Mushroom Bow");
		//Tooltip.SetDefault("Has a 50% chance to convert arrows into fast moving fungus arrows");
	}

	public override void SetDefaults()
	{
		Item.damage = 12;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 46;
		Item.height = 18;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item5;
		Item.shoot = ProjectileID.PurificationPowder;
		Item.autoReuse = true;
		Item.shootSpeed = 5f;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(2) == 0)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y);
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("ShroomArrow").Type, damage, knockback, player.whoAmI, 0f, 0f);
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.GlowingMushroom, 10);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
