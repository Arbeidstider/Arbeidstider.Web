using System.Collections.Generic;

namespace Arbeidstider.DataInterfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Add(T item);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Remove(T item);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Update(T item);

        /// <summary>
        /// Deletes the record by the ID
        /// </summary>
        /// <param name="id">
        /// The unique ID
        /// </param>
        void Delete(int id);

        IEnumerable<T> GetAll(object parameters);
        int Create(object parameters);
        T Get(object parameters);
        bool Update(object parameters);
        bool Exists(object parameters);
        bool Delete(object parameters);
    }
}
