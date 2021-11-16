using EmotionalCalendar.Backend.Constracts.EmotionalEventContracts;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Tests
{
    public class EmotionEventRepositoryTests
    {
        private readonly Mock<IEmotionEventRepository> _repository = new Mock<IEmotionEventRepository>();
        private readonly List<Emotion> TestEmotions = new List<Emotion>
        {
            new Emotion { Id = 1, Name = "Happiness" },
            new Emotion { Id = 2, Name = "Sadness" },
            new Emotion { Id = 3, Name = "Curiosity" }
        };

        [OneTimeSetUp]
        public void SetupMocks()
        {
            _repository.Setup(p => p.GetAllEmotionsAsync()).ReturnsAsync(TestEmotions);
        }

        [TearDown]
        public void Reset()
        {
            _repository.Reset();
        }

        [Test]
        public async Task GetAllEmotionsTest_AllSuccess()
        {
            var expectedCount = 3;
            var expectedMinIdItem = new Emotion { Id = 1, Name = "Happiness" };

            var actualResult = await _repository.Object.GetAllEmotionsAsync();
            var actualCount = actualResult.Count();
            var actualMinIdItem = actualResult.FirstOrDefault();

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedMinIdItem.Id, actualMinIdItem.Id);
            Assert.AreEqual(expectedMinIdItem.Name, actualMinIdItem.Name);

            _repository.Verify(x => x.GetAllEmotionsAsync(), Times.Once);
        }
    }
}