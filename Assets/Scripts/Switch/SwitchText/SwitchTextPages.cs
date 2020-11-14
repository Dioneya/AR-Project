public class SwitchTextPages : SwitchPages
{
    public void OnTabSelected(ISwitchableItem switchPageItem)
    {
        ChangeActiveItems(switchPageItem);
    }
}
