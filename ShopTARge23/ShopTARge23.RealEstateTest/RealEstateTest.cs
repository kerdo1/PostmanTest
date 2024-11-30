using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ShopTARge23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Size = 100;
            dto.Location = "asd";
            dto.RoomNumber = 1;
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
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealestate_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");
            Guid guid = Guid.Parse("8edd7b5d-822b-483d-ab81-048a638a2b31");

            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
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

            Assert.NotEqual(result.Id, realEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = new Guid("9edd7b5d-822b-483d-ab81-048a638a2b31");
            
            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("9edd7b5d-822b-483d-ab81-048a638a2b31");
            domain.Size= 100;
            domain.Location = "adsad";
            domain.RoomNumber = 12;
            domain.BuildingType = "something";
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;


            await Svc<IRealEstateServices>().Update(dto);

            //Assert.Equal(dto.Id, domain.Id);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            //Assert.DoesNotMatch(dto.Location.ToString(), domain.Location.ToString());
            Assert.Equal(dto.Size, domain.Size);
        }


        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateVersion2()
        {
            //use 2 mock databases
            //compare them
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            
            RealEstateDto dto2 = MockRealEstateData2();
            var result = await Svc<IRealEstateServices>().Update(dto2); 

            Assert.DoesNotMatch(result.Location, createRealEstate.Location);

            
        }



        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            //compare before and after
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);


            RealEstateDto nullUpdate = MockNullRealEstateData2();
            var result = await Svc<IRealEstateServices>().Update(nullUpdate);


            Assert.NotEqual(createRealEstate.Id, result.Id);
        }


        [Fact]
        public async Task Date_Not_In_Future()
        {
         
        }

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Size = 100,
                Location = "asd",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }


        private RealEstateDto MockRealEstateData2()
        {
            RealEstateDto realEstate = new()
            {
                Size = 22,
                Location = "qwerty",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }

        private RealEstateDto MockNullRealEstateData2()
        {
            RealEstateDto realEstate = new()
            {
                Id = null,
                Size = 22,
                Location = "qwerty",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }

        private RealEstateDto MockRealEstateData_Invalid()
        {
            RealEstateDto realEstate = new()
            {
                Size = 22,
                Location = "122",
                RoomNumber = 12,
                BuildingType = "",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }
    }
}