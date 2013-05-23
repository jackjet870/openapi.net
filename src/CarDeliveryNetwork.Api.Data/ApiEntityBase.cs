﻿using System.Collections.Generic;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A base class for Car Delivery Network data entities.
    /// </summary>
    /// <typeparam name="T">The type of this ApiEntityBase class.</typeparam>
    public abstract class ApiEntityBase<T> : IApiEntity where T: new()
    {
        /// <summary>
        /// Readonly - A unique identifier for this resource, generated by Car Delivery Network.
        /// </summary>
        /// <remarks>
        /// Id is generated by Car Delivery Network on creation of the resource and cannot be changed.
        /// </remarks>
        public virtual int Id { get; set; }

        /// <summary>
        /// Optional (40) - A unique identifier for this resource, generated by the client system.
        /// </summary>
        /// <remarks>
        /// RemoteId is an optional, client system generated, unique Id by which the resource can be
        /// referred to on Car Delivery Network.  Once the resource is created, RemoteId cannot be changed.
        /// </remarks>
        public virtual string RemoteId { get; set; }

        /// <summary>
        /// Indicates that we are in a unit test scenario
        /// </summary>
        public static bool IsUnitTesting { get; set; }

        /// <summary>
        /// ApiEntityBase default constructor
        /// </summary>
        public ApiEntityBase()
        {
        }

        /// <summary>
        /// Returns a new instance of T
        /// </summary>
        /// <returns>A new instance of T</returns>
        public static T GetNew()
        {
            return new T();
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

        /// <summary>
        /// Takes the specified raw imported value, trims and performs ToUpper/ToLower as specified
        /// </summary>
        /// <param name="value">The raw imported value</param>
        /// <param name="trim">Trims whitespace from the ends of the string</param>
        /// <param name="toUpper">True: Upper case retrned, False: Lower case returnd, Null: Case not changed</param>
        /// <returns>The value with the specified tasks performed on it</returns>
        public static string GetSafeApiString(string value, bool trim = true, bool? toUpper = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (trim)
                value = value.Trim();

            return toUpper.HasValue
                ? toUpper.Value ? value.ToUpper() : value.ToLower()
                : value;
        }
    }
}