//***************************************************************************
// Copyright � 2010 Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//***************************************************************************

//Extracted from PkgDef Sample at http://code.msdn.microsoft.com/PkgDefEditor

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using System.Windows.Threading;

namespace Java.EditorExtensions
{
	/// <summary>
	/// Handy reusable utility to listen for change events on the associated buffer, but
	/// only pass these along to a set of listeners when the user stops typing for a half second
	/// </summary>
	static class BufferIdleEventUtil
	{
		static readonly object BufferListenersKey = new object();
		static readonly object BufferTimerKey = new object();

		#region Public interface

		public static bool AddBufferIdleEventListener(ITextBuffer buffer, EventHandler handler)
		{
			HashSet<EventHandler> listenersForBuffer;
			if (!TryGetBufferListeners(buffer, out listenersForBuffer))
				listenersForBuffer = ConnectToBuffer(buffer);

			if (listenersForBuffer.Contains(handler))
				return false;

			listenersForBuffer.Add(handler);

			return true;
		}

		public static bool RemoveBufferIdleEventListener(ITextBuffer buffer, EventHandler handler)
		{
			HashSet<EventHandler> listenersForBuffer;
			if (!TryGetBufferListeners(buffer, out listenersForBuffer))
				return false;

			if (!listenersForBuffer.Contains(handler))
				return false;

			listenersForBuffer.Remove(handler);

			if (listenersForBuffer.Count == 0)
				DisconnectFromBuffer(buffer);

			return true;
		}

		#endregion

		#region Helpers

		static bool TryGetBufferListeners(ITextBuffer buffer, out HashSet<EventHandler> listeners)
		{
			return buffer.Properties.TryGetProperty(BufferListenersKey, out listeners);
		}

		static void ClearBufferListeners(ITextBuffer buffer)
		{
			buffer.Properties.RemoveProperty(BufferListenersKey);
		}

		static bool TryGetBufferTimer(ITextBuffer buffer, out DispatcherTimer timer)
		{
			return buffer.Properties.TryGetProperty(BufferTimerKey, out timer);
		}

		static void ClearBufferTimer(ITextBuffer buffer)
		{
			DispatcherTimer timer;
			if (TryGetBufferTimer(buffer, out timer))
			{
				if (timer != null)
					timer.Stop();
				buffer.Properties.RemoveProperty(BufferTimerKey);
			}
		}

		static void DisconnectFromBuffer(ITextBuffer buffer)
		{
			buffer.Changed -= BufferChanged;

			ClearBufferListeners(buffer);
			ClearBufferTimer(buffer);

			buffer.Properties.RemoveProperty(BufferListenersKey);
		}

		static HashSet<EventHandler> ConnectToBuffer(ITextBuffer buffer)
		{
			buffer.Changed += BufferChanged;

			RestartTimerForBuffer(buffer);

			var listenersForBuffer = new HashSet<EventHandler>();
			buffer.Properties[BufferListenersKey] = listenersForBuffer;

			return listenersForBuffer;
		}

		static void RestartTimerForBuffer(ITextBuffer buffer)
		{
			DispatcherTimer timer;

			if (TryGetBufferTimer(buffer, out timer))
			{
				timer.Stop();
			}
			else
			{
				timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle)
				{
					Interval = TimeSpan.FromMilliseconds(500)
				};

				timer.Tick += (s, e) =>
				{
					ClearBufferTimer(buffer);

					HashSet<EventHandler> handlers;
					if (TryGetBufferListeners(buffer, out handlers))
					{
						foreach (var handler in handlers)
						{
							handler(buffer, new EventArgs());
						}
					}
				};

				buffer.Properties[BufferTimerKey] = timer;
			}

			timer.Start();
		}

		static void BufferChanged(object sender, TextContentChangedEventArgs e)
		{
			var buffer = sender as ITextBuffer;
			if (buffer == null)
				return;

			RestartTimerForBuffer(buffer);
		}

		#endregion
	}
}
