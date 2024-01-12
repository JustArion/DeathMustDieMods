### Description
Adds more difficulty levels to the **Star Crux**.

Done:
- Added several more difficulty levels to most if not all `The Outer Circle` **Star Crux** challenges.
- Increased Item Rarity progress bar includes modded difficulty levels.
- Merged the [Longer Games](../LongerGames) mod into `Star Crux Expansion` under the `Star Crux Extras` realm.
- Sync modded points pages with vanilla points
- Implement an affordance to notify the user that there are modded realms
- Add own page in the Star Crux for modded challenges
  - Add custom modded challenges
- Have the challenges appear properly in-game
- Save modded realms' points on page navigate

To Do:
- Further refactorings for code quality and expandability in the future. (Mostly done in the form of builders)
- More challenges for Realm Expansion #1

[!NOTE]
Save data is stored at `...\Death Must Die\BepInEx\config\dawn.dmd.starcruxexpansion.cfg` 

### Alterations:

| Code            |       Name       | Description                                    | # Increase Per Point | Max Level | Points Per Level |
|:----------------|:----------------:|:-----------------------------------------------|:--------------------:|:---------:|:----------------:|
| EliteMoreMs     |     Make Way     | Elite enemies have +# movement                 |          30          |  3 -> 6   |        2         |
| EliteMoreAre    | Bigger Is Better | Elite enemies have +#% area.                   |          30          |  2 -> 3   |        1         |
| MinionsMoreDmg  |  Three Hundred   | Minions have +#% damage in the last 3 minutes. |          80          |     1     |        3         |
| EnemiesMoreProj |  Liama Blessed   | Enemies have +# extra projectiles.             |          2           |  1 -> 3   |      3 -> 5      |
| NoPickUps       |   Instant Kill   | Enemy projectiles are +#% faster.              |          35          |  1 -> 2   |      2 -> 3      |
| BossesMoreDmg   |   Death Struck   | Bosses have +#% damage.                        |          30          |  2 -> 4   |      2 -> 3      |
| PlayerNoHeal    | Blackened Skies  | Elites and bosses have +# extra projectiles.   |          2           |  1 -> 4   |        4         |
| BossesMoreLife  |  Borrowed Time   | Bosses have +#% more life per rank.            |          35          |  3 -> 6   |      2 -> 4      |

### Modded Realms:

`Realm Expansion #1`:

| Code        |    Name    | Description                | # Increase Per Point | Max Level | Points Per Level |
|:------------|:----------:|:---------------------------|:--------------------:|:---------:|:----------------:|
| boss_spood  | The Spood  | Bosses have +#% movement.  |          35          |     5     |        2         |
| trash_spood | The Homies | Minions have +# movement.  |          10          |     5     |        3         |
| fun_kb      | Surf's Up  | Enemies have +# knockback. |          50          |    10     |        1         |


`Star Crux Extras`:

| Code         |     Name     | Description                  | # Increase Per Point | Max Level | Points Per Level |
|:-------------|:------------:|:-----------------------------|:--------------------:|:---------:|:----------------:|
| longer_runs  | Longer Runs  | Your runs are # min longer.  |          5           |    10     |        0         |
| shorter_runs | Shorter Runs | Your runs are # min shorter. |          5           |     3     |        1         |

The shortest run is currently limited to 5min by the mod, though unlikely for the user to finish the run in 5min.

[!NOTE]
Suggestions are welcome by opening an issue on the repo page.