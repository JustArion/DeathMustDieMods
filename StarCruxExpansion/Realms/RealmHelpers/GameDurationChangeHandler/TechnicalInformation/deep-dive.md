All monster spawn events in the game are unique.

Unique in the sense that each spawn event has a specific time when it should start.

This time goes from the run start -> 1200 sec (20 mins)

Info we have access to manipulate here are:

```json
{
    "Id": 98,
    "BeginTimeSec": 1200,
    "EndTimeSec": 0,
    "Mode": 1,
    "Repeats": false,
    "RepeatIntervalSec": 0,
    "MonsterCode": "VampireLord",
    "MonsterCount": 1,
    "SharedHealth": false,
    "EndOnMonsterKills": []
}
```
- `Id` : From 1 - 98, the sequence of events that happens.
- `BeginTimeSec` : When that event should happen.
- `EndTimeSec` : When the event should stop (doesn't remove the monster)
- `Mode` : 1 of 3 modes. 
  - '0' - Maintain, 
  - '1' - Add, 
  - '2' - CustomEvent (Boss Encounter)
- `Repeats` : Whether during the spawn event the amount of monsters should be added to the game again.
- `RepeatIntervalSec` : Between `BeginTimeSec` and the `EndTimeSec`, every interval of this property the monsters are added again.
- `MonsterCode` : What monster to spawn
- `MonsterCount` : How many of this monster to spawn
- `SharedHealth` : Assumed that all the monsters in this event die when 1 is killed.
- `EndOnMonsterKills` : Custom Property that ends this event sequence once the condition has been met (Kill the boss, the boss encounter is done, spawn more monsters)

We would have a big issue with this if we just added say some amount of time to the `EndTimeSec`. 
Since if there wasn't a `Repeats` property extending the time between waves would just leave the player wandering around finding little to no enemies. 

Luckily the `Repeats` property is sort of unique to boss encounters and mini-boss encounters where those enemies don't repeat.
We can abuse this to get what we want (Longer Games)

The Vanilla Wave Data can be found [here](VanillaWaveData.json).

How we can achieve this is apply a multiplier to the `BeginTimeSec` (if not 0)
and the `EndTimeSec` (if not 0).

eg. Dracula spawns at 20 min (1200 sec) if we want him to appear at say 35 min instead we can do `35 / 20 = 1.75`.
That's our new multiplier. We multiply all waves and increase the time it takes for each wave to begin and end by 75%

This is enough of an opening for us to deliver this as a config option for the user to set.

Funny enough, this also provides us an option to speed games up by doing something like `15 / 20 = 0.75` meaning a game would last 15 mins.