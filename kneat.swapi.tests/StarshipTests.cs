using kneat.swapi.Domain;
using Xunit;

namespace kneat.swapi.tests {
    public class StarshipTests {
        [Fact]
        public void GIVIN_VALID_PARAMETERS_RETURN_VALID_STARSHIP() {
            // arrange:
            var starshipConsubamles = "2 months";
            var starshipMGLT = 75;
            var starshipName = "Millennium Falcon";

            // act:
            var starship = new Starship(starshipName, starshipMGLT, starshipConsubamles);

            // assert:
            Assert.NotNull(starship);
            Assert.True(starship.IsValid);
        }

        [Fact]
        public void GIVIN_EMPTY_NAME_RETURN_ISVALID_FALSE_WITH_NOTIFICATION() {
            // arrange:
            var starshipConsubamles = "2 months";
            var starshipMGLT = 75;
            string starshipName = null;
            var expectedNotification = "Name is required.";

            // act:
            var starship = new Starship(starshipName, starshipMGLT, starshipConsubamles);

            // assert:
            Assert.False(starship.IsValid);
            Assert.Equal(starship.ValidationNotifications[0], expectedNotification);
        }

        [Fact]
        public void GIVIN_INVALID_CONSUMABLES_RETURN_ISVALID_FALSE_WITH_NOTIFICATION() {
            // arrange:
            var consubamles = "Not a valid consumable";
            var starshipMGLT = 75;
            var starshipName = "Millennium Falcon";
            var expectedNotification = "Consumables must be a valid day, week, month or year.";

            // act:
            var starship = new Starship(starshipName, starshipMGLT, consubamles);

            // assert:
            Assert.False(starship.IsValid);
            Assert.Equal(starship.ValidationNotifications[0], expectedNotification);
        }

        [Fact]
        public void GIVIN_NEGATIVE_MGLT_RETURN_ISVALID_FALSE_WITH_NOTIFICATION() {
            // arrange:
            var starshipConsubamles = "2 months";
            var starshipMGLT = -75;
            var starshipName = "Millennium Falcon";
            var expectedNotification = "MGLT must be gratter then 0.";

            // act:
            var starship = new Starship(starshipName, starshipMGLT, starshipConsubamles);

            // assert:
            Assert.False(starship.IsValid);
            Assert.Equal(starship.ValidationNotifications[0], expectedNotification);
        }

        [Fact]
        public void GIVEN_VALID_DISTANCE_IN_MGLT_RETURN_CALCULATED_STOPS() {
            // arrange:
            var distance = 1000000;
            var expectedStoppes = 9;
            var starshipConsubamles = "2 months";
            var starshipMGLT = 75;
            var starshipName = "Millennium Falcon";

            // act:
            var starship = new Starship(starshipName, starshipMGLT, starshipConsubamles);
            var stoppes = starship.CalculateStopsRequired(distance);

            // assert:
            Assert.Equal(stoppes, expectedStoppes);
        }
    }
}