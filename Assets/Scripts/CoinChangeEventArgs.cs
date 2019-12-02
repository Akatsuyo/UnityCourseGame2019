using System;

public class CoinChangeEventArgs : EventArgs {
    public int Change {get;}

    public CoinChangeEventArgs(int change)
    {
        Change = change;
    }
}