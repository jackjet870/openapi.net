﻿using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A base class for Car Delivery Network data entities.
    /// </summary>
    /// <typeparam name="T">The type of this ApiEntityBase class.</typeparam>
    public abstract class ApiEntityBase<T> : IApiEntity
    {
        /// <summary>
        /// A unique identifier for this resource, generated by Car Delivery Network.
        /// </summary>
        /// <remarks>
        /// Id is generated by Car Delivery Network on creation of the resource and cannot be changed.
        /// </remarks>
        public virtual int Id { get; set; }

        /// <summary>
        /// A unique identifier for this resource, generated by the client system
        /// </summary>
        /// <remarks>
        /// RemoteId is an optional, client system generated, unique Id by which the resource can be 
        /// referred to on Car Delivery Network.  Once the resource is created, RemoteId cannot be changed
        /// </remarks>
        public virtual string RemoteId { get; set; }

        /// <summary>
        /// ApiEntityBase
        /// </summary>
        public ApiEntityBase()
        {
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return this.ToString(MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }

        /// <summary>
        /// Reads the JSON document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The JSON serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T FromString(string serializedObject)
        {
            return Serialization.Deserialise<T>(serializedObject, MessageFormat.Json);
        }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<T>(serializedObject, format);
        }
    }
}