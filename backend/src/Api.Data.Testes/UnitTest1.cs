using App.Data.Context;
using App.Data.Implementations;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Testes
{
    public class UnitTest1: DataBaseTeste
    {
        private readonly IUserRepository _repository;
        public UnitTest1()
        {
            _repository = new UserImplementation(ContextoBD);
        }

        [Fact]
        public async void ReturnNull()
        {
            //var repo = new UserImplementation(ContextoBD);

            var result = await _repository.GetByLogin("TESTE");

            Assert.Null(result);
        }

        [Fact]
        public async void Delete()
        {            
            InsertData(ContextoBD);
            //var repo = new UserImplementation(ContextoBD);

            var result = await _repository.DeleteAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async void ReturnNotNull()
        {
            InsertData(ContextoBD);
            //var repo = new UserImplementation(ContextoBD);

            var result = await _repository.GetByLogin("TESTE");

            Assert.NotNull(result);
        }

        [Fact]
        public async void FailDelete()
        {
            InsertData(ContextoBD);
            //var repo = new SectorImplementation(ContextoBD);

            var result = await _repository.DeleteAsync(1);

            Assert.True(result);
        }
    }
}