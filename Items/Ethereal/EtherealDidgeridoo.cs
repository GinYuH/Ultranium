using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealDidgeridoo : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ethereal Didgeridoo");
		//Tooltip.SetDefault("Conujures lingering ethereal notes that create ethereal tentacles upon death");
	}

	public override void SetDefaults()
	{
		Item.damage = 75;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 28;
		Item.height = 32;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.buyPrice(0, 30);
		Item.UseSound = new Terraria.Audio.SoundStyle("Ultranium/Sounds/Item/Didgeridoo");
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EtherealNote").Type;
		Item.shootSpeed = 10f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-8f, 0f);
	}
}
