﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;
using CarDeliveryNetwork.Types.Interfaces;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Job entity.
    /// </summary>
    public class Job : ApiEntityBase<Job>, IApiEntity, IImportable
    {
        /// <summary>
        /// Readonly (50) - The job's unique job number generated by Car Delivery Network.
        /// </summary>
        /// <remarks>
        /// JobNumber is generated by Car Delivery Network on job creation and cannot be changed.
        /// Setting this property will have no effect on the underlying job.
        /// </remarks>
        public virtual string JobNumber { get; set; }

        /// <summary>
        /// Readonly - The job's current status
        /// </summary>
        public virtual JobStatus Status { get; set; }

        /// <summary>
        /// Mandatory (40) - Name of person/system initiating this job.
        /// </summary>
        public virtual string JobInitiator { get; set; }

        /// <summary>
        /// Optional (50) - A customer reference for this job (Does not have to be unique to this job.
        /// For a unique identifier see the Id and RemoteId fields).
        /// </summary>
        public virtual string CustomerReference { get; set; }

        /// <summary>
        /// Optional (ntext) - The job notes. These notes will be sent to the Driver.
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Mandatory - The service required for this job (Driven, Transported, Either or Auto). Cannot be Either if job is allocated to a network or carrier.
        /// </summary>
        public virtual ServiceType ServiceRequired { get; set; }

        /// <summary>
        /// Mandatory (.net JSON Date) - The requested pickup date for this job.
        /// </summary>
        public virtual DateTime? RequestedPickup { get; set; }

        /// <summary>
        /// Mandatory - When true, indicates that RequestedPickup is an exact date.  Pickup must be ON this date.
        /// When false, indicates that RequestedPickup is not exact.  Pickup should be FROM this date.
        /// </summary>
        public virtual bool RequestedPickupIsExactDate { get; set; }

        /// <summary>
        /// Mandatory (.net JSON Date) - The requested pickup date for this job.
        /// </summary>
        public virtual DateTime? RequestedDropoff { get; set; }

        /// <summary>
        /// Mandatory - When true, indicates that RequestedDropoff is an exact date.  Dropoff must be ON this date.
        /// When false, indicates that RequestedDropoff is not exact.  Dropoff should be BY this date.
        /// </summary>
        public virtual bool RequestedDropoffIsExactDate { get; set; }

        /// <summary>
        /// Optional - The estimated mileage of the job. If not specified system will try to calculate the mileage based on google mapping.
        /// </summary>
        public virtual int Mileage { get; set; }

        /// <summary>
        /// Optional - The estimated travel time of the job in minutes. If not specified system will try to calculate the travel time based on google mapping.
        /// </summary>
        public virtual int TravelTimeMinutes { get; set; }

        /// <summary>
        /// Optional - The price that the customer will be charged, in the smallest denomination of the currency 
        /// (pennies, cents etc). If not specified will be set to 0.
        /// </summary>
        public virtual int SellPrice { get; set; }

        /// <summary>
        /// Optional - The price that the transport company will be paid, in the smallest denomination of the currency 
        /// (pennies, cents etc). Must be specified if you are putting the Job into a Fixed Price Network.  If not specified will be set to 0.
        /// </summary>
        public virtual int BuyPrice { get; set; }

        /// <summary>
        /// Optional - The price that the carrier pays the driver
        /// (pennies, cents etc). Only used if you are the Carrier who pays the driver. If not specified will be set to 0.
        /// </summary>
        public virtual int DriverPay { get; set; }

        /// <summary>
        /// Mandatory - The customer details for this job.
        /// </summary>
        public virtual ContactDetails Customer { get; set; }

        /// <summary>
        /// Mandatory - The pick-up details for this job.
        /// </summary>
        public virtual ContactDetails Pickup { get; set; }

        /// <summary>
        /// Mandatory - The drop-off details for this job.
        /// </summary>
        public virtual ContactDetails Dropoff { get; set; }

        /// <summary>
        /// Mandatory - A collection of vehicles associated with this job.
        /// </summary>
        public virtual List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Optional - The SCAC of the allocated carrier
        /// </summary>
        /// <remarks>
        /// Specifying SCAC during job creation will ignore the Status field and attempt to 
        /// allocate the job directly to this carrier.  Status will be set to 'Allocated'
        /// </remarks>
        public virtual string AllocatedCarrierScac { get; set; }

        /// <summary>
        /// Readonly - The number of vehicles on this job
        /// </summary>
        public virtual int VehicleCount
        {
            get
            {
                return Vehicles == null
                    ? 0
                    : Vehicles.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Job"/> class.
        /// </summary>
        public Job()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this job.
        /// </summary>
        protected virtual void InitObjects()
        {
            Customer = new ContactDetails();
            Pickup = new ContactDetails();
            Dropoff = new ContactDetails();
            Vehicles = new List<Vehicle>();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Job job entities.
    /// </summary>
    [CollectionDataContract]
    public class Jobs : ApiEntityCollectionBase<Job, Jobs>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Jobs() : base() { } 

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public Jobs(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="jobs">The collection of jobs whose elements are copied to the new collection.</param>
        public Jobs(List<Job> jobs) : base(jobs) { }
    }
}