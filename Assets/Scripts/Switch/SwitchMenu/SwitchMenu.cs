public class SwitchMenu : SwitchPages { 
    public void OnTabSelected(ISwitchableItem switchPageItem)
    {
        ChangeActiveItems(switchPageItem);
    }
}
