using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Ultranium.Items.Eldritch;

public class ErebusGuitar : ModItem
{
	public int AttackMode = 1;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eld-riff");
		Tooltip.SetDefault("'Blasting enemies with the power of eldritch music!'\nHas 3 different attack modes that can be switched by right clicking\nMode 1 will shoot green sound pulses that bounce off of tiles\nMode 2 will fire circles of fast moving purple sound pulses\nMode 3 will rapidly fire eldritch notes\nThe current mode you are in will be displayed in the corner of the item");
	}

	public override void SetDefaults()
	{
		Item.damage = 270;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 28;
		Item.height = 32;
		Item.useTime = 15;
		Item.useAnimation = 45;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1, 50);
        Item.UseSound = new SoundStyle("Ultranium/Sounds/Item/ErebusGuitar1");
        Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ErebusGuitarPulse").Type;
		Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		for (int i = 0; i < 10; i++)
		{
			string text = string.Concat(AttackMode);
			new Vector2(Main.hotbarScale[i], Main.hotbarScale[i]);
			if (Main.player[Main.myPlayer].inventory[i] == Item)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Turquoise, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
			}
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			AttackMode++;
			if (AttackMode > 3)
			{
				AttackMode = 1;
			}
			SoundEngine.PlaySound(SoundID.MenuTick, player.position);
			switch (AttackMode)
			{
			case 1:
				Item.damage = 320;
				Item.DamageType = DamageClass.Magic;
				Item.mana = 12;
				Item.width = 28;
				Item.height = 32;
				Item.useTime = 15;
				Item.useAnimation = 45;
				Item.useStyle = 5;
				Item.noMelee = true;
				Item.knockBack = 5f;
				Item.rare = 11;
				Item.UseSound = new SoundStyle("Ultranium/Sounds/Item/ErebusGuitar1");
				Item.autoReuse = true;
				Item.shoot = Mod.Find<ModProjectile>("ErebusGuitarPulse").Type;
				Item.shootSpeed = 13f;
				break;
			case 2:
				Item.damage = 320;
				Item.DamageType = DamageClass.Magic;
				Item.mana = 12;
				Item.width = 28;
				Item.height = 32;
				Item.useTime = 60;
				Item.useAnimation = 60;
				Item.useStyle = 5;
				Item.noMelee = true;
				Item.knockBack = 5f;
				Item.rare = 11;
                    Item.UseSound = new SoundStyle("Ultranium/Sounds/Item/ErebusGuitar2");
                    Item.autoReuse = true;
				Item.shoot = Mod.Find<ModProjectile>("ErebusGuitarPulse").Type;
				Item.shootSpeed = 10f;
				break;
			case 3:
				Item.damage = 320;
				Item.DamageType = DamageClass.Magic;
				Item.mana = 12;
				Item.width = 28;
				Item.height = 32;
				Item.useTime = 12;
				Item.useAnimation = 120;
				Item.useStyle = 5;
				Item.noMelee = true;
				Item.knockBack = 5f;
				Item.rare = 11;
                    Item.UseSound = new SoundStyle("Ultranium/Sounds/Item/ErebusGuitar3");
                    Item.autoReuse = true;
				Item.shoot = Mod.Find<ModProjectile>("ErebusGuitarPulse").Type;
				Item.shootSpeed = 10f;
				break;
			default:
				return true;
			}
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.altFunctionUse != 2)
		{
			if (AttackMode == 1)
			{
				Projectile.NewProjectile(null, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ErebusGuitarPulse").Type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			else if (AttackMode == 2)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector2 vector = ((float)Math.PI / 4f * (float)i).ToRotationVector2();
					vector.Normalize();
					vector *= 6f;
					Projectile.NewProjectile(null, player.Center.X, player.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("ErebusGuitarPulsePurple").Type, damage, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			else if (AttackMode == 3)
			{
				int num = Main.rand.Next(2);
				if (num == 0)
				{
					Projectile.NewProjectile(null, position.X, position.Y, velocity.X, velocity.X, Mod.Find<ModProjectile>("EldritchNote1").Type, damage, knockback, player.whoAmI, 0f, 0f);
				}
				if (num == 1)
				{
					Projectile.NewProjectile(null, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("EldritchNote2").Type, damage, knockback, player.whoAmI, 0f, 0f);
				}
			}
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
	}
}
