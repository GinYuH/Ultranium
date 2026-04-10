using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Fungal Bulb Scepter");
		// Tooltip.SetDefault("Summons a stationary fungus bulb that shoots spores at enemies\nOnly one bulb can be active at once");
	}

	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 28;
		Item.rare = 1;
		Item.mana = 20;
		Item.damage = 12;
		Item.knockBack = 1f;
		Item.useStyle = 1;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.DamageType = DamageClass.Summon;
		Item.noMelee = true;
		Item.UseSound = SoundID.Item117;
		Item.shoot = Mod.Find<ModProjectile>("ShroomBulb").Type;
		Item.shootSpeed = 0f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < Main.projectile.Length; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (((Entity)projectile).active && projectile.type == Item.shoot && projectile.owner == player.whoAmI)
			{
				((Entity)projectile).active = false;
			}
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
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
