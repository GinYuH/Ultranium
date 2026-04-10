using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class ShadowFlute : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(34, 166, 162),
		new Color(138, 7, 163)
	};

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Despairides");
		// ((ModItem)this).Tooltip.SetDefault("When played, the flute will create a phantom claw that will home in on enemies\n~Developer Item~");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 250;
		((ModItem)this).Item.mana = 20;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 58;
		((ModItem)this).Item.useTime = 21;
		((ModItem)this).Item.useAnimation = 21;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 1f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(2);
		((ModItem)this).Item.UseSound = ((ModItem)this).Mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/Flute");
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("PhantomClaw").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, -4f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
