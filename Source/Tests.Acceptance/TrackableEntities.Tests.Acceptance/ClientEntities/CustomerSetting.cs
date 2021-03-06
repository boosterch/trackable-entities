﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TrackableEntities.Client;

namespace TrackableEntities.Tests.Acceptance.ClientEntities
{
    [JsonObject(IsReference = true)]
    public class CustomerSetting : ModelBase<CustomerSetting>, ITrackable, IEquatable<CustomerSetting>
    {
        private string _customerId;
        public string CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value == _customerId) return;
                _customerId = value;
                NotifyPropertyChanged(m => m.CustomerId);
            }
        }

        private string _setting;
        public string Setting
        {
            get { return _setting; }
            set
            {
                if (value == _setting) return;
                _setting = value;
                NotifyPropertyChanged(m => m.Setting);
            }
        }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (value == _customer) return;
                _customer = value;
                CustomerChangeTracker = _customer == null ? null
                    : new ChangeTrackingCollection<Customer> { _customer };
                NotifyPropertyChanged(m => m.Customer);
            }
        }
        private ChangeTrackingCollection<Customer> CustomerChangeTracker { get; set; }

        public TrackingState TrackingState { get; set; }
        public ICollection<string> ModifiedProperties { get; set; }

        bool IEquatable<CustomerSetting>.Equals(CustomerSetting other)
        {
            if (EntityIdentifier != default(Guid))
                return EntityIdentifier == other.EntityIdentifier;
            return false;
        }

#pragma warning disable 414
        [JsonProperty]
        private Guid EntityIdentifier { get; set; }
        [JsonProperty]
        private Guid _entityIdentity = default(Guid);
#pragma warning restore 414
    }
}
