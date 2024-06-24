using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.EfCore.Readers;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.EfCore
{
    public class EfEntityReader_Tests : EfEntityBaseOperator_Tests
    {

        #region Tests: Constructor

        [Fact]
        public void Constructor_If_Param_Is_Null_Then_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new EfEntityReader<TestDbContext>(null));
        }

        #endregion

        #region Tests: Get<TEntity>(object key, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetSingleKey_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var user = reader.Get<UserTc>(userAdmin.Id, asNoTracking);

            Assert.NotNull(user);
            Assert.Equal(userAdmin.Id, user.Key);

            var state = _dbContext.Entry(user).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: GetAsync<TEntity>(object key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetAsyncSingleKey_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var user = await reader.GetAsync<UserTc>(userAdmin.Id, asNoTracking);

            Assert.NotNull(user);
            Assert.Equal(userAdmin.Id, user.Key);

            var state = _dbContext.Entry(user).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: Get<TEntity>((object, object) key, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetCompositeKey2_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productA = DefaultData.ProductA;
            AddEnities(productA);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var product = reader.Get<ProductTc>((productA.Id, productA.CountryCode), asNoTracking);

            Assert.NotNull(product);
            Assert.Equal(productA.Id, product.Key1);
            Assert.Equal(productA.CountryCode, product.Key2);

            var state = _dbContext.Entry(product).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: GetAsync<TEntity>((object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetAsyncCompositeKey2_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productA = DefaultData.ProductA;
            AddEnities(productA);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var product = await reader.GetAsync<ProductTc>((productA.Id, productA.CountryCode), asNoTracking);

            Assert.NotNull(product);
            Assert.Equal(productA.Id, product.Key1);
            Assert.Equal(productA.CountryCode, product.Key2);

            var state = _dbContext.Entry(product).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: Get<TEntity>((object, object, object) key, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetCompositeKey3_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productOrder = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            AddEnities(productOrder);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var entity = reader.Get<ProductOrderTc>((productOrder.Key1, productOrder.Key2, productOrder.Key3), asNoTracking);

            Assert.NotNull(entity);
            Assert.Equal(productOrder.CompanyId, entity.Key1);
            Assert.Equal(productOrder.LocationId, entity.Key2);
            Assert.Equal(productOrder.UserId, entity.Key3);

            var state = _dbContext.Entry(entity).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: GetAsync<TEntity>((object, object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetAsyncCompositeKey3_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productOrder = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            AddEnities(productOrder);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            var entity = await reader.GetAsync<ProductOrderTc>((productOrder.Key1, productOrder.Key2, productOrder.Key3), asNoTracking);

            Assert.NotNull(entity);
            Assert.Equal(productOrder.CompanyId, entity.Key1);
            Assert.Equal(productOrder.LocationId, entity.Key2);
            Assert.Equal(productOrder.UserId, entity.Key3);

            var state = _dbContext.Entry(entity).State;

            if (asNoTracking)
            {
                Assert.Equal(EntityState.Detached, state);
            }
            else
            {
                Assert.Equal(EntityState.Unchanged, state);
            }
        }

        #endregion

        #region Tests: GetMany<TEntity>(IEnumerable<object> keys, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetManySingleKey_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<object> keys = [userAdmin.Id, userManager.Id];
            var users = reader.GetMany<UserTc>(keys, asNoTracking);

            Assert.NotEmpty(users);
            Assert.Equal(2, users.Count);

            if (asNoTracking)
            {
                users.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                users.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion

        #region Tests: GetManyAsync<TEntity>(IEnumerable<object> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetManyAsyncSingleKey_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<object> keys = [userAdmin.Id, userManager.Id];
            var users = await reader.GetManyAsync<UserTc>(keys, asNoTracking);

            Assert.NotEmpty(users);
            Assert.Equal(2, users.Count);

            if (asNoTracking)
            {
                users.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                users.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion

        #region Tests: GetMany<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetManyCompositeKey2_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<(object, object)> keys = [(productA.Key1, productA.Key2), (productB.Key1, productB.Key2)];
            var products = reader.GetMany<ProductTc>(keys, asNoTracking);

            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count);

            keys.All(k => products.FirstOrDefault(p => p.Key1 == (long)k.Item1 && p.Key2 == (string)k.Item2) is not null);

            if (asNoTracking)
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion

        #region Tests: GetManyAsync<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetManyAsyncCompositeKey2_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<(object, object)> keys = [(productA.Key1, productA.Key2), (productB.Key1, productB.Key2)];
            var products = await reader.GetManyAsync<ProductTc>(keys, asNoTracking);

            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count);

            keys.All(k => products.FirstOrDefault(p => p.Key1 == (long)k.Item1 && p.Key2 == (string)k.Item2) is not null);

            if (asNoTracking)
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion

        #region Tests: GetMany<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetManyCompositeKey3_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productOrderA = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            var productOrderB = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "102", 2);
            var productOrderC = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "103", 3);
            AddEnities(productOrderA, productOrderB, productOrderC);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<(object, object, object)> keys = [
                (productOrderA.Key1, productOrderA.Key2, productOrderA.Key3),
                (productOrderB.Key1, productOrderB.Key2, productOrderB.Key3),
            ];

            var products = reader.GetMany<ProductOrderTc>(keys, asNoTracking);

            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count);

            keys.All(k => products.FirstOrDefault(p => p.Key1 == (Guid)k.Item1 && p.Key2 == (string)k.Item2 && p.Key3 == (int)k.Item3) is not null);

            if (asNoTracking)
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion

        #region Tests: GetManyAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetManyAsyncCompositeKey3_If_Param_AsNoTracking_Is_True_Then_NoTracking(bool asNoTracking)
        {
            var productOrderA = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            var productOrderB = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "102", 2);
            var productOrderC = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "103", 3);
            AddEnities(productOrderA, productOrderB, productOrderC);

            var reader = new EfEntityReader<TestDbContext>(_dbContext);
            IEnumerable<(object, object, object)> keys = [
                (productOrderA.Key1, productOrderA.Key2, productOrderA.Key3),
                (productOrderB.Key1, productOrderB.Key2, productOrderB.Key3),
            ];

            var products = await reader.GetManyAsync<ProductOrderTc>(keys, asNoTracking);

            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count);

            keys.All(k => products.FirstOrDefault(p => p.Key1 == (Guid)k.Item1 && p.Key2 == (string)k.Item2 && p.Key3 == (int)k.Item3) is not null);

            if (asNoTracking)
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Detached);
            }
            else
            {
                products.All(u => _dbContext.Entry(u).State == EntityState.Unchanged);
            }
        }

        #endregion
    }
}