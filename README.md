# Improved Water Wheel
Town of Salem mod that overhauls the Water Wheel scene to make it more unpredictable.

# Support
For any issues regarding the mod, please seek help in this [Discord server](https://discord.gg/MZb8cUD).

# License
Improved Water Wheel mod is licensed under **[GNU Lesser General Public License version 3](https://www.gnu.org/licenses/lgpl-3.0.txt)** (shortened to **LGPLv3**).
You are free to do anything you want, as long as it falls under the terms of this license.

Even though this mod is open-source, due to the nature of Town of Salem modding, I will not be providing any support on how to do so (except explain how my mods work). You're on your own.

# Settings
## oddsMode
**Default: `2`**

Controls how the chances for the roles displayed on the water wheel are calculated. Can be set to a number ranging `0 - 3`.

`0 and 1` - The vanilla odds implementation, it just divides your role lots by the total lots. Everyone has 10 lots on a role by default, but a (normal) scroll multiplies your rolls by 10.

`2` - The vanilla odds are multiplied by the chance that the role appears in your current role list in the first place. This is meant to more realistically portray your chance of getting each role. Additionally for Unique roles, every consequent role list entry accounts for the fact that previous entries should NOT have rolled that specific Unique roles. Keep in mind that these odds are not fully accurate. They don't account for Unique roles having a chance to increase all other roles' odds. They also don't account for non-Unique roles having a chance of appearing more than once. The special mafia and VH rules are also not accounted for.

`3` - Like oddsMode 2, except it also accounts for VH and the Mafia's rules. Still not accurate in regards to Unique roles. Added in v1.0.4.

## disabled
**Default: `false`**

If set to `true`, the Improved Water Wheel will be disabled without the mod being uninstalled.

Note: The mod will still look over all the files and read through them. If you did something that would make it break, while the setting is set to `true` it wouldn't matter anyway, though.

## debugMode
**Default: `false`**

It is recommended to keep this setting at its default, it is a setting I used to aid with testing of the mod.

Setting it to `true` will cause the role lots text to flash (if you enable it) with different colors, I used it together with OBS to determine how the water wheel arrowhead was placed for debugging purposes.

## logMode
**Default: `true`**

It is recommended to keep this setting at its default, it is a setting that helps with collecting logs about the mod.

Setting it to `false` will remove a lot of this mod's logs. Most of them generate right when the Water Wheel scene triggers, since that's when most of the work is done. Also, you won't see the logs anyway unless you change the BepInEx's configuration to include `Debug` severity logs!

## enableMockGameDebug
**Default: `false`**

It is recommended to keep this setting at its default, as otherwise you may compromise your ability to read the Achievements tab of the main menu. Even then this setting's function won't work if you don't have BetterTOS installed.

This setting, if set to `true` will trigger a fake test game if you click any tab on the left in the Achievements section of the main menu. This fake test game will switch to the water wheel 2 seconds after the pick names screen is shown to simply show the Water Wheel. Evidently, I used this to test my mod and may still do so in the future. This fake game attempts to adds BetterTOS roles to the Water Wheel. If you don't have the mod already, you'll probably crash here, so I don't recommend trying it.

## roleApparanceMode
**Default: `1`**

Controls the chances of the non-role group and non-blab roles appearing on the water wheel.

`0` - all roles have equal chances to appear.

`1` - odds are affected by scrolls, but that's it.

`2` - odds are identical to the chance displayed by your current oddsMode.

## Respin odds
`immediateRespinChance` - **Default: `0.05`**

`delayedRespinChance` - **Default: `0.002`**

These two settings control the odds of these respins appearing. They are consecutive, so if any of those appear, they are rerolled until the first failure. Keep in mind that these settings represent a chance with a number from 0 to 1, where `1 = 100%` and `0 = 0%`.

## Maximum respin count
`maxImmediateRespins` - **Default: `20`**

`maxDelayedRespins` - **Default: `10`**

As they self-describe, these are the limits on the respins you can get for both kinds. They are set up so that you don't end up with the water wheel being spun all the way until Day 1 actually starts. While you can control the max amount of delayed respins, the actual number will be lowered by the mod regardless by 1 for every 3 immediate respins(each game).

## Minimum and maximum spin strength
`minImmediateSpinStrength` - **Default: `4000`**

`maxImmediateSpinStrength` - **Default: `6500`**

`minDelayedSpinStrength` - **Default: `3800`**

`maxDelayedSpinStrength` - **Default: `6000`**

For every spin of its kind in the IWW mod, a random number is picked between these two to determine the peak speed of each spin. All 4 of these are set to the amount of y increments per second. Weird thing about the screen in Town of Salem - it seems to be around 2200 y high... regardless of your monitor. Maybe this will affect these settings too.

## Flatline Factors
`minImmediateFlatlineFactor` - **Default: `0.8`**

`maxImmediateFlatlineFactor` - **Default: `1.2`**

`minDelayedFlatlineFactor` - **Default: `0.7`**

`maxDelayedFlatlineFactor` - **Default: `1.2`**

The speed of the spins in Town of Salem follow a specific pattern - it starts off as a parabola, but then for the last length of speed it turns into a straight line (which slows down until 0 - a lot more slowly than that parabola would've). When the Water Wheel enters this linear decrease in speed is what I call a 'flatline'. The flatline factor determines how far off from 0 the flatline starts. A low flatline factor will cause the water wheel to enter the flatline later, while a high flatline factor will cause the water wheel to enter the flatline sooner, thus making it take longer to fully halt.
A flatline factor of 0 will mean the Water Wheel's speed will plot a parabola from start to finish.

## Role Group Weights
`customRoleGroupWeight` - **Default: `0`**

`investResultWeight` - **Default: `0.7`**

`subalignmentWeight` - **Default: `0.25`**

`factionWeight` - **Default: `0.05`**

As you can tell there are 4 role group types: Investigator result, subalignment (or basically alignment), an entire faction or custom. The first 3 types are shown off in the video of the Water Wheel mod, but the Custom role group type requires custom role groups to be set up in the file to function.
A 'weight' determines how likely this type of role group to be picked (when a role group spawns). A weight of 0 guarantees this type never spawns. In the default setting, invest results have a 70% chance to be picked for a role group, due to having a weight of `0.7`, out of the total combined of `1`, making it `0.7 / 1`. You need to set a higher weight for custom role groups than 0 in order for Custom Role Groups to spawn.

## Role Group Spawn Chance
`roleGroupSpawnChance` - **Default: `0.5`**

The chance for a role group to spawn. Defaults to `50%` and is also consecutive. Meaning if it hits the `50%`, it will keep going until it fails or hits the maximum amount of role groups. Just like for spins, this is a number ranging from 0 to 1.

## Max Role Group Count
`maxRoleGroups` - **Default: `20`**

The max amount of role groups that can spawn on the Water Wheel. Now should be a good time to note that if a role group has no legitimate spot to spawn, it just won't. Blabs take priority over role groups, so if the Water wheel is filled with blabs, role groups won't have space to spawn, so be careful with that.

## Blab Spawn Chance
`blabSpawnChance` - **Default: `0.001`**

A number ranging from `0 to 1`, which represents the chance for a blab to spawn on every slot. Defaults to `0.1%`.

## Flatline Falloff Speed
`maxFlatlineFalloffSpeed` - **Default: `5.9`**

`minFlatlineFalloffSpeed` - **Default: `3.7`**

The water wheel slows down to a halt at a certain speed. Up until v1.0.5, this speed was locked into place. You can customize the minimum and maximum of this fall off speed. I don't recommend setting it to a low value, as that will result in slow spins, with longer simulations at the start (aka longer stutters).

## Arrow Slot Coverage
`maxArrowheadSlotCoverage` - **Default: `0.4`**

`minArrowheadSlotCoverage` - **Default: `0.15`**

To flip to the next slot, the arrow's position has to cover a specific proportion of the next slot, while the arrow is rotating. Prior to v1.0.5, this proportion was locked to 35% of the next slot, but is now random every game. Setting this to 0 will cause the arrow to never rotate and always point in the same direction.
