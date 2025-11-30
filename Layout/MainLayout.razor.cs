namespace BookMakerPredictionsFE.Layout
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;

        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void HandleNavItemClick()
        {
            _drawerOpen = false;
        }
    }
}
