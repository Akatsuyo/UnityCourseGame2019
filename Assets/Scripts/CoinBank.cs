using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CoinBank : MonoBehaviour {
    private int _coins = 0;

    public int Coins {
        get {
            return _coins;
        }
        set {
            int oldValue = _coins;
            _coins = value;
            Changed?.Invoke(this, new CoinChangeEventArgs(value - oldValue));
        }
    }

    public event EventHandler<CoinChangeEventArgs> Changed;
    public event EventHandler Pickup;

    public void PickupCoins(int count) {
        Coins += count;
        Pickup?.Invoke(this, null);
    }

    public void RemoveCoins(int count) {
        Coins -= count;
    }

    public bool HasEnough(int count) {
        return count <= Coins;
    }
}
