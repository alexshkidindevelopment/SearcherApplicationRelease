using FakeItEasy;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SearcherApplication.DAL.Tests.Helpers
{
    public static class DbSetMockHelper 
    {
        public static DbSet<T> GetMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var dataAsQueryable = data.AsQueryable();
            var fakeDbSet = A.Fake<DbSet<T>>(b => b.Implements(typeof(IQueryable<T>)));

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).GetEnumerator()).Returns(dataAsQueryable.GetEnumerator());
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Provider).Returns(dataAsQueryable.Provider);
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Expression).Returns(dataAsQueryable.Expression);
            A.CallTo(() => ((IQueryable<T>)fakeDbSet).ElementType).Returns(dataAsQueryable.ElementType);
            return fakeDbSet;
        }
    }
}