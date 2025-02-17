# LeasingCompanyMS 

### Management system application for leasing company

#### Tech specifications:
* Target framework: .NET 8.0
* UI framework: WPF (Windows Presentation Foundation)
* Test framework: MSTest

#### Other application info:
* Output type: Windows Application
* Target OS: Windows
* Minimum OS version: 7.0

#### Functionalities:
* login (2 user types - user & admin)
* logout (back to login view)
* user actions:
  - browse cars
  - choose car
* admin actions:
  - manage cars
  - manage clients


#### Login credentials (Username/Password):
* admin/admin
* user/user

### How to use
Open LeasingCompanyMS.sln in Visual Studio. Project is ready to run.

### Login view
![login](https://github.com/user-attachments/assets/71ddc1bc-575d-420f-b4d5-0bc8fdfc2c32)


### User View 
![available](https://github.com/user-attachments/assets/4a6dfd58-f122-40d7-a7de-30469b4b3b14)

Column on the left is used for navigating between pages and for logging out. The panel on the right displays currently selected page

#### Available cars page
Table displays details all cars available for lease.
Combo boxes under the table allow selection of leasing options. Selecting all options and a desired car will calculate and display monthly lease installment. 
To lease selected vehicle press Lease button. A confirmation dialog will be shown, click Confirm to continue.

#### My leasings
![leasings](https://github.com/user-attachments/assets/49a90174-b12c-41b9-955f-0d0c7bd654a8)

Combo box in "My Leasings" section contains leasings for the current user. Picking an option from the drop down list allows to preview the payment schedule in the table below.

### Admin View

#### Manage cars
![manage cars](https://github.com/user-attachments/assets/1e0c8ff8-b178-42b7-b1e0-402515ab9116)

The table displays all cars, both available and leased. To add a ne car, click on the button below, fill in the details and confirm:

![manage cars dialog](https://github.com/user-attachments/assets/9cbd9ed8-5de7-4e2b-b45d-436142d69f4a)


#### Manage clients
![manage clients](https://github.com/user-attachments/assets/4152c43f-9c8b-4404-8506-122536fa0aa7)

Table displays all details of all clients, both admins and regular users. To add a new client, click on the button below, fill in the details and confirm:

![manage clients popup](https://github.com/user-attachments/assets/d7a19a7a-658c-46a7-80ff-06550d0a02e5)


