﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Document entity
    /// </summary>
    public class Document : IApiEntity
    {
        /// <summary>
        /// The url of the document
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return this.ToString(Types.MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(Types.MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Document entities.
    /// </summary>
    [CollectionDataContract]
    public class Documents : ApiEntityCollectionBase<Document, Documents>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Documents"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Documents() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Documents"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Documents(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Documents"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="Documents">The collection of Documents whose elements are copied to the new collection.</param>
        public Documents(List<Document> Documents) : base(Documents) { }
    }
}
