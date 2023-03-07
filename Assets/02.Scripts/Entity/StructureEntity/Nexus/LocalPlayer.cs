public static class LocalPlayer
{
    public static PlayerObjectControler Instance
    {
        get
        {
            if (CustomNetworkManager.Instance == null)
                return null;
            var instance = CustomNetworkManager.Instance.GetLocalPlayer();
            return instance == null ? null : instance;
        }
    }
}