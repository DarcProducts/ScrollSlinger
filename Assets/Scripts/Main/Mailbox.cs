using System;
using UnityEngine;

public class Mailbox : MonoBehaviour, ITakeScroll
{
    public static int totalSubscribersSet;
    [SerializeField] GameObject deliveredScroll;
    [SerializeField] GameObject[] subscriberEffects;
    [SerializeField, Header("Sets Dynamically")] bool _isSubscriber;
    [SerializeField] GameEvent PerfectDelivery;
    bool hasBeenDelivered;

    public bool IsSubscriber
    {
        get => _isSubscriber;
        set
        {
            _isSubscriber = value;
            if (value)
            {
                SetEffectsTrue();
                totalSubscribersSet++;
            }
            else
                SetEffectsFalse();
        }
    }

    void OnEnable()
    {
        if (deliveredScroll != null)
            deliveredScroll.SetActive(false);
        this.IsSubscriber = Subscriptions.IsASubscriber();
        if (_isSubscriber)
            hasBeenDelivered = false;
        else
            hasBeenDelivered = true;
    }


    public bool ScrollDelivered(bool wasPerfect)
    {
        if (!hasBeenDelivered)
        {
            hasBeenDelivered = true;
            if (wasPerfect)
            {
                deliveredScroll.SetActive(true);
                PerfectDelivery.Invoke(gameObject);
            }
            SetEffectsFalse();
            return true;
        }
        return false;
    }

    void SetEffectsFalse()
    {
        foreach (var e in subscriberEffects)
            e.SetActive(false);
    }

    void SetEffectsTrue()
    {
        foreach (var e in subscriberEffects)
            e.SetActive(true);
    }
}
