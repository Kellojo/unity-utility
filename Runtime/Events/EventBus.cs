using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyMessenger;
using System;

namespace Kellojo.Events {
    public static class EventBus {

        static TinyMessengerHub hub = new TinyMessengerHub();

        public static void Publish<TMessage>(TMessage message) where TMessage : class, ITinyMessage {
            hub.Publish(message);
        }
        public static void Subscribe<TMessage>(Action<TMessage> deliveryAction) where TMessage : class, ITinyMessage {
            hub.Subscribe(deliveryAction);
        }


    }
}
