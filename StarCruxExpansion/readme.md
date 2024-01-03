### Description
Adds more difficulty levels to the **Star Crux**.

Done:
- Added several more difficulty levels to most if not all `The Outer Circle` **Star Crux** challenges.
- Increased Item Rarity progress bar includes modded difficulty levels.

To Do:
- Add custom modded challenges
  - Sync modded points pages with vanilla points
  - Save modded realms' points on page navigate
  - Implement an affordance to notify the user that there are modded realms
- ~~Add own page in the Star Crux for modded challenges~~
- Have the challenges appear properly in-game


Alterations:

| Code            |       Name       | Description                                    | # Increase Per Point | Max Level | Points Per Level |
|:----------------|:----------------:|:-----------------------------------------------|:--------------------:|:---------:|:----------------:|
| EliteMoreMs     |     Make Way     | Elite enemies have +# movement                 |          30          |  3 -> 6   |        2         |
| EliteMoreAre    | Bigger Is Better | Elite enemies have +#% area.                   |          30          |  2 -> 3   |        1         |
| MinionsMoreDmg  |  Three Hundred   | Minions have +#% damage in the last 3 minutes. |          80          |     1     |        3         |
| EnemiesMoreProj |  Liama Blessed   | Enemies have +# extra projectiles.             |          2           |  1 -> 3   |      3 -> 5      |
| NoPickUps       |   Instant Kill   | Enemy projectiles are +#% faster.              |          35          |  1 -> 2   |      2 -> 3      |
| BossesMoreDmg   |   Death Struck   | Bosses have +#% damage.                        |          30          |  2 -> 4   |      2 -> 3      |
| PlayerNoHeal    | Blackened Skies  | Elites and bosses have +# extra projectiles.   |          2           |  1 -> 4   |        4         |
| BossesMoreLife  |  Borrowed Time   | Bosses have +#% more life per rank.            |          35          |  3 -> 12  |        2         |

