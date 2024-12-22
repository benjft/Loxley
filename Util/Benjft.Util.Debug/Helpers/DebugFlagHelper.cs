namespace Benjft.Util.Debug.Helpers;

public static class DebugFlagHelper {
    public const bool IsDebug =
        #if DEBUG
        true;
    #else
            false;
    #endif
}
