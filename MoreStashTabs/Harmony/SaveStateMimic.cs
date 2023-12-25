namespace Dawn.DMD.MoreStashTabs.Harmony;

using Death.Items;

public class SaveStateMimic
{
    public static SaveStateMimic Create(object originalInstance)
    {
        var originalType = originalInstance.GetType();
        if (originalType.Name != "SaveState")
            throw new ArgumentException("The argument is not a SaveState, cannot mimic");

        var pagesInfo = originalType.GetField("Pages", BindingFlags.Public | BindingFlags.Instance);

        if (pagesInfo is null)
            throw new NullReferenceException("Unable to find the 'Pages' field");
        
        var pages = (ItemGrid.SaveState[])pagesInfo.GetValue(originalInstance);

        return new(pages);
    }

    public static void SyncOriginal(SaveStateMimic mimic, ref object originalInstance)
    {
        var originalType = originalInstance.GetType();
        if (originalType.Name != "SaveState")
            throw new ArgumentException("The argument is not a SaveState, cannot sync mimic with original");

        var pagesInfo = originalType.GetField("Pages", BindingFlags.Public | BindingFlags.Instance);
        
        if (pagesInfo is null)
            throw new NullReferenceException("Unable to find the 'Pages' field");
        
        pagesInfo.SetValue(originalInstance, mimic.Pages);
    }
    
    private SaveStateMimic(ItemGrid.SaveState[] pages)
    {
        Pages = pages;
    }
    
    public bool IsValid => Pages is { Length: > 0 };

    public ItemGrid.SaveState[] Pages;
}