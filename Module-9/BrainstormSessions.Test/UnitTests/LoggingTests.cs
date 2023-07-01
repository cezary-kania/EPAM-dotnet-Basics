using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Api;
using BrainstormSessions.Controllers;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Xunit;

namespace BrainstormSessions.Test.UnitTests
{
    public class LoggingTests
    {
        private List<LogEvent> _logEntries;
        private SerilogLoggerFactory _loggerFactory;
        public LoggingTests()
        {
            _logEntries = new List<LogEvent>();
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.Sink(new TestLogEventSink(@event => _logEntries.Add(@event)))
                .MinimumLevel.Debug()
                .CreateLogger();
            _loggerFactory = new SerilogLoggerFactory(serilogLogger);
        }

        [Fact]
        public async Task HomeController_Index_LogInfoMessages()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var mockLogger = _loggerFactory.CreateLogger<HomeController>();
            var controller = new HomeController(mockLogger, mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.True(_logEntries.Any(l => l.Level == LogEventLevel.Information), "Expected Info messages in the logs");
        }

        [Fact]
        public async Task HomeController_IndexPost_LogWarningMessage_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var mockLogger = _loggerFactory.CreateLogger<HomeController>();
            var controller = new HomeController(mockLogger, mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new HomeController.NewSessionModel();
        
            // Act
            var result = await controller.Index(newSession);
        
            // Assert
            Assert.True(_logEntries.Any(l => l.Level == LogEventLevel.Warning), "Expected Warn messages in the logs");
        }
        
        [Fact]
        public async Task IdeasController_CreateActionResult_LogErrorMessage_WhenModelStateIsInvalid()
        {
            // Arrange & Act
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            var mockLogger = _loggerFactory.CreateLogger<IdeasController>();
            var controller = new IdeasController(mockLogger, mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
        
            // Act
            var result = await controller.CreateActionResult(model: null);
        
            // Assert
            Assert.True(_logEntries.Any(l => l.Level == LogEventLevel.Error), "Expected Error messages in the logs");
        }
        
        [Fact]
        public async Task SessionController_Index_LogDebugMessages()
        {
            // Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(GetTestSessions().FirstOrDefault(
                    s => s.Id == testSessionId));
            var mockLogger = _loggerFactory.CreateLogger<SessionController>();
            var controller = new SessionController(mockLogger, mockRepo.Object);
        
            // Act
            var result = await controller.Index(testSessionId);
        
            // Assert
            Assert.True(_logEntries.Count(l => l.Level == LogEventLevel.Debug) == 2, "Expected 2 Debug messages in the logs");
        }
        
        private List<BrainstormSession> GetTestSessions()
        {
            var sessions = new List<BrainstormSession>();
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1,
                Name = "Test One"
            });
            sessions.Add(new BrainstormSession()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2,
                Name = "Test Two"
            });
            return sessions;
        }

    }
}
