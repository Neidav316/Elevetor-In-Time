using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManeger 
{
    public static event Action KeyCollected;
    public static void OnKeyCollected() => KeyCollected?.Invoke();

    public static event Action KeyUsed;
    public static void OnKeyUsed() => KeyUsed?.Invoke();

    public static event Action FadeOut;
    public static void OnFadeOut() => FadeOut?.Invoke();

    public static event Action ReplanishItemCollected;

    public static void OnHealthItemCollected() => ReplanishItemCollected?.Invoke();

    public static event Action TakenDamage;

    public static void OnTakenDamage() => TakenDamage?.Invoke();

    public static event Action Interact;

    public static void OnInteract() => Interact?.Invoke();

    public static event Action NotInteract;

    public static void OnNotInteract() => NotInteract?.Invoke();


}
