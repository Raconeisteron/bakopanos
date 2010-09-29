// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace UIComposition.Services.DataServiceReference
{
    [DebuggerStepThrough]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name = "Project", Namespace = "http://schemas.datacontract.org/2004/07/UIComposition.Contracts")]
    [Serializable]
    public class Project : object, IExtensibleDataObject, INotifyPropertyChanged
    {
        [OptionalField] private string ProjectNameField;

        [OptionalField] private string RoleField;
        [NonSerialized] private ExtensionDataObject extensionDataField;

        [DataMember]
        public string ProjectName
        {
            get { return ProjectNameField; }
            set
            {
                if ((ReferenceEquals(ProjectNameField, value) != true))
                {
                    ProjectNameField = value;
                    RaisePropertyChanged("ProjectName");
                }
            }
        }

        [DataMember]
        public string Role
        {
            get { return RoleField; }
            set
            {
                if ((ReferenceEquals(RoleField, value) != true))
                {
                    RoleField = value;
                    RaisePropertyChanged("Role");
                }
            }
        }

        #region IExtensibleDataObject Members

        [Browsable(false)]
        public ExtensionDataObject ExtensionData
        {
            get { return extensionDataField; }
            set { extensionDataField = value; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name = "Employee", Namespace = "http://schemas.datacontract.org/2004/07/UIComposition.Contracts")]
    [Serializable]
    public class Employee : object, IExtensibleDataObject, INotifyPropertyChanged
    {
        [OptionalField] private string AddressField;

        [OptionalField] private string CityField;

        [OptionalField] private string EmailField;

        [OptionalField] private int EmployeeIdField;

        [OptionalField] private string FirstNameField;

        [OptionalField] private string LastNameField;

        [OptionalField] private string PhoneField;

        [OptionalField] private string StateField;
        [NonSerialized] private ExtensionDataObject extensionDataField;

        [DataMember]
        public string Address
        {
            get { return AddressField; }
            set
            {
                if ((ReferenceEquals(AddressField, value) != true))
                {
                    AddressField = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        [DataMember]
        public string City
        {
            get { return CityField; }
            set
            {
                if ((ReferenceEquals(CityField, value) != true))
                {
                    CityField = value;
                    RaisePropertyChanged("City");
                }
            }
        }

        [DataMember]
        public string Email
        {
            get { return EmailField; }
            set
            {
                if ((ReferenceEquals(EmailField, value) != true))
                {
                    EmailField = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        [DataMember]
        public int EmployeeId
        {
            get { return EmployeeIdField; }
            set
            {
                if ((EmployeeIdField.Equals(value) != true))
                {
                    EmployeeIdField = value;
                    RaisePropertyChanged("EmployeeId");
                }
            }
        }

        [DataMember]
        public string FirstName
        {
            get { return FirstNameField; }
            set
            {
                if ((ReferenceEquals(FirstNameField, value) != true))
                {
                    FirstNameField = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        [DataMember]
        public string LastName
        {
            get { return LastNameField; }
            set
            {
                if ((ReferenceEquals(LastNameField, value) != true))
                {
                    LastNameField = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }

        [DataMember]
        public string Phone
        {
            get { return PhoneField; }
            set
            {
                if ((ReferenceEquals(PhoneField, value) != true))
                {
                    PhoneField = value;
                    RaisePropertyChanged("Phone");
                }
            }
        }

        [DataMember]
        public string State
        {
            get { return StateField; }
            set
            {
                if ((ReferenceEquals(StateField, value) != true))
                {
                    StateField = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        #region IExtensibleDataObject Members

        [Browsable(false)]
        public ExtensionDataObject ExtensionData
        {
            get { return extensionDataField; }
            set { extensionDataField = value; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName = "DataServiceReference.IProjectService")]
    public interface IProjectService
    {
        [OperationContract(Action = "http://tempuri.org/IProjectService/RetrieveProjects",
            ReplyAction = "http://tempuri.org/IProjectService/RetrieveProjectsResponse")]
        Project[] RetrieveProjects(int employeeId);
    }

    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IProjectServiceChannel : IProjectService, IClientChannel
    {
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class ProjectServiceClient : ClientBase<IProjectService>, IProjectService
    {
        public ProjectServiceClient()
        {
        }

        public ProjectServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public ProjectServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ProjectServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ProjectServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        #region IProjectService Members

        public Project[] RetrieveProjects(int employeeId)
        {
            return base.Channel.RetrieveProjects(employeeId);
        }

        #endregion
    }

    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName = "DataServiceReference.IEmployeeService")]
    public interface IEmployeeService
    {
        [OperationContract(Action = "http://tempuri.org/IEmployeeService/RetrieveEmployees",
            ReplyAction = "http://tempuri.org/IEmployeeService/RetrieveEmployeesResponse")]
        Employee[] RetrieveEmployees();
    }

    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IEmployeeServiceChannel : IEmployeeService, IClientChannel
    {
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class EmployeeServiceClient : ClientBase<IEmployeeService>, IEmployeeService
    {
        public EmployeeServiceClient()
        {
        }

        public EmployeeServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public EmployeeServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public EmployeeServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public EmployeeServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        #region IEmployeeService Members

        public Employee[] RetrieveEmployees()
        {
            return base.Channel.RetrieveEmployees();
        }

        #endregion
    }
}