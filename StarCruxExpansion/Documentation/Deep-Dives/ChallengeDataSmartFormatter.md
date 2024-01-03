Under the mountain of localizations the class `Death.UserInterface.Localization.Formatter_Stat` appeared after tons of digging.

The formatter acts as a custom formatter for the `Death.Run.Core.Stats` class and is used when the example format `Bosses have {0:stat(val|0.#|s|u|*100)} movement.` is used.

In this case, the output would be a flat movement speed increase and displayed as 

> Bosses have +25 movement

