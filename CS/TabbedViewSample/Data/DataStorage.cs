using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
using System;
using System.IO;

namespace Data {
    [XmlRoot("Employees")]
    public class Employees : List<Employee> {
    }
    [XmlRoot("Employee")]
    public class Employee {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get {
                return FirstName + " " + LastName;
            }
        }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryRegionName { get; set; }
        public string GroupName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        BitmapImage imageSource;
        [XmlIgnore]
        public BitmapImage ImageSource {
            get {
                if(imageSource == null) {
                    SetImageSource();
                }
                return imageSource;
            }
        }
        async void SetImageSource() {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(ImageData.AsBuffer());
            stream.Seek(0);

            imageSource = new BitmapImage();
            imageSource.SetSource(stream);
        }
    }

    public static class DataStorage {
        static Employees employees;
        public static Employees Employees {
            get {
                if(employees == null) {
                    StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Employees.xml")).AsTask().Result;

                    Stream stream = file.OpenStreamForReadAsync().Result;
                    XmlSerializer serializier = new XmlSerializer(typeof(Employees));
                    employees = serializier.Deserialize(stream) as Employees;
                }
                return employees;
            }
        }
    }
    public class EmployeesData {
        public Employees DataSource {
            get { return DataStorage.Employees; }
        }
    }
}