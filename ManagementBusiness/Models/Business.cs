using System;

namespace ManagementBusiness.Models
{
    public class Business : BaseModel
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private decimal _revenue;
        private DateTime _createdDate;
        private bool _isActive;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public decimal Revenue
        {
            get => _revenue;
            set => SetProperty(ref _revenue, value);
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set => SetProperty(ref _createdDate, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public Business()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }

        public Business(string name, string description, decimal revenue) : this()
        {
            Name = name;
            Description = description;
            Revenue = revenue;
        }
    }
}
