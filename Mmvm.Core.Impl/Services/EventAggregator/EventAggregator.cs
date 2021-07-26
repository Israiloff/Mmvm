using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Container.Enums;
using Israiloff.Mmvm.Net.Core.Services.EventAggregator;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.EventAggregator
{
    /// <summary>
    /// Central event dispatcher used to send application messages to registered handlers
    /// </summary>
    [Service(LifetimeScope = LifetimeScope.SingleInstance, Name = nameof(EventAggregator))]
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// Storage for all our registered handlers
        /// </summary>
        private readonly List<Delegate> _messageHandlers = new List<Delegate>();

        /// <summary>
        /// SynchronizationContext used to transition to the correct thread
        /// </summary>
        private readonly SynchronizationContext _messageSynchronizationContext;

        /// <summary>
        /// Initializes a new instance of the EventAggregator class.
        /// </summary>
        public EventAggregator()
        {
            _messageSynchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Send a message instance for immediate delivery
        /// </summary>
        /// <typeparam name="T">Type of the message</typeparam>
        /// <param name="message">Message to send</param>
        public void SendMessage<T>(T message)
        {
            if (message == null)
            {
                return;
            }

            if (_messageSynchronizationContext != null)
            {
                _messageSynchronizationContext.Send(m => Dispatch((T) m), message);
            }
            else
            {
                Dispatch(message);
            }
        }

        /// <summary>
        /// Post a message instance for asynchronous delivery
        /// </summary>
        /// <typeparam name="T">Type of the message</typeparam>
        /// <param name="message">Message to send</param>
        public void PostMessage<T>(T message)
        {
            if (message == null)
            {
                return;
            }

            if (_messageSynchronizationContext != null)
            {
                _messageSynchronizationContext.Post(m => Dispatch((T) m), message);
            }
            else
            {
                Dispatch(message);
            }
        }

        /// <summary>
        /// Register a message handler
        /// </summary>
        /// <param name="eventHandler">Message handler to add.</param>
        public Action<T> RegisterHandler<T>(Action<T> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            _messageHandlers.Add(eventHandler);
            return eventHandler;
        }

        /// <summary>
        /// Unregister a message handler
        /// </summary>
        /// <param name="eventHandler">Message handler to remove.</param>
        public void UnregisterHandler<T>(Action<T> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            _messageHandlers.Remove(eventHandler);
        }

        /// <summary>
        /// Dispatch a message to all appropriate handlers
        /// </summary>
        /// <typeparam name="T">Type of the message</typeparam>
        /// <param name="message">Message to dispatch to registered handlers</param>
        private void Dispatch<T>(T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _messageHandlers
                .OfType<Action<T>>()
                .ToList()
                .ForEach(action => action(message));
        }
    }
}