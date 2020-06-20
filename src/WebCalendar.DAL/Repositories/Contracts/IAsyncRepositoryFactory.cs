using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.DAL.Repositories.Contracts
{
    /// <summary>
    /// Defines the interfaces for <see cref="IRepository{TEntity}"/> interfaces.
    /// </summary>
    interface IAsyncRepositoryFactory
    {
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomRepository"><c>True</c> if providing custom repositry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IAsyncRepository{TEntity}"/> interface.</returns>
        IAsyncRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    }
}
