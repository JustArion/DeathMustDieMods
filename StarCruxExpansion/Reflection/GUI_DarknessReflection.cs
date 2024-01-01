namespace Dawn.DMD.StarCruxExpansion.Reflection;

using Cysharp.Threading.Tasks;
using Death.Darkness;
using Death.TimesRealm.UserInterface.Darkness;
using MonoMod.Utils;

public static class GUI_DarknessReflection
{
    private static readonly Lazy<Func<GUI_Darkness, IDarknessController, UniTask>> _setDataAsync = new(() =>
    {
        var setDataAsyncMethodInfo = typeof(GUI_Darkness).GetMethod("SetDataAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        return (setDataAsyncMethodInfo == null 
            ? null 
            : setDataAsyncMethodInfo.CreateDelegate<Func<GUI_Darkness, IDarknessController, UniTask>>()) ?? throw new InvalidOperationException();
    });
    
    
    public static UniTask SetDataAsync(this GUI_Darkness darkness, IDarknessController controller) => _setDataAsync.Value?.Invoke(darkness, controller) ?? UniTask.CompletedTask;
}