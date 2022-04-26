using UnityEngine;

public class Mailbox : MonoBehaviour, ITakeScroll
{
    public static int totalSubscribersSet;
    [SerializeField] GlobalInt TotalLifetimeSubscribers;
    [SerializeField] GameObject deliveredScroll;
    [SerializeField] GameObject[] subscriberEffects;
    [SerializeField] GameEvent PerfectDelivery;
    [SerializeField, Header("Sets Dynamically")] bool _isSubscriber;
    bool _hasBeenDelivered;

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
        if (LandPlotGenerator.TotalPlotsGenerated > GameManager.PlotsTillSubscribers)
            this.IsSubscriber = Subscriptions.IsASubscriber();
        else
            this.IsSubscriber = false;
        if (_isSubscriber)
        {
            _hasBeenDelivered = false;
            TotalLifetimeSubscribers.Value++;
        }
        else
            _hasBeenDelivered = true;
    }

    public bool ScrollDelivered(bool wasPerfect)
    {
        if (!_hasBeenDelivered)
        {
            _hasBeenDelivered = true;
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