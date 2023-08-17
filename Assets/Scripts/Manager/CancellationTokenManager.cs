using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CancellationTokenManager
{
    public static CancellationTokenSource battleCts = new CancellationTokenSource();
    public static CancellationToken battleCancellationToken = battleCts.Token;

    public static void ResetBattleToken()
    {
        battleCts.Cancel();
        battleCts.Dispose();
        battleCts = new CancellationTokenSource();
        battleCancellationToken = battleCts.Token;
    }
}
