﻿namespace Castle.RabbitMq.Stubs
{
	using System;
	using System.Collections.Generic;
	using RabbitMQ.Client;


	public class StubRabbitChannel : IRabbitChannel
	{
		private readonly StubRabbitExchange _defaultExchange;
		private readonly List<StubRabbitQueue> _queuesDeclared;
		private readonly List<StubRabbitQueue> _queuesDeclaredNoWait;
		private readonly List<StubRabbitExchange> _exchangesDeclared;
		private readonly List<StubRabbitExchange> _exchangesDeclaredNoWait;
		private readonly List<StubRabbitQueueBinding> _bound;
		private readonly List<StubRabbitQueueBinding> _boundNoWait;
		private readonly List<StubRabbitQueueBinding> _unbound;

		public StubRabbitChannel(ChannelOptions options)
		{
			this.Options = options;

			_defaultExchange = new StubRabbitExchange("", new ExchangeOptions(), o => null);

			_queuesDeclared = new List<StubRabbitQueue>();
			_queuesDeclaredNoWait = new List<StubRabbitQueue>();

			_exchangesDeclared = new List<StubRabbitExchange>();
			_exchangesDeclaredNoWait = new List<StubRabbitExchange>();

			_bound = new List<StubRabbitQueueBinding>();
			_boundNoWait = new List<StubRabbitQueueBinding>();
			_unbound = new List<StubRabbitQueueBinding>();
		}

		// Stub helpers

		public bool Disposed { get; private set; }

		public ChannelOptions Options { get; private set; }

		public List<StubRabbitQueue> QueuesDeclaredNoWait
		{
			get { return _queuesDeclaredNoWait; }
		}

		public List<StubRabbitQueue> QueuesDeclared
		{
			get { return _queuesDeclared; }
		}

		public List<StubRabbitExchange> ExchangesDeclared
		{
			get { return _exchangesDeclared; }
		}

		public List<StubRabbitExchange> ExchangesDeclaredNoWait
		{
			get { return _exchangesDeclaredNoWait; }
		}

		public List<StubRabbitQueueBinding> Bound
		{
			get { return _bound; }
		}

		public List<StubRabbitQueueBinding> BoundNoWait
		{
			get { return _boundNoWait; }
		}

		public List<StubRabbitQueueBinding> Unbound
		{
			get { return _unbound; }
		}

		// End Stub Helpers

		public IRabbitQueue DeclareQueue(string name, QueueOptions options)
		{
			var queue = new StubRabbitQueue(name, options);
			_queuesDeclared.Add(queue);
			return queue;
		}

		public IRabbitQueue DeclareQueueNoWait(string name, QueueOptions options)
		{
			var queue = new StubRabbitQueue(name, options);
			_queuesDeclaredNoWait.Add(queue);
			return queue;
		}

		public void Dispose()
		{
			this.Disposed = true;
		}

		public event Action<MessageUnroutedEventArgs> MessageUnrouted;

		public IRabbitExchange DefaultExchange
		{
			get { return _defaultExchange; }
		}

		public IRabbitExchange DeclareExchange(string name, ExchangeOptions options)
		{
			var exchange = new StubRabbitExchange(name, options, (o => null));
			_exchangesDeclared.Add(exchange);
			return exchange;
		}

		public IRabbitExchange DeclareExchangeNoWait(string name, ExchangeOptions options)
		{
			var exchange = new StubRabbitExchange(name, options, (o => null));
			_exchangesDeclaredNoWait.Add(exchange);
			return exchange;
		}

		public IRabbitQueueBinding Bind(IRabbitExchange exchange, IRabbitQueue queue, string routingKeyOrFilter)
		{
			var binding = new StubRabbitQueueBinding(exchange, queue, routingKeyOrFilter);
			_bound.Add(binding);
			return binding;
		}

		public IRabbitQueueBinding BindNoWait(IRabbitExchange exchange, IRabbitQueue queue, string routingKeyOrFilter)
		{
			var binding = new StubRabbitQueueBinding(exchange, queue, routingKeyOrFilter);
			_boundNoWait.Add(binding);
			return binding;
		}

		public void UnBind(IRabbitExchange exchange, IRabbitQueue queue, string routingKeyOrFilter = null)
		{
			var binding = new StubRabbitQueueBinding(exchange, queue, routingKeyOrFilter);
			_unbound.Add(binding);
		}

		public int ChannelNumber { get; set; }

		public IModel Model { get; set; }
	}
}