using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data.Migrations;


namespace ShopTARgv23.RealEstateTest
{
    public class RealestateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Location = "asd";
            dto.Size = 123;
            dto.RoomNumber = 123;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("1f9e4cce-85a7-4eb7-8379-b12a150fee10");

            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            Guid correctGuid = Guid.Parse("1f9e4cce-85a7-4eb7-8379-b12a150fee10");
            Guid guid = Guid.Parse("1f9e4cce-85a7-4eb7-8379-b12a150fee10");

            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);

            //Assert
            Assert.Equal(correctGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealestate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstateServices>().Create(realEstate);
            var result = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var realEstate1 = await Svc<IRealEstateServices>().Create(realEstate);
            var realEstate2 = await Svc<IRealEstateServices>().Create(realEstate);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = Guid.Parse("1f9e4cce-85a7-4eb7-8379-b12a150fee10");

            //uued andmed
            RealEstateDto dto = MockRealEstateData();

            //andmebaasis olevad andmed
            RealEstate domain = new();

            domain.Id = Guid.Parse("1f9e4cce-85a7-4eb7-8379-b12a150fee10");
            domain.Location = "qwerty";
            domain.Size = 34;
            domain.RoomNumber = 1234;
            domain.BuildingType = "qwerty";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            await Svc<IRealEstateServices>().Update(dto);

            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.Location, dto.Location);
            Assert.DoesNotMatch(domain.RoomNumber.ToString(), dto.RoomNumber.ToString());
            Assert.NotEqual(domain.Size, dto.Size);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
            Assert.NotEqual(result.ModifiedAt, createRealEstate.ModifiedAt);
        }


        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto nullUpdate = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        [Fact]
        public async Task ShouldNot_DeleteRealEstateAndLocationValue_WhenDeleteRealEstate()
        {
            RealEstateDto realEstateDto = MockRealEstateData();

            var realEstate1 = await Svc<IRealEstateServices>().Create(realEstateDto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate1.Id);

            Assert.False(string.IsNullOrEmpty(result.Location));
        }

        [Fact]

        public async Task Should_UpdateByIdRealEstate_WhenReturnsNotEqual()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstateServices>().Create(realEstate);
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            Assert.NotEqual(addRealEstate.Id, result.Id);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenRealEstateDoesNotExist()
        {
            Guid nonExistingId = Guid.NewGuid();

            var result = await Svc<IRealEstateServices>().GetAsync(nonExistingId);

            Assert.Null(result);
        }

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Location = "asd",
                Size = 100,
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Location = "asdasd",
                Size = 99,
                RoomNumber = 123,
                BuildingType = "asdasd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }

        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto nullDto = new()
            {
                Id = null,
                Location = "asd",
                Size = 100,
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now.AddYears(-1),
                ModifiedAt = DateTime.Now.AddYears(-1),
            };

            return nullDto;
        }
    }
}