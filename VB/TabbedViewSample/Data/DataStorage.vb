Imports Microsoft.VisualBasic
Imports System.Xml.Serialization
Imports Windows.Storage
Imports Windows.Storage.Streams
Imports Windows.UI.Xaml.Media.Imaging
Imports System.Runtime.InteropServices.WindowsRuntime
Imports System.Collections.Generic
Imports System
Imports System.IO

Namespace Data
	<XmlRoot("Employees")> _
	Public Class Employees
		Inherits List(Of Employee)
	End Class
	<XmlRoot("Employee")> _
	Public Class Employee
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateParentId As Integer
		Public Property ParentId() As Integer
			Get
				Return privateParentId
			End Get
			Set(ByVal value As Integer)
				privateParentId = value
			End Set
		End Property
		Private privateFirstName As String
		Public Property FirstName() As String
			Get
				Return privateFirstName
			End Get
			Set(ByVal value As String)
				privateFirstName = value
			End Set
		End Property
		Private privateLastName As String
		Public Property LastName() As String
			Get
				Return privateLastName
			End Get
			Set(ByVal value As String)
				privateLastName = value
			End Set
		End Property
		Public ReadOnly Property FullName() As String
			Get
				Return FirstName & " " & LastName
			End Get
		End Property
		Private privateJobTitle As String
		Public Property JobTitle() As String
			Get
				Return privateJobTitle
			End Get
			Set(ByVal value As String)
				privateJobTitle = value
			End Set
		End Property
		Private privatePhone As String
		Public Property Phone() As String
			Get
				Return privatePhone
			End Get
			Set(ByVal value As String)
				privatePhone = value
			End Set
		End Property
		Private privateEmailAddress As String
		Public Property EmailAddress() As String
			Get
				Return privateEmailAddress
			End Get
			Set(ByVal value As String)
				privateEmailAddress = value
			End Set
		End Property
		Private privateAddressLine1 As String
		Public Property AddressLine1() As String
			Get
				Return privateAddressLine1
			End Get
			Set(ByVal value As String)
				privateAddressLine1 = value
			End Set
		End Property
		Private privateCity As String
		Public Property City() As String
			Get
				Return privateCity
			End Get
			Set(ByVal value As String)
				privateCity = value
			End Set
		End Property
		Private privatePostalCode As String
		Public Property PostalCode() As String
			Get
				Return privatePostalCode
			End Get
			Set(ByVal value As String)
				privatePostalCode = value
			End Set
		End Property
		Private privateCountryRegionName As String
		Public Property CountryRegionName() As String
			Get
				Return privateCountryRegionName
			End Get
			Set(ByVal value As String)
				privateCountryRegionName = value
			End Set
		End Property
		Private privateGroupName As String
		Public Property GroupName() As String
			Get
				Return privateGroupName
			End Get
			Set(ByVal value As String)
				privateGroupName = value
			End Set
		End Property
		Private privateBirthDate As DateTime
		Public Property BirthDate() As DateTime
			Get
				Return privateBirthDate
			End Get
			Set(ByVal value As DateTime)
				privateBirthDate = value
			End Set
		End Property
		Private privateHireDate As DateTime
		Public Property HireDate() As DateTime
			Get
				Return privateHireDate
			End Get
			Set(ByVal value As DateTime)
				privateHireDate = value
			End Set
		End Property
		Private privateGender As String
		Public Property Gender() As String
			Get
				Return privateGender
			End Get
			Set(ByVal value As String)
				privateGender = value
			End Set
		End Property
		Private privateMaritalStatus As String
		Public Property MaritalStatus() As String
			Get
				Return privateMaritalStatus
			End Get
			Set(ByVal value As String)
				privateMaritalStatus = value
			End Set
		End Property
		Private privateTitle As String
		Public Property Title() As String
			Get
				Return privateTitle
			End Get
			Set(ByVal value As String)
				privateTitle = value
			End Set
		End Property
		Private privateImageData As Byte()
		Public Property ImageData() As Byte()
			Get
				Return privateImageData
			End Get
			Set(ByVal value As Byte())
				privateImageData = value
			End Set
		End Property
		Private imageSource_Renamed As BitmapImage
		<XmlIgnore> _
		Public ReadOnly Property ImageSource() As BitmapImage
			Get
				If imageSource_Renamed Is Nothing Then
					SetImageSource()
				End If
				Return imageSource_Renamed
			End Get
		End Property
		Private async Sub SetImageSource()
			Dim stream As New InMemoryRandomAccessStream()
			await stream.WriteAsync(ImageData.AsBuffer())
			stream.Seek(0)

			imageSource_Renamed = New BitmapImage()
			imageSource_Renamed.SetSource(stream)
		End Sub
	End Class

	Public NotInheritable Class DataStorage
		Private Shared employees_Renamed As Employees
		Private Sub New()
		End Sub
		Public Shared ReadOnly Property Employees() As Employees
			Get
				If employees_Renamed Is Nothing Then
					Dim file As StorageFile = StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///Data/Employees.xml")).AsTask().Result

					Dim stream As Stream = file.OpenStreamForReadAsync().Result
					Dim serializier As New XmlSerializer(GetType(Employees))
					employees_Renamed = TryCast(serializier.Deserialize(stream), Employees)
				End If
				Return employees_Renamed
			End Get
		End Property
	End Class
	Public Class EmployeesData
		Public ReadOnly Property DataSource() As Employees
			Get
				Return DataStorage.Employees
			End Get
		End Property
	End Class
End Namespace