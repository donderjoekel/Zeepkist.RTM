using UnityEngine;
using ZeepSDK.Messaging;

namespace TNRD.Zeepkist.RTM.Messaging;

public class MessengerService : ITaggedMessenger
{
    private readonly ITaggedMessenger _messenger;
    
    public MessengerService()
    {
        _messenger = MessengerApi.CreateTaggedMessenger("RTM");
    }

    void ITaggedMessenger.Log(string message, float duration)
    {
        _messenger.Log(message, duration);
    }

    void ITaggedMessenger.LogSuccess(string message, float duration)
    {
        _messenger.LogSuccess(message, duration);
    }

    void ITaggedMessenger.LogWarning(string message, float duration)
    {
        _messenger.LogWarning(message, duration);
    }

    void ITaggedMessenger.LogError(string message, float duration)
    {
        _messenger.LogError(message, duration);
    }

    void ITaggedMessenger.LogCustomColors(string message, Color backgroundColor, Color textColor, float duration)
    {
        _messenger.LogCustomColors(message, backgroundColor, textColor, duration);
    }

    string ITaggedMessenger.Tag => _messenger.Tag;
}