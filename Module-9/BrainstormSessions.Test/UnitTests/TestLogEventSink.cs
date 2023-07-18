using System;
using Serilog.Core;
using Serilog.Events;

namespace BrainstormSessions.Test.UnitTests
{
    internal class TestLogEventSink : ILogEventSink
    {
        private readonly Action<LogEvent> _logEventDelegate;

        public TestLogEventSink(Action<LogEvent> logEventDelegate)
        {
            _logEventDelegate = logEventDelegate ?? throw new ArgumentNullException(nameof(logEventDelegate));
        }

        public void Emit(LogEvent logEvent)
        {
            _logEventDelegate?.Invoke(logEvent);
        }
    }
}