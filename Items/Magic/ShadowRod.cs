using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class ShadowRod : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Conjures a short lived shadow bolt that pierces through enemies");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 10;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 40;
		Item.useAnimation = 40;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = 10000;
		Item.rare = 2;
		Item.value = Item.buyPrice(0, 10);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShadowRodBolt").Type;
		Item.shootSpeed = 5f;
	}
}
